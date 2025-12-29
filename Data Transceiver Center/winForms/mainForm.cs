using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 主窗体类，负责协调各子窗体交互及核心业务流程
    /// </summary>
    public partial class mainForm : Form, IMainConfigurable
    {
        #region 子窗体实例
        private Form1 _form1;    // 工作流程窗体
        private Form2 _form2;    // PLC通信窗体
        private Form3 _form3;    // MES设置窗体
        #endregion

        #region 网络配置
        private readonly IPAddress _localAddr = IPAddress.Parse("0.0.0.0"); // 本地所有网络地址
        private readonly int _localPortNum = int.Parse("8080");             // TCP服务器端口
        #endregion

        #region 相机监控线程相关
        private Thread _camMonitorThread;                       // 相机监控线程
        private bool _isCamMonitorRunning;                      // 相机监控线程运行状态
        private readonly object _camMonitorLock = new object(); // 线程同步锁
        #endregion

        #region 校验与SCN监控线程相关
        private bool _isCheckMonitorRunning = false; // 校验监控线程标记
        private bool _isScnMonitorRunning = false;   // SCN监控线程标记（原忽略校验时的PLC监控）
        private Thread _checkMonitorThread;          // 校验监控线程
        private Thread _scnMonitorThread;            // SCN监控线程
        #endregion

        #region 配置接口依赖
        private IIniConfigurable _iniLoadForm;   // Form1配置加载接口
        private IJsonConfigurable _jsonLoadForm; // Form3配置加载接口
        private IIniSavable _iniSaveForm;        // Form1配置保存接口
        private IJsonSavable _jsonSaveForm;      // Form3配置保存接口
        private IPLCService _plcService;
        #endregion

        // 在mainForm类中添加CheckHelper实例变量
        private CheckHelper _checkHelper;
        private readonly IniHelper _iniHelper = IniHelper.Instance; // 单例IniHelper实例
        private readonly LogHelper _logHelper = LogHelper.Instance; // 日志助手(单例模式)

        // 自动流程 状态标签
        public enum ProcessStatus
        {
            Wait,       // 等待触发
            Ready,      // 准备就绪
            Working,    // 执行中
            Complete,   // 完成
            Exception   // 异常
        }
        // 自动流程 状态标签
        public enum ProcessName
        {
            VCR,        // 相机拍二维码
            MES,        // MES通信
            ZPL,        // 生成ZPL
            Print,      // 发送打印
            Check       // 校验
        }


        #region 自动流程触发的互斥锁和状态变量
        /// <summary>
        /// 自动模式总开关：是否允许响应外部触发信号
        /// </summary>
        private bool _isAutoModeEnabled = false;

        /// <summary>
        /// 流程执行状态：是否正在执行AutoRun全流程（互斥用）
        /// </summary>
        private bool _isAutoRunning = false;

        /// <summary>
        /// 触发标记：防止同一模式下重复触发（跳过相机模式专用）
        /// </summary>
        private bool _isAutoProcessTriggered = false;

        /// <summary>
        /// 流程执行互斥锁：保护_isAutoRunning的原子性
        /// </summary>
        private readonly object _autoRunLock = new object();

        /// <summary>
        /// 触发标记互斥锁：保护_isAutoProcessTriggered的原子性
        /// </summary>
        private readonly object _triggerLock = new object();

        /// <summary>
        /// TCP服务器实例（统一管理TCP连接和数据接收）
        /// </summary>
        private TCPServer _tcpServer;
        #endregion

        /// <summary>
        /// 主窗体构造函数
        /// </summary>
        public mainForm()
        {
            InitializeComponent();

            // 初始化子窗体
            _form2 = new Form2();
            _plcService = _form2;  // 将 Form2 赋值给 _plcService（因 Form2 实现了 IPLCService）
            _form1 = new Form1(_plcService);
            _form3 = new Form3();

            // 初始化校验助手类
            _checkHelper = new CheckHelper(_form1, _plcService);

            // 依赖注入：绑定配置接口实现
            _iniLoadForm = _form1;
            _jsonLoadForm = _form3;
            _iniSaveForm = _form1;
            _jsonSaveForm = _form3;
           
            // 初始化子窗体嵌入属性
            InitChildForm(_form1);
            InitChildForm(_form2);
            InitChildForm(_form3);

            // 事件订阅：建立子窗体间数据同步机制
            _form1.btnRetryRead += btn_RetryRead_Click; // form1的手动读码按钮事件
            _form1.btnRetryChk += btn_RetryChk_Click;   // form1的手动验码按钮事件
            _form1.TextUpdated += OnForm1TextChanged;   // form1的文本框更新事件
            _form1.RequestMesRootData += OnForm1RequestMesRootData; // form1的Mes查询事件
            _form1.SerialDataReceived += OnSerialDataReceived; // form1的串口收到数据事件
            _form1.PanelIDgot += CamOK;                 // form1的Cam放行事件
            this.FormClosing += mainForm_FormClosing;   // 窗口关闭

            // 默认显示工作流程窗体
            ShowForm(_form1); 
        }

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + "  V" + Application.ProductVersion;
            this.statusTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");

            // 可选：自定义日志配置
            LogHelper.Instance.SetLogConfig(
                //logFilePath: "AutoRun.log",
                maxLogSize: 5 * 1024 * 1024, // 5MB
                maxHistoryLogs: 10
            );
            // 启动信息
            _logHelper.Log("\r\n##################", "\r\n##################", "\r\n##################");
            _logHelper.Log("软件启动", "INFO", $"启动时间：{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}");
        }

        #region 子窗体管理
        /// <summary>
        /// 初始化子窗体嵌入属性
        /// </summary>
        /// <param name="childForm">子窗体实例</param>
        private void InitChildForm(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            childForm.Visible = false;
            childForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        }

        /// <summary>
        /// 在容器中显示指定子窗体
        /// </summary>
        /// <param name="form">要显示的子窗体</param>
        private void ShowForm(Form form)
        {
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(form);
            form.Visible = true;
        }

        /// <summary>
        /// 打开工作流程窗体
        /// </summary>
        private void btn_form1Open(object sender, EventArgs e)
        {
            ShowForm(_form1);
        }

        /// <summary>
        /// 打开PLC通信窗体
        /// </summary>
        private void btn_form2Open(object sender, EventArgs e)
        {
            ShowForm(_form2);
        }

        /// <summary>
        /// 打开MES设置窗体
        /// </summary>
        private void btn_form3Open(object sender, EventArgs e)
        {
            ShowForm(_form3);
        }
        #endregion

        #region 子窗体数据同步
        /// <summary>
        /// 同步Form1的文本到Form3
        /// </summary>
        private void OnForm1TextChanged(string text)
        {
            // 若勾选跳过相机，则清空文本
            if (checkBox_ignoreCam.Checked)
            {
                text = "";
            }
            _form3.UpdateIDText(text);
        }

        /// <summary>
        /// 处理Form1的MES数据请求
        /// </summary>
        private void OnForm1RequestMesRootData()
        {
            // 从Form3获取MES数据
            MesPostRoot mesData = _form3.MesPostRoot;

            // 若勾选跳过相机，清空panelID
            if (checkBox_ignoreCam.Checked)
            {
                mesData.MesData.input.panelId = "";
            }

            // 将数据传递给Form1
            _form1.ReceiveMesRootData(mesData);
        }

        /// <summary>
        /// 处理Form1的相机放行请求
        /// </summary>
        private void CamOK()
        {
            WritePLCReg(cam: CommunicationProtocol.camOK);
            Console.WriteLine($"已发送{CommunicationProtocol.camOK}给{CommunicationProtocol.camRegister}");
        }
        #endregion

        #region 配置管理
        /// <summary>
        /// 保存配置按钮点击事件
        /// </summary>
        private void btnSaveIni_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "请选择配置保存位置";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                string savePath = dialog.SelectedPath;
                string iniFile = Path.Combine(savePath, "settings.ini");
                string jsonFile = Path.Combine(savePath, "MesSettings.json");

                // 保存各窗体配置
                SaveForm1Config(iniFile);
                SaveForm3Config(jsonFile);
                SaveMainConfig(iniFile);

                MessageBox.Show($"配置已保存到:\r\n{savePath}");
            }
        }

        /// <summary>
        /// 加载配置按钮点击事件
        /// </summary>
        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            // 选择并加载INI配置文件
            string iniFile = SelectFile("请选择ini文件", "ini文件(*.ini)|*.ini");
            if (string.IsNullOrEmpty(iniFile)) return;

            // 选择并加载JSON配置文件
            string jsonFile = SelectFile("请选择json文件", "json文件(*.json)|*.json");
            if (string.IsNullOrEmpty(jsonFile)) return;

            // 加载配置
            LoadForm1Config(iniFile);
            LoadForm3Config(jsonFile);
            LoadMainConfig(iniFile);
        }

        /// <summary>
        /// 文件选择通用方法
        /// </summary>
        /// <param name="title">对话框标题</param>
        /// <param name="filter">文件过滤规则</param>
        /// <returns>选中的文件路径，未选中则返回null</returns>
        private string SelectFile(string title, string filter)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = title;
                dialog.Filter = filter;
                return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
            }
        }

        /// <summary>
        /// 线程安全加载Form1的INI配置
        /// </summary>
        private void LoadForm1Config(string iniFile)
        {
            if (_iniLoadForm is Form form && form.InvokeRequired)
            {
                form.Invoke(new Action(() => _iniLoadForm.LoadIni(iniFile)));
            }
            else
            {
                _iniLoadForm.LoadIni(iniFile);
            }
        }

        /// <summary>
        /// 线程安全加载Form3的JSON配置
        /// </summary>
        private void LoadForm3Config(string jsonFile)
        {
            if (_jsonLoadForm is Form form && form.InvokeRequired)
            {
                form.Invoke(new Action(() => _jsonLoadForm.LoadJson(jsonFile)));
            }
            else
            {
                _jsonLoadForm.LoadJson(jsonFile);
            }
        }

        /// <summary>
        /// 加载主窗体自身配置
        /// </summary>
        public void LoadMainConfig(string iniFilePath)
        {
            // 校验文件是否存在
            if (!File.Exists(iniFilePath))
            {
                MessageBox.Show($"ini配置文件不存在：{iniFilePath}");
                return;
            }

            try
            {
                // 加载复选框状态
                checkBox_ignoreCam.Checked   = _iniHelper.ReadBoolean(iniFilePath, "ignoreCam",   "Checkbox", false);
                checkBox_ignoreCheck.Checked = _iniHelper.ReadBoolean(iniFilePath, "ignoreCheck", "Checkbox", false);
                checkBox_ignorePlc.Checked   = _iniHelper.ReadBoolean(iniFilePath, "ignorePlc",   "Checkbox", false);
                checkBox_connectPlc.Checked  = _iniHelper.ReadBoolean(iniFilePath, "connectPlc",  "Checkbox", false);
                checkBox_autoMode.Checked    = _iniHelper.ReadBoolean(iniFilePath, "autoRun",     "Checkbox", false);
                checkBox_tcpServer.Checked   = _iniHelper.ReadBoolean(iniFilePath, "tcpServer",   "Checkbox", false);

                Console.WriteLine("mainForm CheckBox状态加载完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载mainForm配置失败：{ex.Message}");
            }
        }

        /// <summary>
        /// 线程安全保存Form1的INI配置
        /// </summary>
        private void SaveForm1Config(string iniFile)
        {
            if (_iniSaveForm is Form form && form.InvokeRequired)
            {
                form.Invoke(new Action(() => _iniSaveForm.SaveIniSettings(iniFile)));
            }
            else
            {
                _iniSaveForm.SaveIniSettings(iniFile);
            }
        }

        /// <summary>
        /// 线程安全保存Form3的JSON配置
        /// </summary>
        private void SaveForm3Config(string jsonFile)
        {
            if (_jsonSaveForm is Form form && form.InvokeRequired)
            {
                form.Invoke(new Action(() => _jsonSaveForm.SaveJson(jsonFile)));
            }
            else
            {
                _jsonSaveForm.SaveJson(jsonFile);
            }
        }

        /// <summary>
        /// 保存主窗体自身配置
        /// </summary>
        private void SaveMainConfig(string iniFilePath)
        {
            try
            {
                // 保存复选框状态
                _iniHelper.WriteBoolean(iniFilePath, "ignoreCam",   "Checkbox", checkBox_ignoreCam.Checked);
                _iniHelper.WriteBoolean(iniFilePath, "ignoreCheck", "Checkbox", checkBox_ignoreCheck.Checked);
                _iniHelper.WriteBoolean(iniFilePath, "ignorePlc",   "Checkbox", checkBox_ignorePlc.Checked);
                _iniHelper.WriteBoolean(iniFilePath, "connectPlc",  "Checkbox", checkBox_connectPlc.Checked);
                _iniHelper.WriteBoolean(iniFilePath, "autoRun",     "Checkbox", checkBox_autoMode.Checked);
                _iniHelper.WriteBoolean(iniFilePath, "tcpServer",   "Checkbox", checkBox_tcpServer.Checked);

                Console.WriteLine("MainForm配置保存完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存主窗体配置失败：{ex.Message}");
            }
        }
        #endregion

        #region 自动运行流程
        /// <summary>
        /// 手动触发自动流程按钮点击事件
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void autoRun_btn_Click(object sender, EventArgs e)
        {
            // 手动触发忽略触发源过滤，强制执行
            TriggerAutoRun(isManualTrigger: true, "手动按钮");
        }

        /// <summary>
        /// 全流程执行逻辑（仅负责按顺序执行操作，不处理触发判断）
        /// 流程：读PLC -> MES通信 -> 生成打印指令 -> 发送打印 -> 校验 -> 写PLC结果
        /// </summary>
        
        private async void ExecuteFullProcess()
        {
            short cam = -1, prt = -1, scn = -1;
            string consoleInfo = "";

            // ========== 1. 读取PLC状态（调用拆分后的函数） ==========
            _logHelper.Log("PLC", "INFO", "开始读取PLC状态");

            var plcResult = ReadPlcStatus();
            if (!plcResult.Success)
            {
                // 读取失败时的处理（保持原逻辑：终止流程）
                consoleInfo = "PLC读取失败，终止本次自动流程";
                _logHelper.Log("PLC", "ERROR", consoleInfo);
                _logHelper.Log("PLC", "ERROR", $"读取失败详情：cam={plcResult.Cam}, prt={plcResult.Prt}, scn={plcResult.Scn}");
                UpdateStatusBar("PLC", "ERROR", $"PLC连接失败，详情：cam={plcResult.Cam}, prt={plcResult.Prt}, scn={plcResult.Scn}");
                return;
            }
            // 读取成功，赋值给变量
            cam = plcResult.Cam;
            prt = plcResult.Prt;
            scn = plcResult.Scn;
            _logHelper.Log("PLC", "INFO", $"PLC状态读取成功：cam={cam}, prt={prt}, scn={scn}");
            // 若屏蔽PLC，已在ReadPlcStatus中返回(-1,-1,-1)，无需额外处理

            // ========== 2. 重构MES通信部分 ==========
            // 步骤2.1：简化MES参数（仅保留必要的配置获取）
            string postUrl = "";
            MsePostData mesPostData = null; // 替换原postJson，直接用实体类
            string postJson = ""; // 保留原postJson变量，用于打印请求参数

            try
            {
                // 从Form3获取MES配置（保留原有配置来源，仅修改数据类型）
                _logHelper.Log("MES", "INFO", "开始获取MES配置信息");
                postUrl = _form3.MesPostRoot.MesUrl;
                mesPostData = _form3.MesPostRoot.MesData; // 直接获取实体类，无需手动序列化
                // 保留原有序列化逻辑，用于打印请求JSON
                postJson = JsonConvert.SerializeObject(mesPostData, Formatting.Indented);

                _logHelper.Log("MES", "INFO", $"MES配置获取成功 - URL：{postUrl}");

                // 新增：更新“发送消息”到Form1的txtBox_postData（线程安全）
                BeginInvoke(new Action(() =>
                {
                    _form1.txtBox_postData.Text = $"POST发送:\r\n{JsonConvert.SerializeObject(mesPostData)}";
                }));
            }
            catch (Exception ex)
            {
                consoleInfo = $"Mes设置未导入: {ex.Message}";
                _logHelper.Log("MES", "ERROR", $"异常堆栈：{ex.StackTrace}"); // 保留异常堆栈
                // 配置错误直接终止流程
                return;
            }
            // 步骤2.2：调用MES通信助手类
            _logHelper.Log("MES", "INFO", "开始发送MES POST请求");
            UpdateStatusBar("MES", "INFO", "开始发送MES POST请求");
            var mesResult = await MesCommunicationHelper.Instance.SendMesRequestWithRawAsync(postUrl, mesPostData);
            // 调用后查看日志（定位问题）
            Console.WriteLine($"最终结果：Success={mesResult.Success}，Info={mesResult.Info}");
            Console.WriteLine($"原始响应：{mesResult.RawResponse}");

            // 步骤2.3：处理MES响应结果（复用原有业务逻辑，仅适配新的返回值）
            if (mesResult.Success)
            {
                // MES通信成功（逻辑与原一致）
                consoleInfo = $"已收到MES回复的数据：{mesResult.RawResponse}";
                _logHelper.Log("MES", "INFO", consoleInfo);
                _logHelper.Log("MES", "INFO", $"MES响应详情：{mesResult.Info}");
                WritePLCReg(cam: CommunicationProtocol.camOK);

                // 新增1：更新“接收消息”到Form1的txtBox_responseData（线程安全）
                BeginInvoke(new Action(() =>
                {
                    _form1.txtBox_responseData.Text = $"Mes响应:\r\n{mesResult.RawResponse}";
                }));

                // 新增：生成二维码并更新标签

                // MES返回的是JSON对象，需先解析
                string qrCodeContent = mesResult.Data; // 需要显示panelId字段

                BeginInvoke(new Action(() =>
                {
                    _form1.SafeSetLbReadCode(CommunicationProtocol.readCodeOK);
                    _logHelper.Log("UI", "INFO", "更新UI：条码读取状态设为OK");

                    // 1. 生成二维码并显示到pictureBox（假设使用pictureBox1显示）
                    _form1.pictureBox1.Image = Form1.SetBarCode128(qrCodeContent);

                    // 2. 将数值显示到lastPrtCode_label
                    _form1.lastPrtCode_label.Text = qrCodeContent;

                    // 3. 可选：同步更新打印码文本框
                    _form1.txtBox_prtCode.Text = qrCodeContent;

                }));

                cam = CommunicationProtocol.camOK;
                _logHelper.Log("PLC", "INFO", $"写入PLC状态：cam={cam}（camOK）");
                UpdateStatusBar("PLC", "INFO", $"MES通信OK，写入PLC状态：cam={cam}（camOK）");
            }
            else
            {
                // MES通信失败（返回code不是200时）
                consoleInfo = mesResult.Info;
                _logHelper.Log("MES", "ERROR", $"MES通信失败：{consoleInfo}");
                UpdateStatusBar("MES", "ERROR", $"MES通信失败{consoleInfo}");
                WritePLCReg(cam: CommunicationProtocol.camNG);
                BeginInvoke(new Action(() =>
                {
                    _form1.SafeSetLbReadCode(CommunicationProtocol.readCodeNG);
                    _form1.txtBox_responseData.Text = $"Mes响应:\r\n{mesResult.RawResponse}";

                    _logHelper.Log("UI", "INFO", "更新UI：条码读取状态设为NG");
                }));

                cam = CommunicationProtocol.camNG;
                _logHelper.Log("PLC", "INFO", $"写入PLC状态：cam={cam}（camNG）");
                UpdateStatusBar("PLC", "INFO", $"MES通信失败，写入PLC状态：cam={cam}（camNG）");
                _logHelper.Log("AutoRun", "WARN", "MES通信失败，终止本次自动流程");
                return; // 失败终止后续流程
            }

            // 新增一个判断，拍照位的阻挡气缸是否放下，放下才开始打印流程
            bool camHolderReady = true;
            if (!checkBox_ignorePlc.Checked) // 如果未勾选"忽略PLC"，则执行等待
            {
                camHolderReady = await WaitForCamHolderZeroAsync(); // 调用等待函数
            }
            else
            {
                _logHelper.Log("打印流程", "INFO", "已勾选忽略PLC，跳过阻挡气缸检查");
            }

            // 如果等待成功（或被跳过），则执行打印模块
            if (camHolderReady)
            {
                // 如果阻挡气缸放下了，则继续执行；如果没有，则结束打印流程
            }
            else
            {
                _logHelper.Log("打印流程", "ERROR", "等待camHolder失败，终止打印流程");
                UpdateStatusBar("打印流程", "ERROR", "等待camHolder失败，终止打印流程");
                return;
            }

            // ========== 打印模块 ==========
            _logHelper.Log("打印", "INFO", "进入打印流程，开始前置检查");
            // 标记是否需要等待PLC就绪（屏蔽PLC开关）
            bool needWaitPlcReady = !checkBox_ignorePlc.Checked;
            if (needWaitPlcReady)
            {
                _logHelper.Log("打印", "INFO", "屏蔽PLC未勾选，开始等待打印机就绪信号（PLC prtReady）");
                prt = plcResult.Prt; // 复用之前读取的PLC状态，避免重复调用

                // 步骤1：等待打印机就绪（原有逻辑保留）
                while (prt != CommunicationProtocol.prtReady)
                {
                    // 刷新PLC状态（复用ReadPlcStatus，保持逻辑统一）
                    var plcRefreshResult = ReadPlcStatus();
                    if (!plcRefreshResult.Success)
                    {
                        _logHelper.Log("打印", "ERROR", "等待打印机就绪时PLC读取失败，终止打印流程");
                        prt = CommunicationProtocol.prtNG;
                        WritePLCReg(prt: prt);
                        BeginInvoke(new Action(() => _form1.SafeSetLbPrtCode(CommunicationProtocol.prtCodeNG)));
                        return;
                    }
                    prt = plcRefreshResult.Prt;
                    cam = plcRefreshResult.Cam;
                    scn = plcRefreshResult.Scn;

                    _logHelper.Log("打印", "WARN", $"打印机未就绪，当前PLC状态：prt={prt}，等待500ms后重试");
                    UpdateStatusBar("打印", "WARN", $"打印机未就绪，当前PLC状态：prt={prt}，等待500ms后重试");
                    await Task.Delay(500);
                }
                _logHelper.Log("打印", "INFO", "打印机就绪信号已获取（prt=prtReady）");
            }
            else
            {
                // 步骤1（屏蔽PLC）：跳过等待，直接标记为“逻辑就绪”
                _logHelper.Log("打印", "WARN", "屏蔽PLC已勾选，跳过打印机就绪信号校验");
                prt = CommunicationProtocol.prtReady; // 强制设为就绪，推进流程
            }

            // 步骤2：调用PrintingHelper执行打印（无论是否屏蔽PLC，都执行打印）
            try
            {
                _logHelper.Log("打印", "INFO", "开始调用打印助手类执行打印");
                // 获取Form1的打印配置（已暴露属性）
                string zplTemplatePath = _form1.ZplTemplatePath;
                string printerPath = _form1.PrintName;
                string printCode = mesResult.Data; // MES返回的打印内容

                // 异步调用打印助手类
                _logHelper.Log("打印触发", "INFO", "准备调用PrintingHelper.ExecutePrintAsync");
                var printResult = await PrintingHelper.Instance.ExecutePrintAsync(
                    printCode: printCode,
                    zplTemplatePath: zplTemplatePath,  // 直接使用Form1暴露的模板路径属性
                    printerPath: printerPath
                );

                // 步骤3：处理打印结果（统一逻辑+标准化日志）
                if (printResult.Success)
                {
                    _logHelper.Log("打印", "INFO", $"打印成功：{printResult.Message}");
                    prt = CommunicationProtocol.prtOK;
                    // 线程安全更新UI
                    BeginInvoke(new Action(() => _form1.SafeSetLbPrtCode(CommunicationProtocol.prtCodeOK)));

                    // 即使屏蔽PLC，仍写入PLC状态（若不需要可加判断：if(!ignorePlc_checkBox.Checked)）
                    WritePLCReg(prt: prt);
                    _logHelper.Log("PLC", "INFO", $"打印成功，写入PLC状态：prt={prt}（prtOK）");
                    UpdateStatusBar("PLC", "INFO", $"打印成功，写入PLC状态：prt={prt}（prtOK）");
                }
                else
                {
                    _logHelper.Log("打印", "ERROR", $"打印失败：{printResult.Message}");
                    prt = CommunicationProtocol.prtNG;
                    // 线程安全更新UI
                    BeginInvoke(new Action(() => _form1.SafeSetLbPrtCode(CommunicationProtocol.prtCodeNG)));

                    // 即使屏蔽PLC，仍写入PLC状态（若不需要可加判断：if(!ignorePlc_checkBox.Checked)）
                    WritePLCReg(prt: prt);
                    _logHelper.Log("PLC", "INFO", $"打印失败，写入PLC状态：prt={prt}（prtNG）");
                    _logHelper.Log("AutoRun", "WARN", "打印失败，终止本次自动流程");
                    return;
                }
            }
            catch (Exception ex)
            {
                // 捕获助手类未覆盖的异常（兜底）
                _logHelper.Log("打印", "ERROR", $"打印流程未预期异常：{ex.Message}");
                _logHelper.Log("打印", "ERROR", $"异常堆栈：{ex.StackTrace}");
                prt = CommunicationProtocol.prtNG;
                BeginInvoke(new Action(() => _form1.SafeSetLbPrtCode(CommunicationProtocol.prtCodeNG)));
                WritePLCReg(prt: prt);
                return;
            }

            _logHelper.Log("打印", "INFO", "打印流程完成，进入后续校验/收尾环节");
            // ... 后续逻辑（如扫描状态处理、流程结束） ...
        }

       

        /// <summary>
        /// 等待camHolder变为0（异步方式，不阻塞UI线程）
        /// </summary>
        /// <returns>是否成功等待到camHolder=0（true=成功，false=超时或被跳过）</returns>
        private async Task<bool> WaitForCamHolderZeroAsync()
        {
            // 读取初始值
            short? camHolder = ReadPlcSpecificRegister(CommunicationProtocol.camPstHolder);

            // 超时计时器（可选，避免无限等待，单位：毫秒）
            int timeoutMs = 30000; // 30秒超时
            DateTime startTime = DateTime.Now;

            while (camHolder != 0)
            {
                // 检查是否超时
                if ((DateTime.Now - startTime).TotalMilliseconds > timeoutMs)
                {
                    _logHelper.Log("打印流程", "ERROR", $"等待camHolder超时（{timeoutMs}ms），当前值: {camHolder}");
                    UpdateStatusBar("打印流程", "ERROR", $"等待1#阻挡气缸降下超时（{timeoutMs}ms），当前值: {camHolder}");
                    return false;
                }

                _logHelper.Log("打印流程", "INFO", $"等待camHolder变为0，当前值: {camHolder}，等待时间：{(DateTime.Now - startTime).Seconds}");
                UpdateStatusBar("打印流程", "INFO", $"等待1#阻挡气缸降下，等待时间：{(DateTime.Now - startTime).Seconds}");
                // 异步等待500ms（不阻塞线程）
                await Task.Delay(500);
                // 重新读取值
                camHolder = ReadPlcSpecificRegister(CommunicationProtocol.camPstHolder);
            }

            _logHelper.Log("打印流程", "INFO", "camHolder已变为0，继续执行打印");
            return true;
        }

        // 新增线程终止方法
        private void StopAutoRunThread()
        {
            // 临时取消自动流程勾选，触发线程退出循环
            bool wasAutoRunChecked = checkBox_autoMode.Checked;
            checkBox_autoMode.Checked = false;
            // 短暂延迟确保线程退出
            Task.Delay(500).Wait();
            // 恢复原状态（若需要）
            checkBox_autoMode.Checked = wasAutoRunChecked;
        }

        /// <summary>
        /// 自动模式复选框状态变更事件
        /// 核心逻辑：
        /// 1. 自动模式开启时，根据「跳过相机」状态过滤触发源（而非启停TCP/PLC监控）；
        /// 2. 保留TCP服务器始终运行（避免相机连接中断）；
        /// 3. PLC监控仅在「跳过相机」时启动，否则仅停止触发逻辑（不停止监控线程）。
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkBox_autoMode_CheckedChanged(object sender, EventArgs e)
        {
            // 更新自动模式总开关状态
            _isAutoModeEnabled = checkBox_autoMode.Checked;

            if (_isAutoModeEnabled)
            {
                // 开启自动模式：仅控制触发源过滤，不启停TCP服务器
                if (checkBox_ignoreCam.Checked)
                {
                    // 场景1：勾选跳过相机 → 启动PLC监控（响应PLC触发），TCP服务器保持运行（仅过滤触发信号）
                    StartCamMonitor(); // 启动PLC cam寄存器监控线程
                                       // 不修改TCP服务器状态（保留连接，避免相机连错）
                    _logHelper.Log("自动模式", "INFO", "【跳过相机模式】自动模式已开启，仅响应PLC触发信号，TCP服务器保持运行（过滤TCP触发）");
                }
                else
                {
                    // 场景2：未勾选跳过相机 → 停止PLC监控触发（仅保留TCP触发），TCP服务器保持运行
                    StopCamMonitor(); // 停止PLC cam寄存器监控（避免PLC误触发）
                                      // 确保TCP服务器运行（相机数据传输需要）
                    if (!checkBox_tcpServer.Checked)
                    {
                        checkBox_tcpServer.Checked = true;
                        StartTcpServer();
                    }
                    _logHelper.Log("自动模式", "INFO", "【正常模式】自动模式已开启，仅响应TCP触发信号，TCP服务器保持运行");
                }
            }
            else
            {
                // 关闭自动模式：停止PLC监控，TCP服务器由用户手动控制（不强制关闭）
                StopCamMonitor();
                ResetTriggerStatus(); // 重置触发状态标记
                _logHelper.Log("自动模式", "INFO", "自动模式已关闭，停止PLC监控，TCP服务器状态保持不变");
            }
        }

        /// <summary>
        /// 统一触发AutoRun全流程的入口（核心触发控制）
        /// 核心逻辑：
        /// 1. 自动触发需校验自动模式开关；
        /// 2. 根据「跳过相机」状态过滤触发源；
        /// 3. 双重锁防止重复触发/并发执行；
        /// </summary>
        /// <param name="isManualTrigger">是否为手动触发（手动触发忽略触发源过滤）</param>
        /// <param name="triggerSource">触发源描述（用于日志追溯，如：PLC监控/TCP服务器/手动按钮）</param>
        private void TriggerAutoRun(bool isManualTrigger = false, string triggerSource = "未知")
        {
            #region 第一步：校验自动模式（自动触发专属）
            if (!isManualTrigger && !_isAutoModeEnabled)
            {
                _logHelper.Log("触发控制", "INFO", $"[{triggerSource}] 自动模式未开启，忽略触发信号");
                return;
            }
            #endregion

            #region 第二步：过滤触发源（自动触发专属）
            if (!isManualTrigger)
            {
                bool isPlcTrigger = triggerSource.Contains("PLC"); // PLC触发源标识
                bool isTcpTrigger = triggerSource.Contains("TCP"); // TCP触发源标识

                // 规则1：跳过相机模式 → 仅允许PLC触发
                if (checkBox_ignoreCam.Checked && !isPlcTrigger)
                {
                    _logHelper.Log("触发控制", "WARN", $"[{triggerSource}] 跳过相机模式下仅允许PLC触发，忽略本次非PLC触发");
                    return;
                }

                // 规则2：正常模式（未跳过相机）→ 仅允许TCP触发
                if (!checkBox_ignoreCam.Checked && !isTcpTrigger)
                {
                    _logHelper.Log("触发控制", "WARN", $"[{triggerSource}] 正常模式下仅允许TCP触发，忽略本次非TCP触发");
                    return;
                }
            }
            #endregion

            #region 第三步：双重锁防止重复触发/并发执行
            // 第一层锁：防止同一触发源重复标记
            lock (_triggerLock)
            {
                if (_isAutoProcessTriggered)
                {
                    _logHelper.Log("触发控制", "WARN", $"[{triggerSource}] 已有流程触发标记，忽略本次重复触发");
                    return;
                }
                _isAutoProcessTriggered = true; // 标记为已触发
            }

            // 第二层锁：防止流程并发执行
            lock (_autoRunLock)
            {
                if (_isAutoRunning)
                {
                    _logHelper.Log("触发控制", "WARN", $"[{triggerSource}] 已有自动流程在执行，忽略本次触发");
                    ResetTriggerFlag(); // 重置触发标记
                    return;
                }
                _isAutoRunning = true; // 标记为流程执行中
            }
            #endregion

            #region 第四步：执行全流程（核心业务）
            try
            {
                _logHelper.Log("触发控制", "INFO", $"[{triggerSource}] 开始执行自动流程");
                ExecuteFullProcess(); // 执行AutoRun全流程（读PLC→MES→打印→校验→写结果）
            }
            catch (Exception ex)
            {
                _logHelper.Log("触发控制", "ERROR", $"[{triggerSource}] 自动流程执行异常：{ex.Message}，堆栈：{ex.StackTrace}");
            }
            finally
            {
                // 无论成功/失败，都释放状态（必须在finally中执行，避免死锁）
                lock (_autoRunLock)
                {
                    _isAutoRunning = false; // 释放流程执行状态
                }
                ResetTriggerFlag(); // 重置触发标记
                _logHelper.Log("触发控制", "INFO", $"[{triggerSource}] 自动流程执行完成，状态已释放");
            }
            #endregion
        }

        /// <summary>
        /// 重置触发标记（提取为独立方法，便于复用和维护）
        /// </summary>
        private void ResetTriggerFlag()
        {
            lock (_triggerLock)
            {
                _isAutoProcessTriggered = false;
            }
        }

        /// <summary>
        /// 重置所有触发状态（自动模式关闭时调用）
        /// </summary>
        private void ResetTriggerStatus()
        {
            lock (_triggerLock)
            {
                _isAutoProcessTriggered = false;
            }
            lock (_autoRunLock)
            {
                _isAutoRunning = false;
            }
        }

        #endregion

        #region 流程拆分
        /// <summary>
        /// 读取PLC状态（复用ReadPlcSpecificRegister，优化版）
        /// </summary>
        /// <returns>包含读取结果的元组：(是否成功, cam值, prt值, scn值)</returns>
        private (bool Success, short Cam, short Prt, short Scn) ReadPlcStatus()
        {
            // 若勾选"屏蔽PLC"，直接返回默认值（成功状态）
            if (checkBox_ignorePlc.Checked)
            {
                Console.WriteLine("ReadPlcStatus：已屏蔽PLC，返回默认值");
                return (true, -1, -1, -1);
            }

            try
            {
                // 复用ReadPlcSpecificRegister读取三个寄存器（地址来自CommunicationProtocol）
                short? cam = ReadPlcSpecificRegister(CommunicationProtocol.camRegister);
                short? prt = ReadPlcSpecificRegister(CommunicationProtocol.prtRegister);
                short? scn = ReadPlcSpecificRegister(CommunicationProtocol.scannerRegister);
                //short? camHolder = ReadPlcSpecificRegister(CommunicationProtocol.camPstHolder);

                // 检查是否有任一寄存器读取失败
                if (!cam.HasValue || !prt.HasValue || !scn.HasValue)
                {
                    Console.WriteLine("ReadPlcStatus：部分寄存器读取失败");
                    return (false, -1, -1, -1);
                }

                // 判断PLC是否未连接（三个值均为-1，根据Form2.ReadPlc的失败返回值约定）
                if (cam.Value == -1 && prt.Value == -1 && scn.Value == -1)
                {
                    Console.WriteLine("ReadPlcStatus：PLC未连接");
                    return (false, cam.Value, prt.Value, scn.Value);
                }

                // 读取成功
                Console.WriteLine($"ReadPlcStatus：读取成功 (cam:{cam}, prt:{prt}, scn:{scn})");
                return (true, cam.Value, prt.Value, scn.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ReadPlcStatus：读取异常 - {ex.Message}");
                return (false, -1, -1, -1);
            }
        }

        #endregion

        #region 校验相关
        /// <summary>
        /// 忽略校验复选框状态变更事件
        /// </summary>
        private void chkbox_ignoreCheck_checked(object sender, EventArgs e)
        {
            // 1. 先停止所有相关监控线程（避免线程冲突）
            StopAllCheckRelatedThreads();

            // 2. 更新Form1的忽略校验状态（跨线程安全）
            if (_form1.InvokeRequired)
            {
                _form1.Invoke(new Action(() => _form1.ignoreCheck = checkBox_ignoreCheck.Checked));
            }
            else
            {
                _form1.ignoreCheck = checkBox_ignoreCheck.Checked;
            }
            _form1.SafeSetLbChkCode(checkBox_ignoreCheck.Checked ? "忽略校验" : "等待校验");

            // 3. 根据忽略校验状态，启停对应监控线程
            if (checkBox_ignoreCheck.Checked)
            {   // 勾选"忽略校验"：启动scn寄存器监控线程（类似跳过相机的监控逻辑）
                _isScnMonitorRunning = true;
                _scnMonitorThread = new Thread(ScnMonitorLoop);
                _scnMonitorThread.IsBackground = true;
                _scnMonitorThread.Start();
                _logHelper.Log("忽略校验", "INFO", "勾选忽略校验，启动PLC SCN监控（PLC触发校验）");
            }

            else
            {   // 取消忽略校验：启动校验监控线程（串口触发，仅监听不主动执行）
                _isCheckMonitorRunning = true;
                _checkMonitorThread = new Thread(CheckMonitorLoop);
                _checkMonitorThread.IsBackground = true;
                _checkMonitorThread.Start();
                _logHelper.Log("忽略校验", "INFO", "取消忽略校验，启动串口校验监控（串口触发校验）");
            }
        }

        //校验线程终止方法
        private void StopAllCheckRelatedThreads()
        {
            // 停止校验监控线程（原串口触发时的线程）
            _isCheckMonitorRunning = false;
            if (_checkMonitorThread != null && _checkMonitorThread.IsAlive)
            {
                _checkMonitorThread.Join(500); // 等待线程退出（复用原有等待逻辑）
            }

            // 停止SCN监控线程（原忽略校验时的PLC线程）
            _isScnMonitorRunning = false;
            if (_scnMonitorThread != null && _scnMonitorThread.IsAlive)
            {
                _scnMonitorThread.Join(500);
            }
        }

        /// <summary>
        /// PLC SCN寄存器监控线程循环（忽略校验模式下使用）
        /// </summary>
        private void ScnMonitorLoop()
        {
            // 1. 循环入口：只要线程运行标记为true，就一直执行（线程核心循环）
            while (_isScnMonitorRunning)
            {
                try
                {
                    // 2. 读取PLC指定寄存器的值
                    // 作用：查PLC的状态，PLC必须进入校验准备模式（即scn寄存器值为特定触发值），才执行校验 
                    short? scnValue = ReadPlcSpecificRegister(CommunicationProtocol.scannerRegister);
                    // 3. 核心判断：PLC寄存器有值 且 这个值等于“触发校验”的约定值（产品已经到达校验位置）
                    if (scnValue.HasValue && scnValue.Value == CommunicationProtocol.checkIgnore)
                    {
                        // 4. 执行校验逻辑，获取校验结果
                        string checkResult = _checkHelper.ExecuteCheck();
                        // 5. 根据校验结果更新PLC（第二个参数是“是否忽略PLC”的复选框状态）
                        // 作用：把校验结果（OK/NG）写给PLC，同时判断是否要跳过PLC写入
                        _checkHelper.UpdatePlcByCheckResult(checkResult, checkBox_ignorePlc.Checked);
                    }
                }
                catch (Exception ex)
                {
                    _logHelper.Log("SCN监控", "ERROR", ex.Message);
                }
                Thread.Sleep(200); // 轮询间隔
            }
        }

        /// <summary>
        /// 校验监控线程循环-监控串口数据接收（非忽略校验模式下使用）
        /// </summary>
        private void CheckMonitorLoop()
        {
            while (_isCheckMonitorRunning)
            {
                // 取消忽略校验时：仅监听串口数据，不主动执行校验（校验由串口事件触发）
                // 原有串口接收事件（SerialPort_DataReceived）会自行触发校验，此处仅保持线程存活
                Thread.Sleep(200); // 复用原有休眠逻辑
            }
        }

        /// <summary>
        /// 串口数据接收事件处理，触发校验流程
        /// </summary>
        private void OnSerialDataReceived()
        {
            // 执行验码以及放行的功能
            if (checkBox_ignoreCheck.Checked)
            {
                return;
            }
            // 使用CheckHelper处理校验
            try
            {
                string checkResult = _checkHelper.ExecuteCheck();
                _checkHelper.UpdatePlcByCheckResult(checkResult, checkBox_ignorePlc.Checked);
                _form1.SafeSetLbChkCode(checkResult);
            }
            catch (Exception ex)
            {
                _logHelper.Log("串口校验", "ERROR", $"处理串口校验数据异常: {ex.Message}");
                _logHelper.Log("串口校验", "ERROR", $"异常堆栈: {ex.StackTrace}");
            }
        }
        #endregion

        #region PLC通信相关
        /// <summary>
        /// 连接PLC复选框状态变更事件
        /// </summary>
        private void connectPlc_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_connectPlc.Checked)
            {
                // 连接PLC
                _form2.btn_Open_Click(null, null);
                if (_form2.GetReturnCode() == "0x00000000 [HEX]")
                {
                    lable_PlcConnectStatus.Text = "PLC 已连接";
                    lable_PlcConnectStatus.ForeColor = System.Drawing.Color.Black;
                    lable_PlcConnectStatus.BackColor = System.Drawing.Color.FromArgb(114, 233, 186);
                }
                else
                {
                    checkBox_connectPlc.Checked = false;
                    lable_PlcConnectStatus.Text = "PLC 无法连接";
                    lable_PlcConnectStatus.ForeColor = System.Drawing.Color.Black;
                    lable_PlcConnectStatus.BackColor = System.Drawing.Color.FromArgb(246, 111, 81);
                }
            }
            else
            {
                // 断开PLC连接
                _form2.btn_Close_Click(null, null);
                lable_PlcConnectStatus.Text = "PLC 已断开";
                lable_PlcConnectStatus.ForeColor = System.Drawing.Color.White;
                lable_PlcConnectStatus.BackColor = System.Drawing.Color.Black;
                // 关键：停止所有可能操作PLC的线程
                StopAutoRunThread();            // 停止自动流程线程
                StopAllCheckRelatedThreads();   // 停止校验监控线程
                StopCamMonitor();               // 停止相机监控线程（若启用）
            }
        }

        /// <summary>
        /// 更新PLC寄存器值（优化版）
        /// 仅写入指定的寄存器，未指定的保持原有值
        /// </summary>
        /// <param name="cam">相机状态值（不修改传null）</param>
        /// <param name="prt">打印状态值（不修改传null）</param>
        /// <param name="scn">扫描状态值（不修改传null）</param>
        private void WritePLCReg(short? cam = null, short? prt = null, short? scn = null)
        {
            if (!checkBox_ignorePlc.Checked && checkBox_connectPlc.Checked)
            {
                try
                {
                    // 直接调用Form2的重载方法，内部会自动处理未指定参数的保留逻辑
                    _form2.SafeWritePlc(cam, prt, scn);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"更新PLC寄存器失败: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// 读取指定地址的PLC寄存器值（供监控线程使用）
        /// </summary>
        /// <param name="deviceName">寄存器地址（如"D100"）</param>
        /// <returns>成功返回short值，失败返回null</returns>
        private short? ReadPlcSpecificRegister(string deviceName)
        {
            // 新增：若未连接PLC，直接返回null
            if (!checkBox_connectPlc.Checked)
            {
                Console.WriteLine("PLC未连接，跳过读取");
                return null;
            }
            // 检查PLC连接状态和屏蔽设置
            if (checkBox_ignorePlc.Checked || !checkBox_connectPlc.Checked)
            {
                Console.WriteLine("PLC已屏蔽或未连接，跳过读取");
                return null;
            }

            try
            {
                // 调用Form2的线程安全方法
                return _form2.ReadSpecificPlcRegister(deviceName);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MainForm读取PLC寄存器失败: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// 手动读码按钮事件（来自Form1委托）
        /// </summary>
        public void btn_RetryRead_Click()
        {
            WritePLCReg(cam: CommunicationProtocol.camRetry);
        }

        /// <summary>
        /// 手动验码按钮事件（来自Form1委托）
        /// </summary>
        public void btn_RetryChk_Click()
        {
            try
            {
                _logHelper.Log("手动验码", "INFO", "用户触发手动验码");
                string checkResult = _checkHelper.ExecuteCheck();
                _checkHelper.UpdatePlcByCheckResult(checkResult, checkBox_ignorePlc.Checked);
            }
            catch (Exception ex)
            {
                _logHelper.Log("手动验码", "ERROR", $"手动验码执行失败: {ex.Message}");
                _logHelper.Log("手动验码", "ERROR", $"异常堆栈: {ex.StackTrace}");
            }
        }
        #endregion

        #region 跳过相机功能
        /// <summary>
        /// 跳过相机复选框状态变更事件
        /// 联动自动模式：切换跳过相机时，同步更新触发源
        /// </summary>
        /// <param name="sender">触发控件</param>
        /// <param name="e">事件参数</param>
        private void chkBox_ignoreCam_CheckedChanged(object sender, EventArgs e)
        {
            // 清空校验码
            if (_form1.InvokeRequired)
            {
                _form1.Invoke(new Action(_form1.ClearVericode));
            }
            else
            {
                _form1.ClearVericode();
            }

            // 如果当前处于自动模式，重新触发自动模式逻辑，同步触发源
            if (_isAutoModeEnabled)
            {
                // 触发自动模式逻辑重计算
                chkBox_autoMode_CheckedChanged(null, EventArgs.Empty);
            }
            else
            {
                // 非自动模式下，仅控制PLC监控线程（TCP由用户手动控制）
                if (checkBox_ignoreCam.Checked)
                {
                    StartCamMonitor();
                    _logHelper.Log("跳过相机", "INFO", "非自动模式下，跳过相机已勾选，启动PLC监控（仅监听，不触发流程）");
                }
                else
                {
                    StopCamMonitor();
                    _logHelper.Log("跳过相机", "INFO", "非自动模式下，跳过相机已取消，停止PLC监控");
                }
            }
        }

        /// <summary>
        /// 启动相机监控线程
        /// 监控PLC寄存器，当cam值为1时触发自动运行流程
        /// </summary>
        private void StartCamMonitor()
        {
            lock (_camMonitorLock)
            {
                if (_isCamMonitorRunning)
                    return;

                _isCamMonitorRunning = true;
                _camMonitorThread = new Thread(CamMonitorLoop);
                _camMonitorThread.IsBackground = true;
                _camMonitorThread.Start();
                Console.WriteLine("相机监控线程已启动");
            }
        }

        /// <summary>
        /// 停止相机监控线程
        /// </summary>
        private void StopCamMonitor()
        {
            lock (_camMonitorLock)
            {
                if (!_isCamMonitorRunning)
                    return;

                _isCamMonitorRunning = false;
            }

            // 等待线程终止
            if (_camMonitorThread != null && _camMonitorThread.IsAlive)
            {
                _camMonitorThread.Join(1000);// 等待线程退出
                if (_camMonitorThread.IsAlive)
                {
                    Console.WriteLine("相机监控线程未能正常终止");
                }
                else
                {
                    Console.WriteLine("相机监控线程已停止");
                }
                _camMonitorThread = null;
            }
        }

        /// <summary>
        /// 相机监控循环
        /// 持续检测PLC的cam值，满足条件时触发自动运行
        /// </summary>
        private void CamMonitorLoop()
        {
            while (_isCamMonitorRunning)
            {
                try
                {
                    // 检查PLC连接状态
                    if (!checkBox_connectPlc.Checked)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    // 使用新方法读取指定寄存器（相机允许信号）
                    var camValue = ReadPlcSpecificRegister(CommunicationProtocol.camRegister);

                    // 检测到触发条件
                    if (camValue.HasValue && camValue.Value == CommunicationProtocol.camAllow)
                    {
                        _logHelper.Log("PLC监控", "INFO", $"检测到cam寄存器值为{camValue.Value}，符合触发条件");
                        // 触发自动流程，标记触发源为「PLC监控」
                        BeginInvoke(new Action(() => TriggerAutoRun(isManualTrigger: false, "PLC监控")));
                        Thread.Sleep(2000); // 防抖动：避免短时间重复读取触发
                    }
                    else
                    {
                        Thread.Sleep(500); // 非触发状态，降低轮询频率
                    }
                }
                catch (Exception ex)
                {
                    _logHelper.Log("PLC监控", "ERROR", $"PLC监控线程异常：{ex.Message}");
                    Thread.Sleep(1000);
                }
            }
        }
        #endregion

        #region TCP服务器功能
        /// <summary>
        /// TCP接收复选框状态变更事件
        /// </summary>
        private void tcpServer_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_tcpServer.Checked)
            {
                StartTcpServer();
            }
            else
            {
                StopTcpServer();
            }
        }

        /// <summary>
        /// 启动TCP服务器（接收相机数据）
        /// </summary>
        private void StartTcpServer()
        {
            if (_tcpServer != null && _tcpServer.IsRunning)
                return;

            try
            {
                _tcpServer = new TCPServer();
                // 订阅事件
                _tcpServer.DataReceived += OnTcpDataReceived;
                _tcpServer.ClientConnected += OnClientConnected;
                _tcpServer.ClientDisconnected += OnClientDisconnected;

                // 启动TCP服务器（IP/端口从配置读取，示例值可替换）
                string tcpIp = _localAddr.ToString(); // 监听所有网卡
                int tcpPort = _localPortNum;       // 自定义端口
                _tcpServer.Start(tcpIp, tcpPort);
                _logHelper.Log("TCP服务器", "INFO", $"TCP服务器已启动，监听[{tcpIp}:{tcpPort}]");
            }
            catch (Exception ex)
            {
                _logHelper.Log("TCP服务器", "ERROR", $"启动TCP服务器失败：{ex.Message}");
                checkBox_tcpServer.Checked = false;
                _tcpServer = null;
            }
        }

        /// <summary>
        /// 停止TCP服务器
        /// </summary>
        private void StopTcpServer()
        {
            if (_tcpServer == null)
                return;
            if (_tcpServer != null)
            {
                try
                {
                    // 取消订阅
                    _tcpServer.DataReceived -= OnTcpDataReceived;
                    _tcpServer.ClientConnected -= OnClientConnected;
                    _tcpServer.ClientDisconnected -= OnClientDisconnected;

                    _tcpServer.Stop();
                    _logHelper.Log("TCP服务器", "INFO", "TCP服务器已停止");
                }
                catch (Exception ex)
                {
                    _logHelper.Log("TCP服务器", "ERROR", $"停止TCP服务器异常：{ex.Message}");
                }
                finally
                {
                    _tcpServer = null;
                }

            }
        }

        // 添加客户端连接事件处理
        private void OnClientConnected(string clientInfo)
        {
            _logHelper.Log("TCP服务器", "INFO", $"客户端[{clientInfo}]已连接");
            // 可以在这里添加UI更新代码
            BeginInvoke(new Action(() =>
            {
                // 更新UI显示客户端连接状态
            }));
        }

        // 添加客户端断开事件处理
        private void OnClientDisconnected(string clientInfo)
        {
            _logHelper.Log("TCP服务器", "INFO", $"客户端[{clientInfo}]已断开");
            // 可以在这里添加UI更新代码
            BeginInvoke(new Action(() =>
            {
                // 更新UI显示客户端断开状态
            }));
        }

        /// <summary>
        /// TCP数据接收事件处理（相机数据通过TCP传输）
        /// </summary>
        /// <param name="clientInfo">客户端IP+端口</param>
        /// <param name="data">接收的原始数据</param>
        private void OnTcpDataReceived(string clientInfo, string data)
        {
            // 1. 日志记录原始数据
            _logHelper.Log("TCP服务器", "INFO", $"收到[{clientInfo}]数据：{data}");

            // 2. 更新UI（显示接收的数据）
            BeginInvoke(new Action(() =>
            {
                //_form1.txtBox_veriCodeHistory.AppendText($"{DateTime.Now:HH:mm:ss} - {data}\r\n");
                _form1.txtBox_veriCodeHistory.AppendText($"{data}\r\n");
            }));

            // 3. 解析产品ID（防重复触发用）
            string productId = ParseProductIdFromTcpData(data);
            if (string.IsNullOrEmpty(productId))
            {
                _logHelper.Log("TCP服务器", "ERROR", "未解析到有效产品ID，忽略触发");
                return;
            }

            // 4. 触发自动流程，标记触发源为「TCP服务器」
            TriggerAutoRun(isManualTrigger: false, "TCP服务器");

            // 5. 可选：向客户端发送确认回执
            _tcpServer.SendToClient(clientInfo, $"服务器已接收并处理数据：{data}");
        }

        /// <summary>
        /// 从TCP数据中解析产品唯一标识（纯ID格式）
        /// </summary>
        /// <param name="tcpData">TCP接收的原始字符串</param>
        /// <returns>产品ID（空字符串表示解析失败）</returns>
        private string ParseProductIdFromTcpData(string tcpData)
        {
            if (string.IsNullOrWhiteSpace(tcpData))
            {
                _logHelper.Log("TCP解析", "WARN", "TCP数据为空，无法解析产品ID");
                return string.Empty;
            }

            // 去除首尾空格（防止数据带空白符）
            string productId = tcpData.Trim();

            // 简单校验（如果收到的为NoRead，则通知camNG）
            if (productId.StartsWith("NoRead"))
            {
                _logHelper.Log("TCP解析", "ERROR", $"无效产品ID格式：{tcpData}");
                return string.Empty;
            }
            else
            {
                return productId;
            }
        }

        #endregion

        /// <summary>
        /// 主窗体关闭事件
        /// </summary>
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopCamMonitor(); // 确保相机监控线程停止
            StopAllCheckRelatedThreads(); // 停止校验相关线程
        }

        /// <summary>
        /// 定时器1，用于刷新状态栏中的时间显示
        /// </summary>
        private void timer1_Tick(object sender, EventArgs e)
        {
            this.statusTime.Text = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// 线程安全更新状态栏（分栏显示module/level/message/时间）
        /// </summary>
        /// <param name="module">模块名（如"触发控制"、"MES通信"）</param>
        /// <param name="level">日志级别（INFO/WARN/ERROR）</param>
        /// <param name="message">具体消息内容</param>
        private void UpdateStatusBar(string module, string level, string message)
        {
            // 非UI线程时通过Invoke切换到UI线程
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<string, string, string>(UpdateStatusBar), module, level, message);
                return;
            }

            // 1. 更新模块
            statusModule.Text = module;

            // 2. 更新级别（带颜色区分）
            statusLevel.Text = level;
            Color levelColor = Color.Black; // 默认颜色
            switch (level)
            {
                case "ERROR":
                    levelColor = Color.Red;
                    break;
                case "WARN":
                    levelColor = Color.Orange;
                    break;
                case "INFO":
                    levelColor = Color.Green;
                    break;
                default:
                    levelColor = Color.Black;
                    break;
            }
            statusLevel.ForeColor = levelColor;

            // 3. 更新消息（超长时自动截断，避免状态栏溢出）
            const int maxMessageLength = 50; // 可根据界面调整
            statusMessage.Text = message.Length > maxMessageLength
                ? message.Substring(0, maxMessageLength) + "..."
                : message;

            statusStrip_mainForm.Refresh();
        }
    }
}