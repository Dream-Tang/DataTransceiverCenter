using Newtonsoft.Json;
using System;
using System.IO;
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
        private Thread _camMonitorThread;               // 相机监控线程
        private bool _isCamMonitorRunning;            // 相机监控线程运行状态
        private readonly object _camMonitorLock = new object(); // 线程同步锁
        #endregion

        #region 配置接口依赖
        private IIniConfigurable _iniLoadForm;   // Form1配置加载接口
        private IJsonConfigurable _jsonLoadForm; // Form3配置加载接口
        private IIniSavable _iniSaveForm;        // Form1配置保存接口
        private IJsonSavable _jsonSaveForm;      // Form3配置保存接口
        #endregion

        #region TCP服务器相关
        private TcpListener _server;               // TCP服务器实例
        private bool _isTcpServerRunning;          // TCP服务器运行状态
        private Thread _tcpListenerThread;         // TCP监听线程
        #endregion

        // 新增校验线程标志位
        private bool _isCheckMonitorRunning = false;
        // 忽略校验，scn监控线程标志位
        private bool _isScnMonitorRunning = false;

        // 在mainForm类中添加CheckHelper实例变量
        private CheckHelper _checkHelper;

        // 自动流程 状态标签
        public enum ProcessStatus
        {
            Wait,       // 等待触发
            Ready,      // 准备就绪
            Working,    // 执行中
            Complete,   // 完成
            Exception   // 异常
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
            _form1 = new Form1();
            _form2 = new Form2();
            _form3 = new Form3();

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
            _form1.TextUpdated += OnForm1TextChanged;
            _form1.RequestMesRootData += OnForm1RequestMesRootData;
            _form1.SerialDataReceived += OnSerialDataReceived;
            _form1.PanelIDgot += CamOK;
            this.FormClosing += mainForm_FormClosing;

            // 默认显示工作流程窗体
            ShowForm(_form1);
        }

        /// <summary>
        /// 主窗体加载事件
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + "  V" + Application.ProductVersion;

            // 绑定Form1的重试按钮事件
            _form1.btnRetryRead += btn_RetryRead_Click;
            _form1.btnRetryChk += btn_RetryChk_Click;

            // 初始化校验助手类
            _checkHelper = new CheckHelper(_form1,_form2,
                                            new LoggerAdapter()  // 这里使用CheckHelper中定义的LoggerAdapter
                                          );

            // 可选：自定义日志配置
            LogHelper.Instance.SetLogConfig(
                logFilePath: "AutoRun.log",
                maxLogSize: 5 * 1024 * 1024, // 5MB
                maxHistoryLogs: 10
            );
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
            if (ignoreCam_checkBox.Checked)
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
            if (ignoreCam_checkBox.Checked)
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
            if (!File.Exists(iniFilePath))
            {
                MessageBox.Show($"ini配置文件不存在：{iniFilePath}");
                return;
            }

            try
            {
                var ini = new IniFile(iniFilePath);
                // 加载复选框状态
                ignoreCam_checkBox.Checked = ini.ReadBoolean("ignoreCam", "Checkbox", false);
                ignoreCheck_checkBox.Checked = ini.ReadBoolean("ignoreCheck", "Checkbox", false);
                ignorePlc_checkBox.Checked = ini.ReadBoolean("ignorePlc", "Checkbox", false);
                connectPlc_checkBox.Checked = ini.ReadBoolean("connectPlc", "Checkbox", false);
                chkBox_autoMode.Checked = ini.ReadBoolean("autoRun", "Checkbox", false);
                tcpServer_checkBox.Checked = ini.ReadBoolean("tcpServer", "Checkbox", false);

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
                form.Invoke(new Action(() => _iniSaveForm.SaveIni(iniFile)));
            }
            else
            {
                _iniSaveForm.SaveIni(iniFile);
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
                var ini = new IniFile(iniFilePath);
                // 保存复选框状态
                ini.Write("ignoreCam", ignoreCam_checkBox.Checked.ToString(), "Checkbox");
                ini.Write("ignoreCheck", ignoreCheck_checkBox.Checked.ToString(), "Checkbox");
                ini.Write("ignorePlc", ignorePlc_checkBox.Checked.ToString(), "Checkbox");
                ini.Write("connectPlc", connectPlc_checkBox.Checked.ToString(), "Checkbox");
                ini.Write("autoRun", chkBox_autoMode.Checked.ToString(), "Checkbox");
                ini.Write("tcpServer", tcpServer_checkBox.Checked.ToString(), "Checkbox");

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
            LogHelper.Instance.Log("PLC", "INFO", "开始读取PLC状态");
            var plcResult = ReadPlcStatus();
            if (!plcResult.Success)
            {
                // 读取失败时的处理（保持原逻辑：终止流程）
                consoleInfo = "PLC读取失败，终止本次自动流程";
                LogHelper.Instance.Log("PLC", "ERROR", consoleInfo);
                LogHelper.Instance.Log("PLC", "ERROR", $"读取失败详情：cam={plcResult.Cam}, prt={plcResult.Prt}, scn={plcResult.Scn}");
                return;
            }
            // 读取成功，赋值给变量
            cam = plcResult.Cam;
            prt = plcResult.Prt;
            scn = plcResult.Scn;
            LogHelper.Instance.Log("PLC", "INFO", $"PLC状态读取成功：cam={cam}, prt={prt}, scn={scn}");

            // 若屏蔽PLC，已在ReadPlcStatus中返回(-1,-1,-1)，无需额外处理

            // ========== 2. 重构MES通信部分 ==========
            // 步骤2.1：简化MES参数（仅保留必要的配置获取）
            string postUrl = "";
            MsePostData mesPostData = null; // 替换原postJson，直接用实体类
            string postJson = ""; // 保留原postJson变量，用于打印请求参数

            try
            {
                // 从Form3获取MES配置（保留原有配置来源，仅修改数据类型）
                LogHelper.Instance.Log("MES", "INFO", "开始获取MES配置信息");
                postUrl = _form3.MesPostRoot.MesUrl;
                mesPostData = _form3.MesPostRoot.MesData; // 直接获取实体类，无需手动序列化
                // 保留原有序列化逻辑，用于打印请求JSON
                postJson = JsonConvert.SerializeObject(mesPostData, Formatting.Indented);

                LogHelper.Instance.Log("MES", "INFO", $"MES配置获取成功 - URL：{postUrl}");
                LogHelper.Instance.Log("MES", "INFO", $"MES请求参数（JSON）：{Environment.NewLine}{postJson}");

                // 新增：更新“发送消息”到Form1的txtBox_postData（线程安全）
                BeginInvoke(new Action(() =>
                {
                    _form1.txtBox_postData.Text = $"POST发送:\r\n{JsonConvert.SerializeObject(mesPostData)}";
                }));
            }
            catch (Exception ex)
            {
                consoleInfo = $"Mes设置未导入: {ex.Message}";
                LogHelper.Instance.Log("MES", "ERROR", consoleInfo);
                LogHelper.Instance.Log("MES", "ERROR", $"异常堆栈：{ex.StackTrace}"); // 保留异常堆栈
                // 配置错误直接终止流程
                return;
            }
            // 步骤2.2：调用MES通信助手类（替换原Task t1）
            LogHelper.Instance.Log("MES", "INFO", "开始发送MES POST请求");
            //var mesResult = await MesCommunicationHelper.Instance.SendMesRequestAsync(postUrl, mesPostData);
            var mesResult = await MesCommunicationHelper.Instance.SendMesRequestWithRawAsync(postUrl, mesPostData);
            // 调用后查看日志（定位问题）
            Console.WriteLine($"最终结果：Success={mesResult.Success}，Message={mesResult.Message}");
            Console.WriteLine($"原始响应：{mesResult.RawResponse}");

            // 步骤2.3：处理MES响应结果（复用原有业务逻辑，仅适配新的返回值）
            if (mesResult.Success)
            {
                // MES通信成功（逻辑与原一致）
                consoleInfo = $"已收到MES回复的数据：{mesResult.RawResponse}";
                LogHelper.Instance.Log("MES", "INFO", consoleInfo);
                LogHelper.Instance.Log("MES", "INFO", $"MES响应详情：{mesResult.Message}");
                WritePLCReg(cam: CommunicationProtocol.camOK);

                // 新增1：更新“接收消息”到Form1的txtBox_responseData（线程安全）
                BeginInvoke(new Action(() =>
                {
                    _form1.txtBox_responseData.Text = $"Mes响应:\r\n{mesResult.RawResponse}";
                }));

                // 新增：生成二维码并更新标签
                // MES返回的是JSON对象，需先解析

                //var mesDataObj = JsonConvert.DeserializeObject<MesData2>(mesResult.Data);
                string qrCodeContent = mesResult.Data; // 需要显示panelId字段

                BeginInvoke(new Action(() =>
                {
                    _form1.SetLbReadCode(CommunicationProtocol.readCodeOK);
                    LogHelper.Instance.Log("UI", "INFO", "更新UI：条码读取状态设为OK");

                    // 1. 生成二维码并显示到pictureBox（假设使用pictureBox1显示）
                    _form1.pictureBox1.Image = Form1.SetBarCode128(qrCodeContent);

                    // 2. 将数值显示到lastPrtCode_label
                    _form1.lastPrtCode_label.Text = qrCodeContent;

                    // 3. 可选：同步更新打印码文本框
                    _form1.txtBox_prtCode.Text = qrCodeContent;

                }));

                cam = CommunicationProtocol.camOK;
                LogHelper.Instance.Log("PLC", "INFO", $"写入PLC状态：cam={cam}（camOK）");
            }
            else
            {
                // MES通信失败（逻辑与原一致，仅替换错误信息来源）
                consoleInfo = mesResult.Message;
                LogHelper.Instance.Log("MES", "ERROR", $"MES通信失败：{consoleInfo}");
                WritePLCReg(cam: CommunicationProtocol.camNG);
                BeginInvoke(new Action(() =>
                {
                    _form1.SetLbReadCode(CommunicationProtocol.readCodeNG);
                    LogHelper.Instance.Log("UI", "INFO", "更新UI：条码读取状态设为NG");
                }));

                cam = CommunicationProtocol.camNG;
                LogHelper.Instance.Log("PLC", "INFO", $"写入PLC状态：cam={cam}（camNG）");
                LogHelper.Instance.Log("AutoRun", "WARN", "MES通信失败，终止本次自动流程");
                return; // 失败终止后续流程
            }


            // ========== 打印模块改造核心 ==========
            LogHelper.Instance.Log("打印", "INFO", "进入打印流程，开始前置检查");
            // 标记是否需要等待PLC就绪（屏蔽PLC开关）
            bool needWaitPlcReady = !ignorePlc_checkBox.Checked;
            if (needWaitPlcReady)
            {
                LogHelper.Instance.Log("打印", "INFO", "屏蔽PLC未勾选，开始等待打印机就绪信号（PLC prtReady）");
                prt = plcResult.Prt; // 复用之前读取的PLC状态，避免重复调用

                // 步骤1：等待打印机就绪（原有逻辑保留）
                while (prt != CommunicationProtocol.prtReady)
                {
                    // 刷新PLC状态（复用ReadPlcStatus，保持逻辑统一）
                    var plcRefreshResult = ReadPlcStatus();
                    if (!plcRefreshResult.Success)
                    {
                        LogHelper.Instance.Log("打印", "ERROR", "等待打印机就绪时PLC读取失败，终止打印流程");
                        prt = CommunicationProtocol.prtNG;
                        WritePLCReg(prt: prt);
                        BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG)));
                        return;
                    }
                    prt = plcRefreshResult.Prt;
                    cam = plcRefreshResult.Cam;
                    scn = plcRefreshResult.Scn;

                    LogHelper.Instance.Log("打印", "WARN", $"打印机未就绪，当前PLC状态：prt={prt}，等待500ms后重试");
                    await Task.Delay(500);
                }
                LogHelper.Instance.Log("打印", "INFO", "打印机就绪信号已获取（prt=prtReady）");
            }
            else
            {
                // 步骤1（屏蔽PLC）：跳过等待，直接标记为“逻辑就绪”
                LogHelper.Instance.Log("打印", "WARN", "屏蔽PLC已勾选，跳过打印机就绪信号校验");
                prt = CommunicationProtocol.prtReady; // 强制设为就绪，推进流程
            }

            // 步骤2：调用PrintingHelper执行打印（无论是否屏蔽PLC，都执行打印）
            try
            {
                LogHelper.Instance.Log("打印", "INFO", "开始调用打印助手类执行打印");
                // 获取Form1的打印配置（已暴露属性）
                string zplTemplatePath = _form1.ZplTemplatePath;
                string printerPath = _form1.PrintName;
                string printCode = mesResult.Data; // MES返回的打印内容

                // 异步调用打印助手类
                LogHelper.Instance.Log("打印触发", "INFO", "准备调用PrintingHelper.ExecutePrintAsync");
                var printResult = await PrintingHelper.Instance.ExecutePrintAsync(
                    printCode: printCode,
                    zplTemplatePath: _form1.ZplTemplatePath,  // 直接使用Form1暴露的模板路径属性
                    printerPath: printerPath
                );

                // 步骤3：处理打印结果（统一逻辑+标准化日志）
                if (printResult.Success)
                {
                    LogHelper.Instance.Log("打印", "INFO", $"打印成功：{printResult.Message}");
                    prt = CommunicationProtocol.prtOK;
                    // 线程安全更新UI
                    BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeOK)));

                    // 即使屏蔽PLC，仍写入PLC状态（若不需要可加判断：if(!ignorePlc_checkBox.Checked)）
                    WritePLCReg(prt: prt);
                    LogHelper.Instance.Log("PLC", "INFO", $"打印成功，写入PLC状态：prt={prt}（prtOK）");
                }
                else
                {
                    LogHelper.Instance.Log("打印", "ERROR", $"打印失败：{printResult.Message}");
                    prt = CommunicationProtocol.prtNG;
                    // 线程安全更新UI
                    BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG)));

                    // 即使屏蔽PLC，仍写入PLC状态（若不需要可加判断：if(!ignorePlc_checkBox.Checked)）
                    WritePLCReg(prt: prt);
                    LogHelper.Instance.Log("PLC", "INFO", $"打印失败，写入PLC状态：prt={prt}（prtNG）");
                    LogHelper.Instance.Log("AutoRun", "WARN", "打印失败，终止本次自动流程");
                    return;
                }
            }
            catch (Exception ex)
            {
                // 捕获助手类未覆盖的异常（兜底）
                LogHelper.Instance.Log("打印", "ERROR", $"打印流程未预期异常：{ex.Message}");
                LogHelper.Instance.Log("打印", "ERROR", $"异常堆栈：{ex.StackTrace}");
                prt = CommunicationProtocol.prtNG;
                BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG)));
                WritePLCReg(prt: prt);
                return;
            }

            LogHelper.Instance.Log("打印", "INFO", "打印流程完成，进入后续校验/收尾环节");
            // ... 后续逻辑（如扫描状态处理、流程结束） ...
        }

        // 新增线程终止方法
        private void StopAutoRunThread()
        {
            // 临时取消自动流程勾选，触发线程退出循环
            bool wasAutoRunChecked = chkBox_autoMode.Checked;
            chkBox_autoMode.Checked = false;
            // 短暂延迟确保线程退出
            Task.Delay(500).Wait();
            // 恢复原状态（若需要）
            chkBox_autoMode.Checked = wasAutoRunChecked;
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
            _isAutoModeEnabled = chkBox_autoMode.Checked;

            if (_isAutoModeEnabled)
            {
                // 开启自动模式：仅控制触发源过滤，不启停TCP服务器
                if (ignoreCam_checkBox.Checked)
                {
                    // 场景1：勾选跳过相机 → 启动PLC监控（响应PLC触发），TCP服务器保持运行（仅过滤触发信号）
                    StartCamMonitor(); // 启动PLC cam寄存器监控线程
                                       // 不修改TCP服务器状态（保留连接，避免相机连错）
                    LogHelper.Instance.Log("自动模式", "INFO", "【跳过相机模式】自动模式已开启，仅响应PLC触发信号，TCP服务器保持运行（过滤TCP触发）");
                }
                else
                {
                    // 场景2：未勾选跳过相机 → 停止PLC监控触发（仅保留TCP触发），TCP服务器保持运行
                    StopCamMonitor(); // 停止PLC cam寄存器监控（避免PLC误触发）
                                      // 确保TCP服务器运行（相机数据传输需要）
                    if (!tcpServer_checkBox.Checked)
                    {
                        tcpServer_checkBox.Checked = true;
                        StartTcpServer();
                    }
                    LogHelper.Instance.Log("自动模式", "INFO", "【正常模式】自动模式已开启，仅响应TCP触发信号，TCP服务器保持运行");
                }
            }
            else
            {
                // 关闭自动模式：停止PLC监控，TCP服务器由用户手动控制（不强制关闭）
                StopCamMonitor();
                ResetTriggerStatus(); // 重置触发状态标记
                LogHelper.Instance.Log("自动模式", "INFO", "自动模式已关闭，停止PLC监控，TCP服务器状态保持不变");
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
                LogHelper.Instance.Log("触发控制", "INFO", $"[{triggerSource}] 自动模式未开启，忽略触发信号");
                return;
            }
            #endregion

            #region 第二步：过滤触发源（自动触发专属）
            if (!isManualTrigger)
            {
                bool isPlcTrigger = triggerSource.Contains("PLC"); // PLC触发源标识
                bool isTcpTrigger = triggerSource.Contains("TCP"); // TCP触发源标识

                // 规则1：跳过相机模式 → 仅允许PLC触发
                if (ignoreCam_checkBox.Checked && !isPlcTrigger)
                {
                    LogHelper.Instance.Log("触发控制", "WARN", $"[{triggerSource}] 跳过相机模式下仅允许PLC触发，忽略本次非PLC触发");
                    return;
                }

                // 规则2：正常模式（未跳过相机）→ 仅允许TCP触发
                if (!ignoreCam_checkBox.Checked && !isTcpTrigger)
                {
                    LogHelper.Instance.Log("触发控制", "WARN", $"[{triggerSource}] 正常模式下仅允许TCP触发，忽略本次非TCP触发");
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
                    LogHelper.Instance.Log("触发控制", "WARN", $"[{triggerSource}] 已有流程触发标记，忽略本次重复触发");
                    return;
                }
                _isAutoProcessTriggered = true; // 标记为已触发
            }

            // 第二层锁：防止流程并发执行
            lock (_autoRunLock)
            {
                if (_isAutoRunning)
                {
                    LogHelper.Instance.Log("触发控制", "WARN", $"[{triggerSource}] 已有自动流程在执行，忽略本次触发");
                    ResetTriggerFlag(); // 重置触发标记
                    return;
                }
                _isAutoRunning = true; // 标记为流程执行中
            }
            #endregion

            #region 第四步：执行全流程（核心业务）
            try
            {
                LogHelper.Instance.Log("触发控制", "INFO", $"[{triggerSource}] 开始执行自动流程");
                ExecuteFullProcess(); // 执行AutoRun全流程（读PLC→MES→打印→校验→写结果）
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Log("触发控制", "ERROR", $"[{triggerSource}] 自动流程执行异常：{ex.Message}，堆栈：{ex.StackTrace}");
            }
            finally
            {
                // 无论成功/失败，都释放状态（必须在finally中执行，避免死锁）
                lock (_autoRunLock)
                {
                    _isAutoRunning = false; // 释放流程执行状态
                }
                ResetTriggerFlag(); // 重置触发标记
                LogHelper.Instance.Log("触发控制", "INFO", $"[{triggerSource}] 自动流程执行完成，状态已释放");
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
            if (ignorePlc_checkBox.Checked)
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
            // 先停止已有线程（包括原校验线程和新增的scn监控线程）
            _isCheckMonitorRunning = false;
            _isScnMonitorRunning = false; // 新增：停止scn监控
            Task.Delay(500).Wait();
            // 更新Form1的忽略校验状态
            _form1.ignoreCheck = ignoreCheck_checkBox.Checked;

            // 勾选"忽略校验"：启动scn寄存器监控线程（类似跳过相机的监控逻辑）
            if (ignoreCheck_checkBox.Checked)
            {
                _isScnMonitorRunning = true;
                var scnMonitorTask = Task.Run(async () =>
                {
                    while (_isScnMonitorRunning)
                    {
                        // 监控scn寄存器，checkIgnore（1）时触发校验
                        var scnValue = ReadPlcSpecificRegister(CommunicationProtocol.scannerRegister);
                        if (scnValue.HasValue && scnValue.Value == CommunicationProtocol.checkIgnore)
                        {
                            Console.WriteLine($"忽略校验模式：检测到scn={scnValue.Value}，触发强制校验");
                            // 执行校验（此时会返回OK）并写入PLC
                            if (this.InvokeRequired)
                            {
                                this.Invoke(new Action(() =>
                                {
                                    string checkResult = _checkHelper.ExecuteCheck();
                                    _checkHelper.UpdatePlcByCheckResult(checkResult, ignorePlc_checkBox.Checked);
                                }));
                            }
                            else
                            {
                                string checkResult = _checkHelper.ExecuteCheck();
                                _checkHelper.UpdatePlcByCheckResult(checkResult, ignorePlc_checkBox.Checked);
                            }
                            // 延迟避免短时间重复触发
                            await Task.Delay(1000);
                        }
                        await Task.Delay(500); // 监控间隔
                    }
                });
            }

            // 未勾选"忽略校验"：启动校验监控线程
            else
            {
                _isCheckMonitorRunning = true;
                // 启动校验监控线程
                var t5 = Task.Run(async () =>
                {
                    while (_isCheckMonitorRunning)  // 使用标志位控制
                    {
                        _form1.ignoreCheck = false;
                        if (_form1.seriStatus == Form1.STATUS_READY)
                        {
                            // 使用CheckHelper执行校验逻辑
                            BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    string checkResult = _checkHelper.ExecuteCheck();
                                    _checkHelper.UpdatePlcByCheckResult(checkResult, ignorePlc_checkBox.Checked);
                                }
                                catch (Exception ex)
                                {
                                    LogHelper.Instance.Log("校验线程", "ERROR", $"校验任务执行异常: {ex.Message}");
                                    LogHelper.Instance.Log("校验线程", "ERROR", $"异常堆栈: {ex.StackTrace}");
                                }
                            }));
                            _form1.seriStatus = Form1.STATUS_WAIT;
                        }
                        await Task.Delay(500);
                    }
                });
            }
        }

        /// <summary>
        /// 校验任务：处理校验结果并写入PLC
        /// </summary>
        private void t5CheckTask()
        {
            short scn = -1;
            string checkResult = "";
            try
            {
                // 无论是否屏蔽校验，都获取校验结果（此时CheckScnPrtCode会返回OK）
                if (_form1.InvokeRequired)
                {
                    _form1.Invoke(new Action(() =>
                    {
                        checkResult = _form1.GetCheckResult(); // 屏蔽时已强制为OK
                    }));
                }
                else
                {
                    checkResult = _form1.GetCheckResult();
                }

                // 根据结果设置scn值（屏蔽时checkResult已为OK）
                if (checkResult == "OK")
                {
                    scn = CommunicationProtocol.checkOK;
                    Console.WriteLine("task t5：校验结果OK（含屏蔽强制OK）");
                }
                else if (checkResult == "NG")
                {
                    scn = CommunicationProtocol.checkNG;
                    Console.WriteLine("task t5：校验结果NG");
                }
                else
                {
                    scn = CommunicationProtocol.checkIgnore;
                    Console.WriteLine("task t5：未校验");
                    return;
                }

                // 写入PLC（即使屏蔽校验，只要未勾选"忽略PLC"，就执行写入）
                if (!ignorePlc_checkBox.Checked)
                {
                    // 仅更新scn寄存器，复用重载方法减少通信
                    if (_form2.InvokeRequired)
                    {
                        _form2.Invoke(new Action(() =>
                            _form2.SafeWritePlc(scn: scn))); // 只传scn，其他保持原值
                    }
                    else
                    {
                        _form2.SafeWritePlc(scn: scn);
                    }
                    Console.WriteLine($"task t5：已发送校验结果{checkResult}给PLC（含屏蔽状态）");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"校验任务异常: {ex.Message}");
            }
            finally
            {
                // 重置状态（保持原逻辑）
                if (_form1.InvokeRequired)
                {
                    _form1.Invoke(new Action(() => _form1.seriStatus = Form1.STATUS_WAIT));
                }
                else
                {
                    _form1.seriStatus = Form1.STATUS_WAIT;
                }
            }
        }

        // 新增校验线程终止方法
        private void StopCheckMonitorThread()
        {
            _isCheckMonitorRunning = false;
        }

        /// <summary>
        /// 串口数据接收事件处理
        /// </summary>
        private void OnSerialDataReceived()
        {
            Console.WriteLine("串口接收到验码数据");
            // 使用CheckHelper处理校验
            try
            {
                string checkResult = _checkHelper.ExecuteCheck();
                _checkHelper.UpdatePlcByCheckResult(checkResult, ignorePlc_checkBox.Checked);
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Log("串口校验", "ERROR", $"处理串口校验数据异常: {ex.Message}");
                LogHelper.Instance.Log("串口校验", "ERROR", $"异常堆栈: {ex.StackTrace}");
            }
        }
        #endregion

        #region PLC通信相关
        /// <summary>
        /// 连接PLC复选框状态变更事件
        /// </summary>
        private void connectPlc_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (connectPlc_checkBox.Checked)
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
                    connectPlc_checkBox.Checked = false;
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
                StopAutoRunThread();       // 停止自动流程线程
                StopCheckMonitorThread();  // 停止校验监控线程
                StopCamMonitor();          // 停止相机监控线程（若启用）
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
            if (!ignorePlc_checkBox.Checked && connectPlc_checkBox.Checked)
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
            if (!connectPlc_checkBox.Checked)
            {
                Console.WriteLine("PLC未连接，跳过读取");
                return null;
            }
            // 检查PLC连接状态和屏蔽设置
            if (ignorePlc_checkBox.Checked || !connectPlc_checkBox.Checked)
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
                LogHelper.Instance.Log("手动验码", "INFO", "用户触发手动验码");
                string checkResult = _checkHelper.ExecuteCheck();
                _checkHelper.UpdatePlcByCheckResult(checkResult, ignorePlc_checkBox.Checked);
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Log("手动验码", "ERROR", $"手动验码执行失败: {ex.Message}");
                LogHelper.Instance.Log("手动验码", "ERROR", $"异常堆栈: {ex.StackTrace}");
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
                if (ignoreCam_checkBox.Checked)
                {
                    StartCamMonitor();
                    LogHelper.Instance.Log("跳过相机", "INFO", "非自动模式下，跳过相机已勾选，启动PLC监控（仅监听，不触发流程）");
                }
                else
                {
                    StopCamMonitor();
                    LogHelper.Instance.Log("跳过相机", "INFO", "非自动模式下，跳过相机已取消，停止PLC监控");
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
                    if (!connectPlc_checkBox.Checked)
                    {
                        Thread.Sleep(1000);
                        continue;
                    }

                    // 使用新方法读取指定寄存器（相机允许信号）
                    var camValue = ReadPlcSpecificRegister(CommunicationProtocol.camRegister);

                    // 检测到触发条件
                    if (camValue.HasValue && camValue.Value == CommunicationProtocol.camAllow)
                    {
                        LogHelper.Instance.Log("PLC监控", "INFO", $"检测到cam寄存器值为{camValue.Value}，符合触发条件");
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
                    LogHelper.Instance.Log("PLC监控", "ERROR", $"PLC监控线程异常：{ex.Message}");
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
            if (tcpServer_checkBox.Checked)
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
                LogHelper.Instance.Log("TCP服务器", "INFO", $"TCP服务器已启动，监听[{tcpIp}:{tcpPort}]");
            }
            catch (Exception ex)
            {
                LogHelper.Instance.Log("TCP服务器", "ERROR", $"启动TCP服务器失败：{ex.Message}");
                tcpServer_checkBox.Checked = false;
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
                    LogHelper.Instance.Log("TCP服务器", "INFO", "TCP服务器已停止");
                }
                catch (Exception ex)
                {
                    LogHelper.Instance.Log("TCP服务器", "ERROR", $"停止TCP服务器异常：{ex.Message}");
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
            LogHelper.Instance.Log("TCP服务器", "INFO", $"客户端[{clientInfo}]已连接");
            // 可以在这里添加UI更新代码
            BeginInvoke(new Action(() =>
            {
                // 更新UI显示客户端连接状态
            }));
        }

        // 添加客户端断开事件处理
        private void OnClientDisconnected(string clientInfo)
        {
            LogHelper.Instance.Log("TCP服务器", "INFO", $"客户端[{clientInfo}]已断开");
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
            LogHelper.Instance.Log("TCP服务器", "INFO", $"收到[{clientInfo}]数据：{data}");

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
                LogHelper.Instance.Log("TCP服务器", "ERROR", "未解析到有效产品ID，忽略触发");
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
                LogHelper.Instance.Log("TCP解析", "WARN", "TCP数据为空，无法解析产品ID");
                return string.Empty;
            }

            // 去除首尾空格（防止数据带空白符）
            string productId = tcpData.Trim();

            // 简单校验（如果收到的为NoRead，则通知camNG）
            if (productId.StartsWith("NoRead"))
            {
                LogHelper.Instance.Log("TCP解析", "ERROR", $"无效产品ID格式：{tcpData}");
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
        }

    }
}