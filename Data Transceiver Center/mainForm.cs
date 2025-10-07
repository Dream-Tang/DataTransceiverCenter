using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Data_Transceiver_Center
{
    public partial class mainForm : Form, IMainConfigurable
    {
        private Form1 _form1;    // 创建窗口1 窗口变量
        private Form2 _form2;    // 创建窗口2 窗口变量
        private Form3 _form3;    // 创建窗口3 窗口变量

        IPAddress localAddr = IPAddress.Parse("0.0.0.0"); // 本地所有网络地址
        Int32 localPortNum = Int32.Parse("8080");  // TCP服务器的端口

        // 跳过相机功能需要使用的私有变量，用于线程的跟踪和处理
        private Thread _camMonitorThread;
        private bool _isCamMonitorRunning;
        private readonly object _camMonitorLock = new object();

        // 依赖抽象接口，而非具体类
        private IIniConfigurable _iniLoadForm;   // 加载Form1的接口引用
        private IJsonConfigurable _jsonLoadForm; // 加载Form3的接口引用
        private IIniSavable _iniSaveForm;    // 保存Form1配置的接口
        private IJsonSavable _jsonSaveForm;  // 保存Form3配置的接口

        public mainForm()
        {
            InitializeComponent();
            // 在构造函数中创建实例
            _form1 = new Form1();
            _form2 = new Form2();
            _form3 = new Form3();

            // 初始化时注入具体实现（依赖注入思想）
            _iniLoadForm = _form1;   // Form1实现了IIniConfigurable
            _jsonLoadForm = _form3;  // Form3实现了IJsonConfigurable
            // 初始化保存接口引用（与加载接口对应）
            _iniSaveForm = _form1;
            _jsonSaveForm = _form3;

            // 初始化嵌入设置
            InitChildForm(_form1);
            InitChildForm(_form2);
            InitChildForm(_form3);

            // 3. 订阅子窗体的事件，建立数据同步机制
            // 关键解耦设计：MainForm作为中间者，接收双方事件并转发
            _form1.TextUpdated += OnForm1TextChanged;
            // 订阅Form1的查询请求事件
            _form1.RequestMesRootData += OnForm1RequestMesRootData;
            // 绑定FormClosing事件
            this.FormClosing += mainForm_FormClosing;

            // 默认显示Form1
            ShowForm(_form1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + "  V" + Application.ProductVersion;
            //this.tcpServer_checkBox.Checked = true;
            //this.autoRun_checkBox.Checked = false;
            //this.ignoreCheck_checkBox.Checked = true;
            // this.autoRun_btn.Enabled = false;
            _form1.btnRetryRead += new Form1.btnOnClickDelegate(btn_RetryRead_Click);
            _form1.btnRetryChk += new Form1.btnOnClickDelegate(btn_RetryChk_Click);
        }

#region 子窗体功能
        // 初始化子窗体嵌入属性
        private void InitChildForm(Form childForm)
        {
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill; // 填充整个容器
            childForm.Visible = false;
            // 关键：设置锚定属性，确保子窗体随容器拉伸
            childForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
        }

        // 显示指定子窗体
        private void ShowForm(Form form)
        {
            panelContainer.Controls.Clear();  // 清空原容器上的控件
            panelContainer.Controls.Add(form);// 将窗体1加入容器panel
            form.Visible = true;
        }

        // 打开页面1
        private void btn_form1Open(object sender, EventArgs e)
        {
            ShowForm(_form1);
        }

        // 打开页面2
        private void btn_form2Open(object sender, EventArgs e)
        {
            ShowForm(_form2);
        }

        // 打开页面3
        private void btn_form3Open(object sender, EventArgs e)
        {
            ShowForm(_form3);
        }
#endregion 子窗体功能

        // Form1的panelID文本变化时，同步到Form3中
        private void OnForm1TextChanged(string text)
        {
            _form3.UpdateIDText(text);
        }

        // 处理Form1的查询请求,将mesData通过事件委托传递给Form1
        private void OnForm1RequestMesRootData()
        {
            // 1. 从Form3获取MesPostRoot属性的数据
            MesPostRoot mesData = _form3.MesPostRoot;

            // 2. 将数据传递给Form1
            _form1.ReceiveMesRootData(mesData);
        }


        // 保存配置按钮点击事件
        private void btnSaveIni_Click(object sender, EventArgs e)
        {
            // 1. 选择保存文件夹（与加载时的文件选择逻辑一致）
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                dialog.Description = "请选择配置保存位置";
                if (dialog.ShowDialog() != DialogResult.OK)
                    return;

                string savePath = dialog.SelectedPath;

                // 2. 保存Form1的ini配置（文件名与加载时保持一致）
                string iniFile = Path.Combine(savePath, "settings.ini");
                SaveForm1Config(iniFile);

                // 3. 保存Form3的json配置（文件名与加载时保持一致）
                string jsonFile = Path.Combine(savePath, "MesSettings.json");
                SaveForm3Config(jsonFile);

                // 4. 保存MainForm自身的复选框状态（与加载时的LoadMainConfig对应）
                SaveMainConfig(iniFile);

                MessageBox.Show($"配置已保存到:\r\n{savePath}");
            }

        }

        // 加载配置按钮点击事件
        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            // 1. 选择ini文件（给Form1）
            string iniFile = SelectFile("请选择ini文件", "ini文件(*.ini)|*.ini");
            if (string.IsNullOrEmpty(iniFile)) return;

            // 2. 选择json文件（给Form3）
            string jsonFile = SelectFile("请选择json文件", "json文件(*.json)|*.json");
            if (string.IsNullOrEmpty(jsonFile)) return;

            // 3. 通过接口调用加载（线程安全处理）
            LoadForm1Config(iniFile);         // 再加载Form1的ini配置
            LoadForm3Config(jsonFile);       // 最后加载Form3的json配置
            LoadMainConfig(iniFile);        // 先加载mainForm自身的Checkbox状态
        }


        // 自动模式按钮（执行一个循环）：
        // 流程：读CSV->mes1->mes2->  生成打印指令->发送打印->mes3->串口(Event)
        // task0：读PLC
        // task1：（启动条件）读csv=》mes1=》mes2
        // async task2：（启动条件）makeZPL=》发送ZPL=》await mes3
        // task3：refalsh()=>methedInvoke();
        // task4：获取CheckResult，读取判定结果，写PLC
        private void autoRun_btn_Click(object sender, EventArgs e)
        {
            AutoRunMode();
        }

        // 自动模式：
        // 流程：读PLC-> mes1-> 生成打印指令 -> 发送打印-> 发送PLC ->串口(Event)
        // task0：读PLC
        // task1：（启动条件）读csv=》mes1=》mes2
        // async task2：（启动条件）makeZPL=》发送ZPL=》await mes3
        // task3：refalsh()=>methedInvoke();
        // task4：获取CheckResult，读取判定结果，写PLC
        private async void AutoRunMode()
        {
            short cam=-1, prt=-1, scn=-1;
            Tuple<short,short,short> plcRegValue;

            string consoleInfo = "";

            // 读PLC，确认PLC是否连接上
            if (ignorePlc_checkBox.Checked)
            {
                //(cam, prt, scn) = (-1, -1, -1);   // .net4.8特性
                cam = -1;
                prt = -1;
                scn = -1;
            }
            else
            {
                //(cam, prt, scn) = _form2.ReadPlc();   // .net4.8特性
                plcRegValue = _form2.ReadPlc();

                cam = plcRegValue.Item1;
                prt = plcRegValue.Item2;
                scn = plcRegValue.Item3;

                // PLC未连接，读取数值为-1，直接返回
                if (cam == -1 & prt == -1 & scn == -1)
                {
                    return;
                }
            }


            // Post的参数
            string postUrl = "";
            string postJson = "";

            string responseString = "";

            string fieldCode = "";
            string fieldInfo = "";
            string fieldData = "";


            try
            {
                // 获取Mes设置值
                postUrl = _form3.MesPostRoot.MesUrl;
                postJson = JsonConvert.SerializeObject(_form3.MesPostRoot.MesData);
            }
            catch (Exception)
            {
                postJson = "Mes设置未导入";
                Console.WriteLine("Mes设置未导入");
            }

            // t1：MES通信 
            var t1 = Task.Run(() =>
            {
                try
                {
                    // 调用POST发送，并获取Mes回复
                    responseString = HttpUitls.PostJson(postUrl, postJson);
                    // 反序列化（将string类型的Json数据转换成类对象）
                    MesResponseJson responseJson = JsonConvert.DeserializeObject<MesResponseJson>(responseString);
                    // 从类对象的属性中，提取Json字段的数据
                    fieldCode = responseJson.code;
                    fieldInfo = responseJson.info;
                    fieldData = responseJson.data;

                    // 如果有收到TCP信息
                    if (fieldData != "")
                    {
                        Console.WriteLine($"已经收到Mes回复的数据：{fieldData}");
                        // 发送camOK信号，UpdatePLC函数中有ignore的判断
                        UpdatePLCReg(cam: CommunicationProtocol.camOK);
                        // 更新form1页面
                        Action action = () => { _form1.SetLbReadCode(CommunicationProtocol.readCodeOK); };
                        BeginInvoke(action);

                        cam = CommunicationProtocol.camOK;
                    }

                }
                catch (Exception)
                {
                    responseString = "Task t1: Mes通信出错了";

                    // 发送camNG，UpdatePLC函数中有ignore的判断
                    UpdatePLCReg(cam: CommunicationProtocol.camNG);
                    // 更新form1页面
                    Action action = () => { _form1.SetLbReadCode(CommunicationProtocol.readCodeNG); };
                    BeginInvoke(action);

                    cam = CommunicationProtocol.camNG;
                }

                MethodInvoker mi1 = new MethodInvoker(() =>
                {
                    // 刷新mes收发数据的显示
                    _form1.refreshMes1(postJson, responseString);
                });
                BeginInvoke(mi1);
            });
            await t1;
            consoleInfo = responseString;
            Console.WriteLine("task t1 done：Post JsonData To Mes, get ResponseData: \r\n" + consoleInfo);


            // t2：打印（需要等待prt信号为ready）
            // 收到mes的fieldData不为空才打印
            if (fieldData != "")
            {
                var t2 = Task.Run(async() =>
                {
                    // 需要读取到PLC的 打印ready信号，才开始打印
                    if (!ignorePlc_checkBox.Checked)
                    {
                        prt = _form2.ReadPlc().Item2;
                        while (!(prt == CommunicationProtocol.prtReady))
                        {
                            plcRegValue = _form2.ReadPlc();

                            cam = plcRegValue.Item1;
                            prt = plcRegValue.Item2;
                            scn = plcRegValue.Item3;

                            await Task.Delay(500); // 用 await Task.Delay 代替 Thread.Sleep，释放线程
                        }
                        // UpdatePLCReg(prt:CommunicationProtocol.prtComplete);
                        Console.WriteLine("打印条码：已收到prtReady信号，准备生成ZPL文件并发送打印机");

                        // 调用打印机打印条码函数
                        Action act0 = () => { _form1.PrintCode(fieldData); };
                        BeginInvoke(act0);// 非阻塞，UI线程空闲时执行

                        consoleInfo = "    条码 " + fieldData;
                        
                        Action act1 = () => { _form1.SetLbPrtCode(CommunicationProtocol.prtCodeOK); };
                        BeginInvoke(act1);// 非阻塞，UI线程空闲时执行
                    }
                    else
                    {
                        consoleInfo = "屏蔽PLC，条码跳过打印";
                        Action action = () => { _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG); };
                        BeginInvoke(action);// 非阻塞，UI线程空闲时执行
                    }
                }); 
                await t2;
                Console.WriteLine("task t2 done：生成ZPL文件, 发送打印机打印： \r\n" + consoleInfo);
            }
            else
            {
                Console.WriteLine("task t2 done：Mes数据为空，不进行打印");
                Action action = () => { _form1.SetLbPrtCode(CommunicationProtocol.prtCodeNG); };
                BeginInvoke(action);
            }


            // t3：校验
            var t5 = Task.Run(() => 
            {
                if (ignoreCheck_checkBox.Checked)
                {
                    _form1.ignoreCheck = true;
                    t5CheckTask();
                }
            });
            await t5;
            Console.WriteLine("task t5：Check:" );
        }

        // 触发自动执行
        private void trigger1_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;

            // 二维码触发输入
            var t1 = Task.Run(async() =>
            {
                while (autoRun_checkBox.Checked)
                {
                    // F1 触发后，将trigSingner置为working状态
                    if (_form1.trigSigner == Form1.STATUS_WORKING)
                    {
                        //未禁用PLC，则执行，禁用则跳过
                        if (!ignorePlc_checkBox.Checked)
                        {
                            // 触发放行
                            plcRegValue = _form2.ReadPlc();
                            cam = CommunicationProtocol.camReset;
                            prt = plcRegValue.Item2;
                            scn = plcRegValue.Item3;
                            _form2.WritePlc(cam, prt, scn);
                        }

                        Console.WriteLine("trigger：自动运行已触发");
                        Action action = () =>
                        {
                            AutoRunMode();
                        };
                        _form1.trigSigner = Form1.STATUS_WAIT;
                        //_form1.veriCodeCount = _form1.veriCodeCount + 1;

                        Invoke(action);
                    }
                    await Task.Delay(1000);// 非阻塞等待
                }
            });
        }

        // 忽略校验按钮
        private void chkbox_ignoreCheck_checked(object sender, EventArgs e) // 
        {
            if (ignoreCheck_checkBox.Checked)
            {
                _form1.ignoreCheck = true;
            }
            else 
            {
                _form1.ignoreCheck = false;
            }
            // 校验 并与PLC通信
            var t5 = Task.Run(async() =>
            {
                while (!ignoreCheck_checkBox.Checked)
                {
                    _form1.ignoreCheck = false;
                    if (_form1.seriStatus == Form1.STATUS_READY)
                    {
                        Action action = () =>
                        {
                            t5CheckTask();
                        };
                        _form1.seriStatus = Form1.STATUS_WAIT;
                        BeginInvoke(action);
                    }
                    await Task.Delay(500);
                }
            });
        }

        // 校验任务t5
        private void t5CheckTask()
        {
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;
            string checkResult = "";

            Action action = () => { checkResult = _form1.GetCheckResult(); };
            Invoke(action);

            if (checkResult == "OK")
            {
                scn = CommunicationProtocol.checkOK;
                Console.WriteLine("task t5：校验结果OK");
                _form1.seriStatus = Form1.STATUS_WAIT;
            }
            if (checkResult == "NG")
            {
                scn = CommunicationProtocol.checkNG;
                Console.WriteLine("task t5：校验结果NG");
                _form1.seriStatus = Form1.STATUS_WAIT;
            }
            if (checkResult == "")
            {
                scn = CommunicationProtocol.checkLose;
                Console.WriteLine("task t5：未校验");
                return; // 此处会一直等待校验结果
            }

           
            // 未禁用PLC，则将信号写入PLC，禁用PLC则跳过
            if (!ignorePlc_checkBox.Checked)
            {
                plcRegValue = _form2.ReadPlc();
                cam = plcRegValue.Item1;
                prt = plcRegValue.Item2;
                _form2.WritePlc(cam, prt, scn);

                Console.WriteLine("task t5：已发送校验结果给PLC");
            }
        }

        // 连接PLC复选按钮
        private void connectPlc_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (connectPlc_checkBox.Checked)
            {
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
                _form2.btn_Close_Click(null, null);
                lable_PlcConnectStatus.Text = "PLC 已断开";
                lable_PlcConnectStatus.ForeColor = System.Drawing.Color.White;
                lable_PlcConnectStatus.BackColor = System.Drawing.Color.Black;
            }
        }

        // 更新PLC寄存器，向PLC进行通信，发送状态
        private void UpdatePLCReg(short cam=-1, short prt=-1, short scn=-1)
        {
            Tuple<short, short, short> plcRegValue;

            //未禁用PLC，则执行，禁用则跳过
            if (!ignorePlc_checkBox.Checked)
            {
                // 触发放行
                plcRegValue = _form2.ReadPlc();
                if (cam == -1)
                {
                    cam = plcRegValue.Item1;
                }
                if (prt == -1)
                {
                    prt = plcRegValue.Item2;
                }
                if (scn == -1)
                {
                    scn = plcRegValue.Item3;
                }
                _form2.WritePlc(cam, prt, scn);
            }
        }

        // 手动读码按钮
        public void btn_RetryRead_Click()
        {
            // 给PLC发送信号
            UpdatePLCReg(cam: CommunicationProtocol.camRetry);
        }

        // 手动验码按钮
        public void btn_RetryChk_Click()
        {
            // 给PLC发送信号
            UpdatePLCReg(scn: CommunicationProtocol.scannerStart);
        }

        // 跳过相机按钮
        private void ignoreCam_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            // 跨线程获取_form1的返回码
            // 判断是否需要跨线程调用
            // 当 当前线程 == 控件的创建线程（UI 线程） 时，InvokeRequired 返回 false，可以直接操作控件。
            // 当 当前线程！= 控件的创建线程 时，InvokeRequired 返回 true，此时必须通过 Invoke 或 BeginInvoke 方法间接操作控件。
            if (_form1.InvokeRequired)
            {// 需要跨线程：通过Invoke在UI线程执行委托
                _form1.Invoke(new Action(() => { _form1.ClearVericode(); }));
            }
            else
            {   // 不需要跨线程：直接操作
                _form1.ClearVericode();
            }

            if (ignoreCam_checkBox.Checked)
            {
                StartCamMonitor();
            }
            else
            {
                StopCamMonitor();
            }
        }

        #region 加载配置的功能
        // 封装文件选择逻辑
        private string SelectFile(string title, string filter)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Title = title;
                dialog.Filter = filter;
                return dialog.ShowDialog() == DialogResult.OK ? dialog.FileName : null;
            }
        }

        // 加载ini配置（线程安全）
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

        // 加载json配置（线程安全）
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

        // 实现主窗体配置加载接口
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
                // 从[Checkbox]节点读取各Checkbox状态
                // 注意：IniFile的Read方法需根据实际实现调整（此处为示例）
                ignoreCam_checkBox.Checked = ini.ReadBoolean("ignoreCam", "Checkbox", false);
                ignoreCheck_checkBox.Checked = ini.ReadBoolean("ignoreCheck", "Checkbox", false);
                ignorePlc_checkBox.Checked = ini.ReadBoolean("ignorePlc", "Checkbox", false);
                connectPlc_checkBox.Checked = ini.ReadBoolean("connectPlc", "Checkbox", false);

                autoRun_checkBox.Checked = ini.ReadBoolean("autoRun", "Checkbox", false);
                tcpServer_checkBox.Checked = ini.ReadBoolean("tcpServer", "Checkbox", false);
                // 其他需要加载的Checkbox...

                Console.WriteLine("mainForm CheckBox状态加载完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载mainForm配置失败：{ex.Message}");
            }
        }
        #endregion 加载配置的功能 

        #region 配置保存功能
        // 线程安全地保存Form1的ini配置
        private void SaveForm1Config(string iniFile)
        {
            if (_iniSaveForm is Form form && form.InvokeRequired)
            {
                // 跨线程时通过Invoke切换到UI线程
                form.Invoke(new Action(() => _iniSaveForm.SaveIni(iniFile)));
            }
            else
            {
                // 同一线程直接调用
                _iniSaveForm.SaveIni(iniFile);
            }
        }

        // 线程安全地保存Form3的json配置
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

        // 保存MainForm自身的配置（与LoadMainConfig对应）
        private void SaveMainConfig(string iniFilePath)
        {
            try
            {
                var ini = new IniFile(iniFilePath);
                // 保存复选框状态（与加载时读取的字段一一对应）
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
        #endregion 配置保存功能（与加载逻辑对应）

        #region TCP通信功能
        // 修改TCP相关字段，增加状态控制和线程安全标识
        private TcpListener server = null;
        private bool isTcpServerRunning = false; // 服务器运行状态
        private Thread tcpListenerThread; // 监听线程

        // TCP接收复选框按钮
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
        /// 启动TCP服务器，自动进入监听状态
        /// </summary>
        private void StartTcpServer()
        {
            // 防止重复启动
            if (isTcpServerRunning || server != null)
                return;

            try
            {
                // 初始化服务器并启动
                server = new TcpListener(localAddr, localPortNum);
                server.Start();
                isTcpServerRunning = true;

                // 启动独立线程进行监听，避免阻塞UI
                tcpListenerThread = new Thread(TcpListenLoop);
                tcpListenerThread.IsBackground = true; // 后台线程，程序退出时自动结束
                tcpListenerThread.Start();

                Console.WriteLine($"TCP服务器已启动，正在监听 {localAddr}:{localPortNum}，等待客户端连接...");
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"启动TCP服务器失败: {ex.Message}");
                tcpServer_checkBox.Checked = false; // 启动失败回滚状态
                CleanupServer();
            }
        }

        /// <summary>
        /// 停止TCP服务器
        /// </summary>
        private void StopTcpServer()
        {
            isTcpServerRunning = false; // 先设置状态为停止
            CleanupServer();

            // 等待监听线程结束
            if (tcpListenerThread != null && tcpListenerThread.IsAlive)
            {
                tcpListenerThread.Join(1000); // 最多等待1秒
                tcpListenerThread = null;
            }

            Console.WriteLine("TCP服务器已停止");
        }

        /// <summary>
        /// 清理服务器资源
        /// </summary>
        private void CleanupServer()
        {
            if (server != null)
            {
                try
                {
                    server.Stop(); // 停止服务器会中断AcceptTcpClient阻塞
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"停止服务器时出错: {ex.Message}");
                }
                server = null;
            }
        }

        /// <summary>
        /// 监听循环，持续接受客户端连接
        /// </summary>
        private void TcpListenLoop()
        {
            while (isTcpServerRunning)
            {
                try
                {
                    // 阻塞等待客户端连接（当server.Stop()被调用时会抛出异常）
                    TcpClient client = server.AcceptTcpClient();

                    // 接收到连接后，启动新线程处理客户端通信
                    // 注意：使用ParameterizedThreadStart传递参数
                    Thread clientThread = new Thread(HandleClientCommunication);
                    clientThread.IsBackground = true;
                    clientThread.Start(client);

                    Console.WriteLine($"客户端 {((IPEndPoint)client.Client.RemoteEndPoint).Address}:{((IPEndPoint)client.Client.RemoteEndPoint).Port} 已连接");
                }
                catch (SocketException ex)
                {
                    // 服务器停止时的正常中断异常，无需处理
                    if (isTcpServerRunning)
                        Console.WriteLine($"监听客户端连接时出错: {ex.Message}");
                    break;
                }
                catch (Exception ex)
                {
                    if (isTcpServerRunning)
                        Console.WriteLine($"TCP监听循环异常: {ex.Message}");
                    break;
                }
            }
        }

        /// <summary>
        /// 处理与单个客户端的通信
        /// </summary>
        /// <param name="obj">TcpClient实例</param>
        private void HandleClientCommunication(object obj)
        {
            TcpClient client = obj as TcpClient;
            if (client == null) return;


            NetworkStream stream = null;
            try
            {
                // 获取通信流
                stream = client.GetStream();
                byte[] buffer = new byte[256];
                string data;

                // 持续接收客户端数据
                while (isTcpServerRunning && client.Connected)
                {
                    // 读取数据（阻塞操作，直到有数据或连接断开）
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        // 读取到0字节表示客户端正常断开
                        Console.WriteLine($"客户端 {GetClientInfo(client)} 已断开连接");
                        break;
                    }

                    // 解析数据（使用UTF8编码避免乱码）
                    data = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"收到来自 {GetClientInfo(client)} 的数据: {data}");

                    // 跨线程更新UI显示
                    MethodInvoker mi = new MethodInvoker(() =>
                    {
                        _form1.txtBox_veriCodeHistory.AppendText($"{data}\r\n");
                    });
                    BeginInvoke(mi);

                    // 回发数据给客户端（可选，根据需求调整）
                    byte[] response = System.Text.Encoding.UTF8.GetBytes($"服务器已收到: {data}");
                    stream.Write(response, 0, response.Length);
                }
            }
            catch (IOException ex)
            {
                // 客户端异常断开会触发此异常
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
        #endregion TCP通信功能

        #region 跳过相机功能实现
        // 启动监控线程，后台线程监控PLC的寄存器
        private void StartCamMonitor()
        {
            // 确保线程未运行
            lock (_camMonitorLock)
            {
                if (_isCamMonitorRunning)
                    return;

                _isCamMonitorRunning = true;
                _camMonitorThread = new Thread(CamMonitorLoop);
                _camMonitorThread.IsBackground = true; // 后台线程，程序退出时自动结束
                _camMonitorThread.Start();
                Console.WriteLine("相机监控线程已启动");
            }
        }

        // 停止PLC监控线程
        private void StopCamMonitor()
        {
            lock (_camMonitorLock)
            {
                if (!_isCamMonitorRunning)
                    return;

                _isCamMonitorRunning = false;
            }

            // 等待线程结束
            if (_camMonitorThread != null && _camMonitorThread.IsAlive)
            {
                _camMonitorThread.Join(1000); // 最多等待1秒
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
        /// 相机监控监控PLC寄存器cam值的循环方法
        /// 在线程中运行，持续检测cam值并在条件满足时触发AutoRun流程
        /// </summary>
        private void CamMonitorLoop()
        {
            // 循环运行，直到监控标志位被设为false
            while (_isCamMonitorRunning)
            {
                try
                {

                    // 检查PLC连接状态：
                    // 1. 检查连接复选框是否勾选
                    if (!connectPlc_checkBox.Checked)
                    {
                        // PLC未连接或连接异常时，休眠1秒后重试
                        Thread.Sleep(1000);
                        // 跳过本次循环剩余代码，直接进入下一次循环
                        continue;
                    }

                    // 跨线程调用_form2的ReadPlc()方法
                    Tuple<short, short, short> plcRegValue = null;
                    if (_form2.InvokeRequired)
                    {
                        // 使用Invoke确保在_form2的线程上执行，保证线程安全
                        plcRegValue = (Tuple<short, short, short>)_form2.Invoke(new Func<Tuple<short, short, short>>(_form2.ReadPlc));
                    }
                    else
                    {
                        plcRegValue = _form2.ReadPlc();
                    }
                    short camValue = plcRegValue.Item1;

                    // 检查cam值是否为10（触发条件）
                    if (camValue == CommunicationProtocol.camAllow)
                    {
                        Console.WriteLine("检测到cam值为10，触发AutoRun流程");

                        // 切换到UI线程执行AutoRunMode方法（避免跨线程操作UI控件）
                        BeginInvoke(new Action(AutoRunMode));

                        // 触发后休眠2秒，避免短时间内重复触发
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        // 未检测到触发条件，短暂休眠减少CPU占用
                        Thread.Sleep(500);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"相机监控线程出错: {ex.Message}");
                    // 出错时稍等再重试
                    Thread.Sleep(1000);
                }
            }
        }

        // 确保线程正确停止
        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 确保线程被正确停止
            StopCamMonitor();
        }

#endregion 跳过相机功能

    }
}