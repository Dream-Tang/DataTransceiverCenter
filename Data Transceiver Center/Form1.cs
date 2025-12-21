using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;

namespace Data_Transceiver_Center
{
    public partial class Form1 : Form, IIniConfigurable, IIniSavable, IDataProvider
    {
        // 定义文本更新事件：当文本框内容变化时触发
        // 事件类型为Action<string>，用于传递最新文本内容
        public event Action<string> TextUpdated;
        // 定义查询请求事件，通知MainForm需要获取Form2的MesRoot数据
        public event Action RequestMesRootData;
        // 添加事件，用于通知 MainForm 串口数据接收完成
        public event Action SerialDataReceived;
        // 添加事件，用于通知 MainForm TCP数据接收完成，PLC放行
        public event Action PanelIDgot;

        public string _zplFilePath = "";
        public string _mPrintName = "";

        public string ZplTemplatePath => zplTemplatePath; // 暴露ZPL模板路径
        public string PrintName => _mPrintName; // 暴露打印机路径

        // 存储从form3查询到的MesRoot数据
        private MesPostRoot _receivedMesRoot;

        // 1. 声明CheckHelper实例（在Form1构造函数中初始化）
        private CheckHelper _checkHelper;

        private readonly IPLCService _plcService;                   // PLC服务接口
        private readonly LogHelper _logHelper = LogHelper.Instance; // 日志助手(单例模式)

        /// <summary>
        /// 构造函数：接收IPLCService
        /// </summary>
        public Form1(IPLCService plcService)
        {
            InitializeComponent();
            // 绑定文本框内容变化事件

            // 当用户输入文本时，自动触发事件传递数据
            txtBox_prtCode.TextChanged += TxtInput_TextChanged;

            // 初始化plcService
            _plcService = plcService ?? throw new ArgumentNullException(
           nameof(plcService), "IPLCService实例不能为空");
            // 初始化CheckHelper
            _checkHelper = new CheckHelper(this, _plcService);
        }

        private StringBuilder cmd_template = new StringBuilder("");     // 打印机指令内容，可变字符串类型

        public string zplTemplatePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public bool ignoreCheck = false;
        public bool ignoreCam = false;

        // 通信和流程标志位状态机，0初始化，1进行中，2完成，3异常
        internal int mes1Status = STATUS_WAIT;
        internal int mes2Status = STATUS_WAIT;
        internal int mes3Status = STATUS_WAIT;
        internal int prtStatus = STATUS_WAIT;        // 打印机状态，当发送完之后，打印机状态清除，只有打印后，才可清除prtCode
        internal int trigSigner = STATUS_WAIT;      // 触发信号状态，当二维码输入时，表示有触发信号，可执行全流程操作
        internal int seriStatus = STATUS_WAIT;      // 串口状态，当串口传入数据时，ready；校验结束后，wait。
        internal string tcpNoReadStr = "NoRead";


        // 通信标志位数值定义
        internal const int STATUS_WAIT = -1;
        internal const int STATUS_READY = 0;
        internal const int STATUS_WORKING = 1;
        internal const int STATUS_COMPLETE = 2;
        internal const int STATUS_EXCEPTION = 3;
        internal const int CONNECT_EXCEPTION = 4;
        internal const int CONVERT_EXCEPTION = 5;

        public string retryRead = "";

        public delegate void btnOnClickDelegate();
        public event btnOnClickDelegate btnRetryRead;
        public event btnOnClickDelegate btnRetryChk;

        //public uint veriCodeCount; // 扫码计数
        //private uint veriHistoryLines;  // 扫码列表行数

        // 生成ZPL文档
        public void makeZpl_btn_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.txtBox_zplPath.Text + "\\zpl.txt";
            string str = txtBox_prtCode.Text;
            MakeZpl(filePathZPL, str);
        }

        // 发送文件到打印机
        public void sendToPrt_btn_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.txtBox_zplPath.Text + "\\zpl.txt";
            string prtName = this.txtBox_prtPath.Text;
            SendFileToPrinter(filePathZPL, prtName);
        }

        // api test 按钮，直接发送mes api文本框中的内容
        private void apiTest_btn_Click(object sender, EventArgs e)
        {
            Task t1 = new Task(() =>
            {
                MesCommunicate();
            });
            t1.Start();
            // 触发事件，通过MainForm查询Form2的MesRoot数据
            PanelIDgot?.Invoke();
        }

        // Mes通信函数：
        // 1.通过查询函数获取Form3的PostJson数据
        // 2.调用http单元的Post方法发送
        // 3.接收Mes回复的Json数据
        // 4.将发送数据和接收数据在界面中进行显示
        private void MesCommunicate()
        {
            // 触发事件，通过MainForm查询Form2的MesRoot数据
            RequestMesRootData?.Invoke();

            string postUrl = _receivedMesRoot.MesUrl;
            //_receivedMesRoot.MesData.input.panelId = txtBox_veriCode.Text;
            string jsonData = JsonConvert.SerializeObject(_receivedMesRoot.MesData);

            // 耗费时间的操作
            string getJson = HttpUitls.PostJson(postUrl, jsonData);

            // 跨线程修改UI，使用methodinvoker工具类
            MethodInvoker mi = new MethodInvoker(() =>
            {
                refreshMes1(jsonData, getJson);
            });
            this.BeginInvoke(mi);
        }

        // 打开、关闭串口
        private void openSerial_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.PortName = cobBox_SeriPortNum.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    btn_openSerial.Text = "关闭串口";
                    serialPort_label.Text = "串口已打开";
                    serialPort_label.BackColor = System.Drawing.Color.Green;
                    serialPort_label.ForeColor = System.Drawing.Color.White;
                    cobBox_SeriPortNum.Enabled = false;
                    comboBox2.Enabled = false;
                }
                else
                {
                    serialPort1.Close();
                    btn_openSerial.Text = "打开串口";
                    serialPort_label.Text = "串口已关闭";
                    serialPort_label.BackColor = System.Drawing.Color.Gray;
                    serialPort_label.ForeColor = System.Drawing.Color.White;
                    cobBox_SeriPortNum.Enabled = true;
                    comboBox2.Enabled = true;
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("对端口的访问被拒绝，端口已被其它进程占用");
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("端口参数设置无效");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("端口名出错");
            }
            catch (IOException)
            {
                MessageBox.Show("此端口处于无效状态");
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("端口已被打开");
            }
            catch (FormatException)
            {
                MessageBox.Show("波特率未设置");
            }
        }


        // 新增串口数据缓存
        private StringBuilder serialDataBuffer = new StringBuilder();

        // 串口中断事件：当有数据收到时执行。将收到的数据按ASCII转换显示
        private void SerialPort1_DataRecived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                // textBox3 读出串口缓存内的数据，textBox4 将string数据转换成16进制byte，然后按ASCII转换成string

                // string portData = serialPort1.ReadTo("\r\n"); // 若结尾无换行，会阻塞线程

                if (!serialPort1.IsOpen) return;

                // 读取所有可用数据（非阻塞）
                string newData = serialPort1.ReadExisting();
                if (string.IsNullOrEmpty(newData)) return;

                serialDataBuffer.Append(newData);
                string fullData = serialDataBuffer.ToString();

                // 检查是否包含终止符
                int endIndex = fullData.IndexOf("\r\n");
                if (endIndex != -1)
                {
                    // 提取完整数据并清空缓存
                    string portData = fullData.Substring(0, endIndex).Trim();
                    serialDataBuffer.Remove(0, endIndex + 2); // 移除已处理数据（包括终止符）

                    seriStatus = STATUS_READY;

                    // 跨线程更新UI
                    IAsyncResult asyncResult = BeginInvoke(new MethodInvoker(() =>
                    {
                        txtBox_serialRead.Clear();
                        txtBox_serialRead.Text = portData;
                    }));

                    // 触发串口数据接收完成事件
                    SerialDataReceived?.Invoke();
                }
            }
            catch (Exception ex)
            {
                if (!lockSettings_checkBox.Checked)
                {
                    MessageBox.Show(ex.Message);
                }
                seriStatus = STATUS_WAIT;
            }

        }

        // 串口数据 文本框更新
        private void serialRead_txtBox_TextChanged(object sender, EventArgs e)
        {
            txtBox_scnCode.Text = txtBox_serialRead.Text;
        }

        // 校验扫码 文本框变化
        private void scnCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            TextUpdated?.Invoke(txtBox_scnCode.Text);
            _logHelper.Log("Form1", "INFO", $"打印码文本框变更：{txtBox_scnCode.Text}");
        }

        // 打印码 文本框变化，生成条码
        private void prtCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.Image = SetBarCode128(txtBox_prtCode.Text);
        }


        // 将ZPL指令输出到txt文档
        // filePathZPL为ZPL指令文件路径，str为打印的条码字符串
        public void MakeZpl(string filePathZPL, string str)
        {
            // ZPL文件生成，使用模板来生成，替换模板中 ^FD到^FS之间的文本
            StringBuilder zpl_cmd_SB = cmd_template;

            string zpl_cmd = zpl_cmd_SB.ToString();
            if (zpl_cmd == "")
            {
                Console.WriteLine("还未加载有效的ZPL模板");
                MessageBox.Show("还未加载有效的ZPL模板,请先选择模板文件");
                return;
            }
            else
            {
                int index_FD = zpl_cmd.IndexOf("^FD")+3; // 找到模板中^FD到^FS之间的位置
                int index_FS = zpl_cmd.IndexOf("^FS");
                zpl_cmd_SB.Remove(index_FD, index_FS - index_FD);// 移出模板中^FD到^FS之间的内容
                zpl_cmd_SB.Insert(index_FD, str);

                zpl_cmd = zpl_cmd_SB.ToString();
                int index_FDQA = zpl_cmd.IndexOf("^FDQA,")+6; // 找到模板中^FD到^FS之间的位置
                int index_FS2 = zpl_cmd.IndexOf("^FS", index_FS+3);
                zpl_cmd_SB.Remove(index_FDQA, index_FS2 - index_FDQA);
                zpl_cmd_SB.Insert(index_FDQA, str);

                // 利用正则表达式替换字符串中的值，此处替换为""，相当于提取字符串中的对应字符
                //string strSplit1 = Regex.Replace(line, "[0-9]", "", RegexOptions.IgnoreCase);// 提取字母部分
                //string strSplit2 = Regex.Replace(line, "[a-z]", "", RegexOptions.IgnoreCase);// 提取数字部分

                //string prtLine = strSplit1 + ">;" + strSplit2; // 输入的打印码，已被处理

                //cmd_template.Insert(index_FD, prtLine);

            }

            // ZPL文件保存
            try
            {
                // 保存到本地文件，先清空文件
                System.IO.File.WriteAllText(filePathZPL, string.Empty);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePathZPL, false, Encoding.UTF8))
                {
                    sw.Write(zpl_cmd_SB.ToString());
                    sw.Close();
                }
                runStatus_lable.Text = "zpl文件生产成功";

                pictureBox1.Image = SetBarCode128(str);
                //MessageBox.Show("zpl文件生产成功，文件位置："+ filePathZPL);
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine($"权限不足：{ex.Message}");
                // 处理逻辑：提示用户、切换路径、请求权限等
            }

            catch (Exception)
            {
                if (!this.lockSettings_checkBox.Checked) // 自动模式关闭才出弹窗
                {
                    MessageBox.Show("zpl文件生成失败\r\n" + filePathZPL);
                }
                runStatus_lable.Text = "zpl文件生产出错";
            }
        }

        // 给打印机发送文件
        // filePathZPL为ZPL指令文件路径，mPrintName为打印机路径，例如：mPrintName = @"\\192.168.0.132\zt411"
        public void SendFileToPrinter(string filePathZPL, string mPrintName)
        {
            try
            {
                // 将ZPL指令发送到打印机，filePathZPL为ZPL指令文件路径，mPrintName为打印机路径，例如：mPrintName = @"\\192.168.0.132\zt411"
                File.Copy(filePathZPL, mPrintName, true);
                runStatus_lable.Text = "发送打印机成功!";
                prtStatus = STATUS_COMPLETE;
            }
            catch (Exception ex)
            {
                if (!this.lockSettings_checkBox.Checked) // 自动模式关闭才出弹窗
                {
                    MessageBox.Show(ex.Message);
                }
                runStatus_lable.Text = "发送打印机失败";
            }

        }

        // 打印条码功能：等待prtCode（打印码来自Mes2消息），有prtCode后，生成ZPL文件，发送给打印机
        public void PrintCode(string printStr)
        {
            // 将读到的玻璃码直接传送到打印机进行打印
            txtBox_prtCode.Text = printStr;

            string prtCode = txtBox_prtCode.Text;


            // 有打印码，才进行打印；
            if (prtCode != "")
            {
                MakeZpl(this._zplFilePath, prtCode);

                SendFileToPrinter(this._zplFilePath, this._mPrintName);

                lastPrtCode_label.Text = prtCode;
            }
            else
            {
                runStatus_lable.Text = "wait prtCode";
            }

        }

        #region "C# 后台刷新UI的方法"
        // C# 中后台刷新UI的方法
        // 声明一个委托，以准备跨线程修改UI的属性
        public delegate void RefreshUI(Control c, object o);

        // 刷新的方法
        public void refreshUI(Control C, object o)
        {
            // 这里可以强转任意控件，然后改他们的方法，参数o也可以是任意复杂结构类型，只要你封装好，拆封好就OK
            ((ListBox)C).Items.Add(o.ToString());
        }
        // 在需要调用的地方写
        //this.Invoke(new RefreshUI(refreshUI), new object[] { listBox1,"string" });
        #endregion

        public void refreshMes1(string api, string getJson)
        {
            // 显示发送的数据
            this.txtBox_postData.Text = "POST发送:\r\n" + api;
            // 显示接收的数据
            this.txtBox_responseData.Text = "Mes响应:\r\n" + getJson;
        }

        // 窗口生成时，需要做的事情
        private void Form1_Load(object sender, EventArgs e)
        {
            reloadPort_btn_Click(null, null);
            LockSetting("Lock");
        }

        // 串口收到hexString = "46 32 33 36 31 35 30 36 37 39 35 0D"的数，转换成byte[]形式返回
        // 通过System.Text.Encoding.ASCII.GetString(byte[] buf)，可以将buf按ascii转换成string
        private byte[] ToBytesFromHexString(string hexString)
        {
            // 串口收到数据 hexString = "46 32 33 36 31 35 30 36 37 39 35";
            // 以' '分割字符串，并去掉空字符
            string[] chars = hexString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            byte[] returnBytes = new byte[chars.Length];

            // 逐个字符变为16进制字节数据
            for (int i = 0; i < chars.Length - 1; i++)
            {
                returnBytes[i] = Convert.ToByte(chars[i], 16);
            }
            return returnBytes;
        }

        // 锁定设置选项
        private void lockSettings_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string Lock_setting_status = "Lock";
            if (lockSettings_checkBox.Checked == true)
            {
                Lock_setting_status = "Lock";
            }
            else
            {
                Lock_setting_status = "UnLock";
            }
            LockSetting(Lock_setting_status);
        }

        private void LockSetting(string status)
        {
            if (status == "Lock")
            {
                txtBox_zplPath.Enabled = false;
                txtBox_prtPath.Enabled = false;
                label_zplTemp.Enabled = false;
            }
            else if (status == "UnLock")
            {
                txtBox_zplPath.Enabled = true;
                txtBox_prtPath.Enabled = true;
                label_zplTemp.Enabled = true;
            }
        }

        // 实现ini配置加载接口
        public void LoadIni(string iniFilePath)
        {
            // 复用原有LoadIniSettings逻辑
            LoadIniSettings(iniFilePath);
        }

        // 实现IIni配置保存接口
        public void SaveIni(string iniFilePath)
        {
            try
            {
                var myIni = new IniFile(iniFilePath);

                // 保存文本框配置
                myIni.Write("filePathZPL", txtBox_zplPath.Text, "Textbox");
                myIni.Write("prtName", txtBox_prtPath.Text, "Textbox");
                myIni.Write("zplTemplate", zplTemplatePath, "Textbox");
                myIni.Write("seriPortNum", cobBox_SeriPortNum.Text, "Textbox");

                // 保存复选框状态（与加载时对应）
                myIni.Write("lockSettings", lockSettings_checkBox.Checked.ToString(), "Checkbox");

                Console.WriteLine("Form1配置保存成功");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Form1配置保存失败: {ex.Message}");
                if (!lockSettings_checkBox.Checked)
                    MessageBox.Show($"保存配置失败: {ex.Message}");
            }
        }


        // 从ini读出数据到页面
        // static 静态类，不用创建实例，只需要通过(类名.方法名)即可进行调用
        public void LoadIniSettings(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            // string Read(string Key,string Section = null)
            string filePathZPL = myIni.ReadString("filePathZPL", "Textbox");
            string prtName = myIni.ReadString("prtName", "Textbox");
            string zplTemplate = myIni.ReadString("zplTemplate", "Textbox");
            string seriPortNum = myIni.ReadString("seriPortNum", "Textbox");

            txtBox_zplPath.Text = filePathZPL;
            txtBox_prtPath.Text = prtName;
            cobBox_SeriPortNum.Text = seriPortNum;
            zplTemplatePath = zplTemplate;
            label_zplTemp.Text = zplTemplate.Substring(zplTemplate.LastIndexOf("\\") + 1);

            // 加载ZPL模板
            try
            {
                cmd_template = new StringBuilder();
                cmd_template.Clear();
                cmd_template.Append(LoadZplTemplate(zplTemplate));
                if (!(cmd_template.ToString() == ""))
                {
                    label_zplTemp.Text = zplTemplate.Substring(zplTemplate.LastIndexOf("\\") + 1);
                }
                else
                {
                    label_zplTemp.Text = "错误的模板";
                }
            }
            catch (Exception)
            {
                return;
            }

            // 打开串口
            try
            {
                openSerial_btn_Click(null, null);
            }
            catch (Exception)
            {
                return;
            }
        }

        // 将页面的数据写入ini保存
        public void SaveIniSettings(string iniFile)
        {
            SaveIni(iniFile);
        }

        // 刷新端口号
        private void reloadPort_btn_Click(object sender, EventArgs e)
        {
            string[] comPort = System.IO.Ports.SerialPort.GetPortNames();
            cobBox_SeriPortNum.Items.Clear();
            cobBox_SeriPortNum.Items.AddRange(comPort);
        }

        // 获取校验数据
        public string GetCheckResult()
        {
            // 直接调用helper的校验方法
            return _checkHelper.ExecuteCheck();
        }

        // 三个模块的头尾提示标签
        public void SetLbChkCode(string str) // str = "验码OK" 或 "验码NG"，"手动验码","屏蔽校验"
        {
            switch (str)
            {
                case "验码OK":
                    lb_ChkCode.Text = "验码OK";
                    lb_ChkCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(233)))), ((int)(((byte)(186)))));
                    lb_ChkCodeNote.Text = "请继续投放";
                    lb_ChkCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(233)))), ((int)(((byte)(186)))));
                    break;

                case "验码NG":
                    lb_ChkCode.Text = "验码NG";
                    lb_ChkCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    lb_ChkCodeNote.Text = "请重新验码";
                    lb_ChkCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    break;

                case "手动验码":
                    lb_ChkCode.Text = "手动验码中";
                    lb_ChkCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    lb_ChkCodeNote.Text = "请重新扫码";
                    lb_ChkCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    break;

                case "屏蔽校验":
                    lb_ChkCode.Text = "校验已屏蔽";
                    lb_ChkCode.BackColor = System.Drawing.SystemColors.GrayText;
                    lb_ChkCodeNote.Text = "校验已屏蔽";
                    lb_ChkCodeNote.BackColor = System.Drawing.SystemColors.GrayText;
                    break;

                default:
                    break;
            }
        }

        public void SetLbReadCode(string str) // str = "读码OK" 或 "读码NG", "手动读码"
        {
            switch (str)
            {
                case "读码OK":
                    lb_ReadCode.Text = "读码OK";
                    lb_ReadCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(233)))), ((int)(((byte)(186)))));
                    lb_ReadCodeNote.Text = "请继续投放";
                    lb_ReadCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(233)))), ((int)(((byte)(186)))));
                    break;

                case "读码NG":
                    lb_ReadCode.Text = "读码NG";
                    lb_ReadCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    lb_ReadCodeNote.Text = "请确认工单已录入";
                    lb_ReadCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    break;

                case "手动读码":
                    lb_ReadCode.Text = "手动读码中";
                    lb_ReadCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    lb_ReadCodeNote.Text = "请重新读玻璃码";
                    lb_ReadCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    break;

                default:
                    break;
            }

        }

        public void SetLbPrtCode(string str) // str = "打印OK" 或 "打印NG"
        {
            switch (str)
            {
                case "打印OK":
                    lb_PrtCode.Text = "FOG ID";
                    lb_PrtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(233)))), ((int)(((byte)(186)))));
                    lb_PrtCodeNote.Text = "请继续投放";
                    lb_PrtCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(233)))), ((int)(((byte)(186)))));
                    break;

                case "打印NG":
                    lb_PrtCode.Text = "打印NG";
                    lb_PrtCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    lb_PrtCodeNote.Text = "请重新打印";
                    lb_PrtCodeNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(111)))), ((int)(((byte)(81)))));
                    break;

                default:
                    break;
            }
        }

        // 给打印文本框传入str，用于跳过相机的情况
        public void SetPrtCode()
        {
            txtBox_prtCode.Text = txtBox_veriCode.Text;
        }

        public void ClearSerial()
        {
            txtBox_serialRead.Text = "";
        }

        public void ClearVericode()
        {
            txtBox_veriCode.Text = "";
            txtBox_prtCode.Text = "";
        }
        /// <summary>
        /// 将生成的条码设置到pictureBox
        /// </summary>
        /// <param name="barCodeStr">条形码对应的字符串</param>
        public static Image SetBarCode128(string barCodeStr, int height = 40)
        {
            try
            {
                EncodingOptions encodingOptions = new EncodingOptions();
                //encodingOptions.Width = width;
                encodingOptions.Height = 40;
                ZXing.BarcodeWriter wr = new ZXing.BarcodeWriter();
                wr.Options = encodingOptions;
                wr.Format = ZXing.BarcodeFormat.CODE_128;
                Bitmap barCodeImg = wr.Write(barCodeStr);
                return barCodeImg;
            }
            catch (Exception)
            {
                // MessageBox.Show("barCode转换出错，存在无法转换的字符");
                Console.WriteLine("barCode转换出错，存在无法转换的字符");
                return null;
            }
        }

        // 二维码扫码录入到多行文本框，从多行文本框末行取值给veriCode
        private void veriCodeHistory_txtBox_TextChanged(object sender, EventArgs e)
        {
            int maxLines = 1000;
            //int currentLines = veriCodeHistory_txtBox.Lines.Length;

            if (txtBox_veriCodeHistory.Lines.Length > 0)
            {
                if (txtBox_veriCodeHistory.Lines.Length > maxLines)
                {
                    // 截去顶行
                    //txtBox_veriCodeHistory.Text = txtBox_veriCodeHistory.Text.Substring(txtBox_veriCodeHistory.Lines[0].Length+1);
                    // veriCodeCount = veriCodeCount - 1;
                    // 光标到最后
                    txtBox_veriCodeHistory.Select(txtBox_veriCodeHistory.Text.Length, 0);
                    // 滚动条到最后
                    txtBox_veriCodeHistory.ScrollToCaret();
                }
                try
                {
                    // 将多行文本框的最后一行取出给veriCode文本框
                    string tcpReceive = txtBox_veriCodeHistory.Lines[txtBox_veriCodeHistory.Lines.Length - 2];

                    // NoRead 过滤
                    if (tcpReceive.Contains(tcpNoReadStr))
                    {
                        return;
                    }
                    txtBox_veriCode.Text = tcpReceive;
                    txtBox_prtCode.Text = tcpReceive;
                }
                catch (Exception)
                {

                }


                // 获取行数
                //veriHistoryLines = Convert.ToUInt32( veriCodeHistory_txtBox.Lines.Length);
                //veriCount_label.Text = veriCodeCount.ToString();
                // 触发信号，当二维码输入时，表示有触发信号，可执行全流程操作
                trigSigner = STATUS_WORKING;
            }
            else
            {
                txtBox_veriCode.Text = "";
            }
        }


        // 条码文本框内容变化时的处理方法,将form1的页面变化传给form3
        // 触发事件后给订阅者发布消息
        private void TxtInput_TextChanged(object sender, EventArgs e)
        {
            // 触发事件，将最新文本传递给订阅者（MainForm）
            // 使用?.运算符确保事件有订阅者时才触发，避免空引用异常
            TextUpdated?.Invoke(txtBox_prtCode.Text);
        }

        // 接收查询结果，通过mainform获得form3中的Json（由MainForm调用）
        public void ReceiveMesRootData(MesPostRoot data)
        {
            // 线程安全检查：确保在UI线程更新控件
            if (txtBox_postData.InvokeRequired)
            {
                txtBox_postData.Invoke(new Action<MesPostRoot>(ReceiveMesRootData), data);
            }
            else
            {
                // 保存接收到的数据
                _receivedMesRoot = data;

                // 显示数据到文本框（使用MesRoot的ToString方法）
                if (_receivedMesRoot != null)
                {
                    txtBox_postData.Text = $"发送给MES的数据：\n{_receivedMesRoot}";
                }
                else
                {
                    txtBox_postData.Text = "未能获取Mes设置";
                }
            }
        }

        // 加载 ZPL 模板按钮
        private void btn_loadZpl_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();   // 选择文件
            dialog.Multiselect = false; // 是否可以选择多个 文件
            dialog.InitialDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            dialog.Title = "请选择 zpl_模板 文件";
            dialog.Filter = "txt文件(*.txt)|*.txt|所有文件（*.*）|*.*";
            string file = "";
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    file = dialog.FileName;
                }
                else
                {
                    return;
                }
            }
            catch (Exception)
            { return; }

            try
            {
                cmd_template = new StringBuilder();
                cmd_template.Clear();
                string templateContent = LoadZplTemplate(file);
                cmd_template.Append(templateContent);
                // 确保路径同步更新
                if (!string.IsNullOrEmpty(templateContent))
                {
                    zplTemplatePath = file;  // 关键：更新存储路径
                    label_zplTemp.Text = file.Substring(file.LastIndexOf("\\") + 1);
                }
                else
                {
                    label_zplTemp.Text = "错误的模板";
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        // 加载 ZPL 模板文件
        private string LoadZplTemplate(string path)
        {
            StreamReader zpl_temp = new StreamReader(path, Encoding.UTF8);
            string readStr = zpl_temp.ReadToEnd();

            if (!readStr.Contains("^XA"))
            {
                Console.WriteLine("ZPL_template error,missing ^XA");
                MessageBox.Show("错误的模板，缺少起始指令：^XA");
                return "";
            }
            else if (!readStr.Contains("^XZ"))
            {
                Console.WriteLine("ZPL_template error,missing ^XZ");
                MessageBox.Show("错误的模板，缺少结束指令：^XZ");
                return "";
            }
            else if (!readStr.Contains("^FD"))
            {
                Console.WriteLine("ZPL_template error,missing ^FD");
                MessageBox.Show("错误的模板，缺少字符指令：^FD");
                return "";
            }
            else
            {
                Console.WriteLine("ZPL_template loaded success");
                //MessageBox.Show("ZPL模板加载成功");
            }
            zpl_temp.Close();
            return readStr;
        }

        // 双击控件清空内容
        private void txtBox_veriCodeHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtBox_veriCodeHistory.Clear();
        }

        // 手动读码
        private void btn_RetryRead_Click(object sender, EventArgs e)
        {
            txtBox_veriCode.Text = "";
            SetLbReadCode("手动读码");
            btn_RetryRead.Enabled = false;
            btn_RetryRead.BackColor = System.Drawing.SystemColors.ControlDark;
            timer1.Enabled = true;
            timer1.Start();
            // 触发外部委托，mainForm通知PLC读码动作
            btnRetryRead?.Invoke();
        }

        // 手动验码
        private void btn_RetryChk_Click(object sender, EventArgs e)
        {
            ClearSerial();
            SetLbChkCode("手动验码");
            seriStatus = Form1.STATUS_WAIT;
            btn_RetryChk.Enabled = false;
            btn_RetryChk.BackColor = System.Drawing.SystemColors.ControlDark;
            timer1.Enabled = true;
            timer1.Start();
            // 触发外部委托，mainForm通知PLC验码动作
            btnRetryChk?.Invoke();
        }

        // timer2定时器：手动扫码和手动验码延时，防止多次点击
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            btn_RetryChk.Enabled = true;
            btn_RetryRead.Enabled = true;
            btn_RetryRead.BackColor = System.Drawing.SystemColors.Control;
            btn_RetryChk.BackColor = System.Drawing.SystemColors.Control;
        }

        // ZPL文件路径
        private void txtBox_zplPath_TextChanged(object sender, EventArgs e)
        {
            _zplFilePath = txtBox_zplPath.Text + "\\zpl.txt";
        }

        // 打印机位置路径
        private void txtBox_prtPath_TextChanged(object sender, EventArgs e)
        {
            _mPrintName = txtBox_prtPath.Text;
        }

        #region IDataProvider接口实现
        public string GetScnCode() // 获取scn
        {
            // 确保在UI线程访问控件
            if (txtBox_scnCode.InvokeRequired)
            {
                return (string)txtBox_scnCode.Invoke(new Func<string>(() => txtBox_scnCode.Text.Trim()));
            }
            return txtBox_scnCode.Text.Trim();
        }

        public string GetPrtCode()// 获取prt
        {
            if (txtBox_prtCode.InvokeRequired)
            {
                return (string)txtBox_prtCode.Invoke(new Func<string>(() => txtBox_prtCode.Text.Trim()));
            }
            return txtBox_prtCode.Text.Trim();
        }

        public bool GetIgnoreCheck() // 获取是否忽略校验
        {
            return ignoreCheck;
        }

        #endregion
    }
}