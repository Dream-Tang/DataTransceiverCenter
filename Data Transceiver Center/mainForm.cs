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
            _checkHelper = new CheckHelper(_form1, _form2, new LoggerAdapter(this));
        }

        // 新增日志适配器（实现ILogger接口，适配主窗体原有Log方法）
        private class LoggerAdapter : ILogger
        {
            private readonly mainForm _mainForm;

            public LoggerAdapter(mainForm mainForm)
            {
                _mainForm = mainForm;
            }

            public void Log(string module, string level, string message)
            {
                _mainForm.Log(module, level, message);
            }
        }

        /// <summary>
        /// 标准化日志输出（带时间戳、模块、级别）
        /// </summary>
        /// <param name="module">模块名（如PLC/MES/打印）</param>
        /// <param name="level">日志级别（INFO/ERROR/WARN）</param>
        /// <param name="message">日志内容</param>
        private void Log(string module, string level, string message)
        {
            string log = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}] [{module}] [{level}] {message}";
            Console.WriteLine(log);
            // 可选：若需要写入日志文件，可在此处追加文件写入逻辑
            File.AppendAllText("AutoRun.log", log + Environment.NewLine);
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
                autoRun_checkBox.Checked = ini.ReadBoolean("autoRun", "Checkbox", false);
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
                ini.Write("autoRun", autoRun_checkBox.Checked.ToString(), "Checkbox");
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
        /// 自动运行一次按钮点击事件
        /// </summary>
        private void autoRun_btn_Click(object sender, EventArgs e)
        {
            AutoRunMode();
        }

        /// <summary>
        /// 自动运行模式主流程
        /// 流程：读PLC -> MES通信 -> 生成打印指令 -> 发送打印 -> 校验 -> 写PLC结果
        /// </summary>
        private async void AutoRunMode()
        {
            short cam = -1, prt = -1, scn = -1;
            string consoleInfo = "";

            // ========== 1. 读取PLC状态（调用拆分后的函数） ==========
            Log("PLC", "INFO", "开始读取PLC状态");
            var plcResult = ReadPlcStatus();
            if (!plcResult.Success)
            {
                // 读取失败时的处理（保持原逻辑：终止流程）
                consoleInfo = "PLC读取失败，终止本次自动流程";
                Log("PLC", "ERROR", consoleInfo);
                Log("PLC", "ERROR", $"读取失败详情：cam={plcResult.Cam}, prt={plcResult.Prt}, scn={plcResult.Scn}");
                return;
            }
            // 读取成功，赋值给变量
            cam = plcResult.Cam;
            prt = plcResult.Prt;
            scn = plcResult.Scn;
            Log("PLC", "INFO", $"PLC状态读取成功：cam={cam}, prt={prt}, scn={scn}");

            // 若屏蔽PLC，已在ReadPlcStatus中返回(-1,-1,-1)，无需额外处理

            // ========== 2. 重构MES通信部分 ==========
            // 步骤2.1：简化MES参数（仅保留必要的配置获取）
            string postUrl = "";
            MsePostData mesPostData = null; // 替换原postJson，直接用实体类
            string postJson = ""; // 保留原postJson变量，用于打印请求参数

            try
            {
                // 从Form3获取MES配置（保留原有配置来源，仅修改数据类型）
                Log("MES", "INFO", "开始获取MES配置信息");
                postUrl = _form3.MesPostRoot.MesUrl;
                mesPostData = _form3.MesPostRoot.MesData; // 直接获取实体类，无需手动序列化
                // 保留原有序列化逻辑，用于打印请求JSON
                postJson = JsonConvert.SerializeObject(mesPostData, Formatting.Indented);

                Log("MES", "INFO", $"MES配置获取成功 - URL：{postUrl}");
                Log("MES", "INFO", $"MES请求参数（JSON）：{Environment.NewLine}{postJson}");
            }
            catch (Exception ex)
            {
                consoleInfo = $"Mes设置未导入: {ex.Message}";
                Log("MES", "ERROR", consoleInfo);
                Log("MES", "ERROR", $"异常堆栈：{ex.StackTrace}"); // 保留异常堆栈
                // 配置错误直接终止流程
                return;
            }
            // 步骤2.2：调用MES通信助手类（替换原Task t1）
            Log("MES", "INFO", "开始发送MES POST请求");
            var mesResult = await MesCommunicationHelper.Instance.SendMesRequestAsync(postUrl, mesPostData);

            // 步骤2.3：处理MES响应结果（复用原有业务逻辑，仅适配新的返回值）
            if (mesResult.Success)
            {
                // MES通信成功（逻辑与原一致）
                consoleInfo = $"已收到MES回复的数据：{mesResult.Data}";
                Log("MES", "INFO", consoleInfo);
                Log("MES", "INFO", $"MES响应详情：{mesResult.Message}");
                WritePLCReg(cam: CommunicationProtocol.camOK);
                BeginInvoke(new Action(() =>
                {
                    _form1.SetLbReadCode(CommunicationProtocol.readCodeOK);
                    Log("UI", "INFO", "更新UI：条码读取状态设为OK");
                }));

                cam = CommunicationProtocol.camOK;
                Log("PLC", "INFO", $"写入PLC状态：cam={cam}（camOK）");
            }
            else
            {
                // MES通信失败（逻辑与原一致，仅替换错误信息来源）
                consoleInfo = mesResult.Message;
                Log("MES", "ERROR", $"MES通信失败：{consoleInfo}");
                WritePLCReg(cam: CommunicationProtocol.camNG);
                BeginInvoke(new Action(() =>
                {
                    _form1.SetLbReadCode(CommunicationProtocol.readCodeNG);
                    Log("UI", "INFO", "更新UI：条码读取状态设为NG");
                }));

                cam = CommunicationProtocol.camNG;
                Log("PLC", "INFO", $"写入PLC状态：cam={cam}（camNG）");
                Log("AutoRun", "WARN", "MES通信失败，终止本次自动流程");
                return; // 失败终止后续流程
            }


            // ========== 打印模块改造核心 ==========
            Log("打印", "INFO", "进入打印流程，开始前置检查");
            // 标记是否需要等待PLC就绪（屏蔽PLC开关）
            bool needWaitPlcReady = !ignorePlc_checkBox.Checked;
            if (needWaitPlcReady)
            {
                Log("打印", "INFO", "屏蔽PLC未勾选，开始等待打印机就绪信号（PLC prtReady）");
                prt = plcResult.Prt; // 复用之前读取的PLC状态，避免重复调用

                // 步骤1：等待打印机就绪（原有逻辑保留）
                while (prt != CommunicationProtocol.prtReady)
                {
                    // 刷新PLC状态（复用ReadPlcStatus，保持逻辑统一）
                    var plcRefreshResult = ReadPlcStatus();
                    if (!plcRefreshResult.Success)
                    {
                        Log("打印", "ERROR", "等待打印机就绪时PLC读取失败，终止打印流程");
                        prt = CommunicationProtocol.prtNG;
                        WritePLCReg(prt: prt);
                        BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG)));
                        return;
                    }
                    prt = plcRefreshResult.Prt;
                    cam = plcRefreshResult.Cam;
                    scn = plcRefreshResult.Scn;

                    Log("打印", "WARN", $"打印机未就绪，当前PLC状态：prt={prt}，等待500ms后重试");
                    await Task.Delay(500);
                }
                Log("打印", "INFO", "打印机就绪信号已获取（prt=prtReady）");
            }
            else
            {
                // 步骤1（屏蔽PLC）：跳过等待，直接标记为“逻辑就绪”
                Log("打印", "WARN", "屏蔽PLC已勾选，跳过打印机就绪信号校验");
                prt = CommunicationProtocol.prtReady; // 强制设为就绪，推进流程
            }

            // 步骤2：调用PrintingHelper执行打印（无论是否屏蔽PLC，都执行打印）
            try
            {
                Log("打印", "INFO", "开始调用打印助手类执行打印");
                // 获取Form1的打印配置（已暴露属性）
                string zplTemplatePath = _form1.ZplTemplatePath;
                string printerPath = _form1.PrintName;
                string printCode = mesResult.Data; // MES返回的打印内容

                // 异步调用打印助手类
                var printResult = await PrintingHelper.Instance.ExecutePrintAsync(
                    printCode: printCode,
                    zplTemplatePath: _form1.ZplTemplatePath,  // 直接使用Form1暴露的模板路径属性
                    printerPath: printerPath
                );

                // 步骤3：处理打印结果（统一逻辑+标准化日志）
                if (printResult.Success)
                {
                    Log("打印", "INFO", $"打印成功：{printResult.Message}");
                    prt = CommunicationProtocol.prtOK;
                    // 线程安全更新UI
                    BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeOK)));

                    // 即使屏蔽PLC，仍写入PLC状态（若不需要可加判断：if(!ignorePlc_checkBox.Checked)）
                    WritePLCReg(prt: prt);
                    Log("PLC", "INFO", $"打印成功，写入PLC状态：prt={prt}（prtOK）");
                }
                else
                {
                    Log("打印", "ERROR", $"打印失败：{printResult.Message}");
                    prt = CommunicationProtocol.prtNG;
                    // 线程安全更新UI
                    BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG)));

                    // 即使屏蔽PLC，仍写入PLC状态（若不需要可加判断：if(!ignorePlc_checkBox.Checked)）
                    WritePLCReg(prt: prt);
                    Log("PLC", "INFO", $"打印失败，写入PLC状态：prt={prt}（prtNG）");
                    Log("AutoRun", "WARN", "打印失败，终止本次自动流程");
                    return;
                }
            }
            catch (Exception ex)
            {
                // 捕获助手类未覆盖的异常（兜底）
                Log("打印", "ERROR", $"打印流程未预期异常：{ex.Message}");
                Log("打印", "ERROR", $"异常堆栈：{ex.StackTrace}");
                prt = CommunicationProtocol.prtNG;
                BeginInvoke(new Action(() => _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG)));
                WritePLCReg(prt: prt);
                return;
            }

            Log("打印", "INFO", "打印流程完成，进入后续校验/收尾环节");
            // ... 后续逻辑（如扫描状态处理、流程结束） ...
        }

        // 新增线程终止方法
        private void StopAutoRunThread()
        {
            // 临时取消自动流程勾选，触发线程退出循环
            bool wasAutoRunChecked = autoRun_checkBox.Checked;
            autoRun_checkBox.Checked = false;
            // 短暂延迟确保线程退出
            Task.Delay(500).Wait();
            // 恢复原状态（若需要）
            autoRun_checkBox.Checked = wasAutoRunChecked;
        }


        /// <summary>
        /// 自动流程复选框状态变更事件
        /// </summary>
        private void trigger1_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // 启动自动触发监控线程
            var t1 = Task.Run(async () =>
            {
                while (autoRun_checkBox.Checked)
                {
                    if (_form1.trigSigner == Form1.STATUS_WORKING)
                    {
                        Console.WriteLine("trigger：自动运行已触发");

                        if (!ignorePlc_checkBox.Checked)
                        {
                            WritePLCReg(cam: CommunicationProtocol.camOK);
                        }

                        // 执行自动流程
                        Invoke(new Action(AutoRunMode));
                        _form1.trigSigner = Form1.STATUS_WAIT;
                    }
                    await Task.Delay(1000);
                }
            });
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
            // 先停止已有线程
            _isCheckMonitorRunning = false;
            Task.Delay(500).Wait();
            // 传递参数，更新Form1的忽略校验状态
            _form1.ignoreCheck = ignoreCheck_checkBox.Checked;

            // 启动新线程（若未勾选“忽略校验”）
            if (!ignoreCheck_checkBox.Checked)
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
                                    Log("校验线程", "ERROR", $"校验任务执行异常: {ex.Message}");
                                    Log("校验线程", "ERROR", $"异常堆栈: {ex.StackTrace}");
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
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;
            string checkResult = "";

            try
            {
                // 线程安全获取校验结果
                if (_form1.InvokeRequired)
                {
                    _form1.Invoke(new Action(() =>
                    {
                        checkResult = _form1.GetCheckResult();
                    }));
                }
                else
                {
                    checkResult = _form1.GetCheckResult();
                }

                // 根据校验结果设置PLC信号
                if (checkResult == "OK")
                {
                    scn = CommunicationProtocol.checkOK;
                    Console.WriteLine("task t5：校验结果OK");
                }
                else if (checkResult == "NG")
                {
                    scn = CommunicationProtocol.checkNG;
                    Console.WriteLine("task t5：校验结果NG");
                }
                else
                {
                    scn = CommunicationProtocol.checkLose;
                    Console.WriteLine("task t5：未校验");
                    return;
                }

                // 写入PLC（如果未屏蔽）
                if (!ignorePlc_checkBox.Checked)
                {
                    plcRegValue = _form2.ReadPlc();
                    if (plcRegValue != null)
                    {
                        cam = plcRegValue.Item1;
                        prt = plcRegValue.Item2;

                        if (plcRegValue.Item3 != CommunicationProtocol.checkLose)
                        {
                            if (_form2.InvokeRequired)
                            {
                                _form2.Invoke(new Action(() =>
                                    _form2.SafeWritePlc(cam, prt, scn)));
                            }
                            else
                            {
                                _form2.SafeWritePlc(cam, prt, scn);
                            }
                            Console.WriteLine("task t5：已发送校验结果给PLC");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"t5CheckTask出错: {ex.Message}");
            }
            finally
            {
                // 重置状态
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
                Log("串口校验", "ERROR", $"处理串口校验数据异常: {ex.Message}");
                Log("串口校验", "ERROR", $"异常堆栈: {ex.StackTrace}");
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
                Log("手动验码", "INFO", "用户触发手动验码");
                string checkResult = _checkHelper.ExecuteCheck();
                _checkHelper.UpdatePlcByCheckResult(checkResult, ignorePlc_checkBox.Checked);
            }
            catch (Exception ex)
            {
                Log("手动验码", "ERROR", $"手动验码执行失败: {ex.Message}");
                Log("手动验码", "ERROR", $"异常堆栈: {ex.StackTrace}");
            }
        }
        #endregion

        #region 跳过相机功能
        /// <summary>
        /// 跳过相机复选框状态变更事件
        /// </summary>
        private void ignoreCam_checkBox_CheckedChanged(object sender, EventArgs e)
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

            // 启动/停止相机监控线程
            if (ignoreCam_checkBox.Checked)
            {
                StartCamMonitor();
            }
            else
            {
                StopCamMonitor();
            }
        }

        /// <summary>
        /// 启动相机监控线程
        /// 监控PLC寄存器，当cam值为10时触发自动运行流程
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
                        Console.WriteLine($"检测到cam值为{camValue.Value}，触发AutoRun流程");
                        BeginInvoke(new Action(AutoRunMode));
                        Thread.Sleep(2000); // 避免短时间重复触发
                    }
                    else
                    {
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"相机监控线程出错: {ex.Message}");
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
        /// 启动TCP服务器
        /// </summary>
        private void StartTcpServer()
        {
            if (_isTcpServerRunning || _server != null)
                return;

            try
            {
                _server = new TcpListener(_localAddr, _localPortNum);
                _server.Start();
                _isTcpServerRunning = true;

                // 启动监听线程
                _tcpListenerThread = new Thread(TcpListenLoop);
                _tcpListenerThread.IsBackground = true;
                _tcpListenerThread.Start();

                Console.WriteLine($"TCP服务器已启动，正在监听 {_localAddr}:{_localPortNum}");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"启动TCP服务器失败: {ex.Message}");
                tcpServer_checkBox.Checked = false;
                CleanupServer();
            }
        }

        /// <summary>
        /// 停止TCP服务器
        /// </summary>
        private void StopTcpServer()
        {
            _isTcpServerRunning = false;
            CleanupServer();

            // 等待监听线程终止
            if (_tcpListenerThread != null && _tcpListenerThread.IsAlive)
            {
                _tcpListenerThread.Join(1000);
                _tcpListenerThread = null;
            }

            Console.WriteLine("TCP服务器已停止");
        }

        /// <summary>
        /// 清理TCP服务器资源
        /// </summary>
        private void CleanupServer()
        {
            if (_server != null)
            {
                try
                {
                    _server.Stop();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"停止服务器时出错: {ex.Message}");
                }
                _server = null;
            }
        }

        /// <summary>
        /// TCP监听循环
        /// 持续接受客户端连接并创建新线程处理
        /// </summary>
        private void TcpListenLoop()
        {
            while (_isTcpServerRunning)
            {
                try
                {
                    // 接受客户端连接
                    TcpClient client = _server.AcceptTcpClient();

                    // 启动客户端处理线程
                    Thread clientThread = new Thread(HandleClientCommunication);
                    clientThread.IsBackground = true;
                    clientThread.Start(client);

                    Console.WriteLine($"客户端 {GetClientInfo(client)} 已连接");
                }
                catch (SocketException ex)
                {
                    if (_isTcpServerRunning)
                        Console.WriteLine($"监听客户端连接时出错: {ex.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    if (_isTcpServerRunning)
                        Console.WriteLine($"TCP监听循环异常: {ex.Message}");
                    break;
                }
            }
        }

        /// <summary>
        /// 处理与客户端的通信
        /// </summary>
        /// <param name="obj">TcpClient实例</param>
        private void HandleClientCommunication(object obj)
        {
            TcpClient client = obj as TcpClient;
            if (client == null) return;

            NetworkStream stream = null;
            try
            {
                stream = client.GetStream();
                byte[] buffer = new byte[256];
                string data;

                // 持续接收数据
                while (_isTcpServerRunning && client.Connected)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        Console.WriteLine($"客户端 {GetClientInfo(client)} 已断开连接");
                        break;
                    }

                    // 解析并显示数据
                    data = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"收到来自 {GetClientInfo(client)} 的数据: {data}");

                    // 更新UI显示
                    BeginInvoke(new MethodInvoker(() =>
                    {
                        _form1.txtBox_veriCodeHistory.AppendText($"{data}\r\n");
                    }));

                    // 回发确认信息
                    byte[] response = System.Text.Encoding.UTF8.GetBytes($"服务器已收到: {data}");
                    stream.Write(response, 0, response.Length);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"与客户端 {GetClientInfo(client)} 通信时出错: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理客户端通信时发生异常: {ex.Message}");
            }
            finally
            {
                // 清理资源
                stream?.Close();
                client.Close();
                Console.WriteLine($"与客户端 {GetClientInfo(client)} 的连接已关闭");
            }
        }

        /// <summary>
        /// 获取客户端信息（IP:端口）
        /// </summary>
        private string GetClientInfo(TcpClient client)
        {
            try
            {
                IPEndPoint remoteEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
                return $"{remoteEndPoint.Address}:{remoteEndPoint.Port}";
            }
            catch
            {
                return "未知客户端";
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