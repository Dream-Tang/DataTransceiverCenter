using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions; // 正则表达式
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;

namespace Data_Transceiver_Center
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private StringBuilder cmd_template = new StringBuilder("");     // 打印机指令内容，可变字符串类型

        private string mesAddr =   "http://" +  "192.168.50.7:7199";
        private static string apiToken =  "/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=";
        private static string mes1Par = "position&par=";
        private static string mes2Par = "print&par=";
        private static string mes3Par = "printCallBack&par=";
        private static string testHttpUrl = "http://www.kuaidi100.com/query?type=shunfeng&postid=367847964498";

        public bool testHttpAPI = false;   // HttpApi 通信功能测试后门，通过ini加载为true时，url使用testHttpUrl
        public string zplTemplatePath = System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        public bool ignoreCheck = false;

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

        string checkResult = "";

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
            string line = txtBox_prtCode.Text;
            CmdToTxt(filePathZPL, line);
        }

        // 发送文件到打印机
        public void sendToPrt_btn_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.txtBox_zplPath.Text + "\\zpl.txt";
            string prtName = this.txtBox_prtPath.Text;
            SendFileToPrinter(filePathZPL, prtName);
        }

        // testApi获取Json数据
        private void button3_Click(object sender, EventArgs e)
        {
            // http的 Api 接口
            string url = "http://www.kuaidi100.com/query?type=shunfeng&postid=367847964498";

            // 将接口传入 httpUitls的类
            string getJson = HttpUitls.Get(url);

            MessageBox.Show(getJson);
        }

        // 解析Json数据
        private void button4_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            string postid = Convert.ToString(rnd.Next(999999)) + Convert.ToString(rnd.Next(999999));
            //我们的接口
            string url = "http://www.kuaidi100.com/query?type=shunfeng&postid=" + postid;

            //将接口传入，这个HttpUitls的类，有兴趣可以研究下，也可以直接用就可以，不用管如何实现。
            string getJson = HttpUitls.Get(url);

            //这个需要引入Newtonsoft.Json这个DLL并using
            //传入我们的实体类还有需要解析的JSON字符串这样就OK了。然后就可以通过实体类使用数据了。
            testApiRoot rt = JsonConvert.DeserializeObject<testApiRoot>(getJson);

            this.txtBox_jsonMsg.Text = rt.nu;
            this.txtBox_fogId.Text = rt.nu;
            //这样就可以取出json数据里面的值
            //MessageBox.Show("com=" + rt.com + "\r\n" + "condition=" + rt.condition + "\r\n" + "ischeck=" + rt.ischeck + "\r\n" + "state=" + rt.state + "\r\n" + "status=" + rt.status);
            //由于这个JSON字符串的 public List<DataItem> data 是一个集合，所以我们需要遍历集合里面的所有数据
            for (int i = 0; i < rt.data.Count; i++)
            {
                this.txtBox_jsonMsg.Text = rt.data[i].context;
                //MessageBox.Show("Data=" + rt.data[i].context + "\r\n" + rt.data[i].location + "\r\n" + rt.data[i].time + "\r\n" + rt.data[i].ftime);
            }
        }

        // 生成Json数据
        private void button5_Click(object sender, EventArgs e)
        {
        }

        // api test 按钮，直接发送mes api 文本框中的内容
        private void apiTest_btn_Click(object sender, EventArgs e)
        {
            string api_url = txtBox_mesApi.Text;
            Task t1 = new Task(() => 
            {
                // 耗费时间的操作
                string getJson = HttpUitls.Get(api_url);
                // 跨线程修改UI，使用methodinvoker工具类
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    txtBox_jsonMsg.Text = "Api Test:\r\n" + getJson;
                });
                this.BeginInvoke(mi);
            });
            t1.Start();
        }

        // Mes通信1
        private void mesCmd1_btn_Click(object sender, EventArgs e)
        {
            MesRoot1 rt = new MesRoot1();
            MesData1 dt = new MesData1();
            rt.data = dt;

            #region SetUrl1
            // http 接口
            string mesAddr = "http://" +  txtBox_mesAddr.Text;
            string api_url = mesAddr + apiToken +  mes1Par  + txtBox_position.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (txtBox_position.Text == "")
            {
                if (!lockSettings_checkBox.Checked)
                {
                    MessageBox.Show("线别未设置");
                }
                txtBox_jsonMsg.Text = "Mes1:\r\n 线别未设置";
                return;
            }
            else { txtBox_mesApi.Text = api_url; }
            #endregion

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t1 = new Task(() =>
            {
                // 耗费时间的操作
                string getJson = HttpUitls.Get(api_url);
                mes1Status = STATUS_WORKING;
                Console.WriteLine("mes1Status: {0}", mes1Status);

                // 跨线程修改UI，使用methodinvoker工具类
                MethodInvoker mi = new MethodInvoker(() =>
                    {
                        txtBox_jsonMsg.Text = "Mes1:\r\n" + getJson;
                        if (getJson == "无法连接到远程服务器")
                        {
                            txtBox_mesId.Text = getJson;
                            this.mes1Status = CONNECT_EXCEPTION;
                            Console.WriteLine("mes1Status: {0}", mes1Status);
                        }
                        else
                        {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                            {
                                rt = JsonConvert.DeserializeObject<MesRoot1>(getJson);
                                txtBox_mesId.Text = rt.data.id;
                                this.mes1Status = STATUS_WAIT;
                                Console.WriteLine("mes1Status: {0}", mes1Status);
                            }
                            catch (Exception)
                            {
                                this.mes1Status = CONVERT_EXCEPTION;
                                Console.WriteLine("mes1Status: {0}", mes1Status);
                                if (!this.lockSettings_checkBox.Checked) // 自动模式关闭才出弹窗
                                {
                                    MessageBox.Show("JsonConver解析出错");
                                }
                                txtBox_mesId.Text = "MES未回复Line ID";
                            }
                        }
                    });
                this.BeginInvoke(mi);
            });
            t1.Start();

        }

        // Mes通信2
        private void mesCmd2_btn_Click(object sender, EventArgs e)
        {
            MesRoot2 rt = new MesRoot2();
            MesData2 dt = new MesData2();
            rt.data = dt;

            // http 接口
            string mesAddr = "http://" + txtBox_mesAddr.Text;
            string api_url = mesAddr + apiToken + mes2Par + txtBox_veriCode.Text + ',' + txtBox_mesId.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (txtBox_veriCode.Text == "")
            {
                if (!lockSettings_checkBox.Checked)
                { MessageBox.Show("还未获取玻璃码"); }
                txtBox_jsonMsg.Text = "Mes2:\r\n 还未获取玻璃码";
                return;
            }
            else { txtBox_mesApi.Text = api_url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t2 = new Task(() =>
            {
                string getJson = HttpUitls.Get(api_url);
                this.mes2Status = STATUS_WORKING;
                Console.WriteLine("mes2Status: {0}", mes2Status);

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    txtBox_jsonMsg.Text = "Mes2:\r\n" + getJson;
                    if (getJson == "无法连接到远程服务器")
                    {
                        txtBox_fogId.Text = getJson;
                        this.mes2Status = CONNECT_EXCEPTION;
                        Console.WriteLine("mes2Status: {0}", mes2Status);
                    }
                    else
                    {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                        {
                            rt = JsonConvert.DeserializeObject<MesRoot2>(getJson);
                            txtBox_fogId.Text = rt.data.fogId;
                            this.mes2Status = STATUS_WAIT;
                            Console.WriteLine("mes2Status: {0}", mes2Status);
                        }
                        catch (Exception)
                        {
                            this.mes2Status = CONVERT_EXCEPTION;
                            Console.WriteLine("mes2Status: {0}", mes2Status);
                            if (!this.lockSettings_checkBox.Checked) // 自动模式关闭才出弹窗
                            {
                                MessageBox.Show("JsonConver解析出错");
                            }
                            txtBox_fogId.Text = "MES未回复FOG ID";
                        }
                    }
                });
                this.BeginInvoke(mi);
            });
            t2.Start();
        }

        // Mes通信3
        private void mesCmd3_btn_Click(object sender, EventArgs e)
        {
            MesRoot3 rt = new MesRoot3();

            // http 接口
            string mesAddr = "http://" + txtBox_mesAddr.Text;
            string api_url = mesAddr + apiToken + mes3Par + txtBox_fogId.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (txtBox_fogId.Text == "")
            {
                if (!lockSettings_checkBox.Checked)
                {
                    MessageBox.Show("fogID未获取");
                }
                txtBox_jsonMsg.Text = "Mes3:\r\n fogID未获取";
                return;
            }
            else { txtBox_mesApi.Text = api_url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t3 = new Task(() =>
            {
                string getJson = HttpUitls.Get(api_url);
                this.mes3Status = STATUS_WORKING;
                Console.WriteLine("mes3Status: {0}", mes3Status);

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    txtBox_jsonMsg.Text = "Mes3:\r\n" + getJson;
                    if (getJson == "无法连接到远程服务器")
                    {
                        txtBox_fogId.Text = getJson;
                        this.mes3Status = CONNECT_EXCEPTION;
                        Console.WriteLine("mes3Status: {0}", mes3Status);
                    }
                    else
                    {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                        {
                            rt = JsonConvert.DeserializeObject<MesRoot3>(getJson);
                            txtBox_jsonMsg.Text = rt.data;
                            this.mes3Status = STATUS_WAIT;
                            Console.WriteLine("mes3Status: {0}", mes3Status);
                        }
                        catch (Exception)
                        {
                            this.mes3Status = CONVERT_EXCEPTION;
                            Console.WriteLine("mes3Status: {0}", mes3Status);
                            if (!this.lockSettings_checkBox.Checked) // 自动模式关闭才出弹窗
                            {
                                MessageBox.Show("JsonConver解析出错");
                            }
                        }
                    }
                });
                this.BeginInvoke(mi);
            });
            t3.Start();
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
                    cobBox_SeriPortNum.Enabled = false;
                    comboBox2.Enabled = false;
                }
                else
                {
                    serialPort1.Close();
                    btn_openSerial.Text = "打开串口";
                    serialPort_label.Text = "串口已关闭";
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

        // 串口中断事件：当有数据收到时执行。将收到的数据按ASCII转换显示
        private void SerialPort1_DataRecived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                // textBox3 读出串口缓存内的数据，textBox4 将string数据转换成16进制byte，然后按ASCII转换成string
                //string portData = serialPort1.ReadExisting(); // 读所有缓存数据
                string portData = serialPort1.ReadTo("\r\n");

                seriStatus = STATUS_READY;

                // 因为要访问UI资源，所以需要使用invoke方式同步ui

                // 跨线程修改UI，使用methodinvoker工具类
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    txtBox_serialRead.Clear();
                    txtBox_serialRead.Text = portData.Trim(); //  移出头部和尾部空白字符
                });
                BeginInvoke(mi);
            }
            catch (Exception ex)
            {
                if (!this.lockSettings_checkBox.Checked)
                {
                    MessageBox.Show(ex.Message);
                }
                seriStatus = STATUS_WAIT;
                return;
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
            CheckScnPrtCode();
        }

        // 打印码 文本框变化
        private void prtCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            // CheckScnPrtCode();
            pictureBox1.Image = SetBarCode128(txtBox_prtCode.Text);
        }

        // FOG ID（MES2） 文本框变化
        private void fogId_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (txtBox_fogId.Text == "解析出错")
            {
                return;
            }
            if (txtBox_prtCode.Text != "")
            {
                lastPrtCode_label.Text = txtBox_prtCode.Text;  // 保存旧值
            }
            txtBox_prtCode.Text = txtBox_fogId.Text;  // 传入新值
        }

        // 将ZPL指令输出到txt文档
        private void CmdToTxt(string filePathZPL, string line)
        {
            // ZPL文件生成，使用模板来生成，替换模板中 ^FD到^FS之间的文本
            string zpl_cmd = cmd_template.ToString();
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

                cmd_template.Remove(index_FD, index_FS-index_FD);// 移出模板中^FD到^FS之间的内容
                cmd_template.Insert(index_FD, line);

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
                    sw.Write(cmd_template.ToString());
                    sw.Close();
                }
                runStatus_lable.Text = "zpl文件生产成功";
               
                pictureBox1.Image = SetBarCode128(line);
                //MessageBox.Show("zpl文件生产成功，文件位置："+ filePathZPL);
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
        private void SendFileToPrinter(string filePathZPL, string mPrintName)
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
        private void AutoSendFile(string filePathZPL, string mPrintName)
        {
            string prtCode = txtBox_prtCode.Text;

            // 有打印码，才进行打印；打印完成后清楚prtCode
            if (prtCode != "")
            {
                CmdToTxt(filePathZPL, prtCode);

                SendFileToPrinter(filePathZPL, mPrintName);

                lastPrtCode_label.Text = prtCode;
            }
            else
            {
                runStatus_lable.Text = "wait prtCode";
            }

        }

        // 自动模式：
        // 流程：读CSV->mes1->mes2->  生成打印指令->发送打印->mes3->串口(Event)
        // task0：读PLC
        // task1：（启动条件）读csv=》mes1=》mes2
        // async task2：（启动条件）makeZPL=》发送ZPL=》await mes3
        // task3：refalsh()=>methedInvoke();
        private void AutoRunMode()
        {

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

        public void refreshPLC(short cam, short prt, short scn)
        {
            this.camValue_label.Text = Convert.ToString(cam);
            this.prtValue_label.Text = Convert.ToString(prt);
            this.scnValue_label.Text = Convert.ToString(scn);
        }

        public void refreshMes1(string api, string getJson, string mesID)
        {
            this.txtBox_mesApi.Text = api;
            this.txtBox_jsonMsg.Text = "Mes1:\r\n" + getJson;
            this.txtBox_mesId.Text = mesID;
        }

        public void refreshMes2(string api, string getJson, string fogID)
        {
            this.txtBox_mesApi.Text = api;
            this.txtBox_jsonMsg.Text = "Mes2:\r\n" + getJson;
            this.txtBox_fogId.Text = fogID;
        }

        public void refreshMes3(string api, string getJson)
        {
            this.txtBox_mesApi.Text = api;
            this.txtBox_jsonMsg.Text = "Mes3:\r\n" + getJson;
        }

        public string GetUrl(string addr, string pluginarg = "", string par = "", string mesApi = "")
        {
            // http 接口。若直接参数传入mesApi，则直接用mesApi，若没有直接传入，则根据参数来合成。
            string api_url = "http://" + addr +  apiToken + pluginarg + "&par=" + par;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI)
            {
                api_url = testHttpUrl;
            }
            if (mesApi != "")
            {
                api_url = mesApi;
            }
            return api_url;
        }

        public string GetMesAddr()
        {
            string addr;
            addr = this.txtBox_mesAddr.Text;
            return addr;
        }

        public string GetMes1prt()
        {
            string position;
            position = this.txtBox_position.Text;
            return position;
        }

        public string GetMes2prt()
        {
            string mesId;
            mesId = this.txtBox_veriCode.Text + ',' + this.txtBox_mesId.Text;
            return mesId;
        }

        public string GetMes3prt()
        {
            string fogId;
            fogId = this.txtBox_fogId.Text;
            return fogId;
        }

        public string GetMesApi()
        {
            string mesApi;
            mesApi = this.txtBox_mesApi.Text;
            return mesApi;
        }

        public void sentTo()
        {
            makeZpl_btn_Click(null, null);
            sendToPrt_btn_Click(null, null);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            Console.WriteLine(System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff:ffffff"));
            Console.WriteLine("timer1 触发");
            string filePathZPL = txtBox_zplPath.Text + "\\zpl.txt";
            string mPrintName = txtBox_prtPath.Text;

            Task t1 = new Task(() =>
            {
                AutoSendFile(filePathZPL, mPrintName);
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    //refreshPLC();
                });
            });
            t1.Start();
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
                txtBox_mesAddr.Enabled = false;
                txtBox_position.Enabled = false;
                label_zplTemp.Enabled = false;
            }
            else if (status == "UnLock")
            {
                timer1.Enabled = false;
                txtBox_zplPath.Enabled = true;
                txtBox_prtPath.Enabled = true;
                txtBox_mesAddr.Enabled = true;
                txtBox_position.Enabled = true;
                label_zplTemp.Enabled = true;
            }
        }

        // 从ini读出数据到页面
        // static 静态类，不用创建实例，只需要通过(类名.方法名)即可进行调用
        public void LoadIniSettings(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            // string Read(string Key,string Section = null)
            string filePathZPL = myIni.Read("filePathZPL", "Form1");
            string prtName = myIni.Read("prtName", "Form1");
            string mesAddr = myIni.Read("mesAddr", "Form1");
            string lineCount = myIni.Read("lineCount","Form1");
            string testHttp = myIni.Read("testHttp", "Form1");
            string zplTemplate = myIni.Read("zplTemplate","Form1");
            string seriPortNum = myIni.Read("seriPortNum","Form1");

            txtBox_zplPath.Text = filePathZPL;
            txtBox_prtPath.Text = prtName;
            txtBox_mesAddr.Text = mesAddr;
            txtBox_position.Text = lineCount;
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
                openSerial_btn_Click(null,null);
            }
            catch (Exception)
            {
                return;
            }

            this.testHttpAPI = Convert.ToBoolean(testHttp);
        }

        // 将页面的数据写入ini保存
        public void SaveIniSettings(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            string filePathZPL = txtBox_zplPath.Text;
            string prtName = txtBox_prtPath.Text ;
            string mesAddr = txtBox_mesAddr.Text ;
            string lineCount = txtBox_position.Text ;
            string seriPortNum = cobBox_SeriPortNum.Text;
            string testHttp = "False" ; // 确保每次保存ini后关闭调试模式

            myIni.Write("filePathZPL", filePathZPL, "Form1");
            myIni.Write("prtName", prtName, "Form1");
            myIni.Write("mesAddr", mesAddr, "Form1");
            myIni.Write("lineCount", lineCount, "Form1");
            myIni.Write("testHttp", testHttp, "Form1");
            myIni.Write("zplTemplate", zplTemplatePath,"Form1");
            myIni.Write("seriPortNum", seriPortNum, "Form1");
        }

        // 刷新端口号
        private void reloadPort_btn_Click(object sender, EventArgs e)
        {
            string[] comPort = System.IO.Ports.SerialPort.GetPortNames();
            cobBox_SeriPortNum.Items.Clear();
            cobBox_SeriPortNum.Items.AddRange(comPort);
        }

        // 进行一次验码
        private string CheckScnPrtCode()
        {
            if (ignoreCheck)    // 屏蔽校验
            {
                checkResult = "OK";
                SetLbChkCode(CommunicationProtocol.chkCodeIG);
                Console.WriteLine("    Check result:ignore");
            }
            else
            {
                // prtCode和ScnCode都不为空时，进行一次校验
                if ((txtBox_prtCode.Text != "") & (txtBox_scnCode.Text != ""))
                {
                    if (txtBox_scnCode.Text == txtBox_prtCode.Text)
                    {
                        checkResult = "OK";
                        SetLbChkCode(CommunicationProtocol.chkCodeOK);
                        Console.WriteLine("    Check result:OK");
                    }
                    else
                    {
                        checkResult = "NG";
                        SetLbChkCode(CommunicationProtocol.chkCodeNG);
                        Console.WriteLine("    Check result:NG");
                    }
                }
                // 校验完成后清空旧数据

                //seriStatus = STATUS_WAIT;

                lastPrtCode_label.Text = txtBox_prtCode.Text;
                //txtBox_scnCode.Text = "";
                //txtBox_prtCode.Text = "";
            }
            return checkResult;
        }

        // 获取校验数据，public对外接口
        public string GetCheckResult()
        {
            string chckResult;
            chckResult = CheckScnPrtCode();
            return chckResult;
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

        public void ClearSerial()
        {
            txtBox_serialRead.Text = "";
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
            int maxLines =1000;
            //int currentLines = veriCodeHistory_txtBox.Lines.Length;

            if (txtBox_veriCodeHistory.Lines.Length > 0)
            {
                if (txtBox_veriCodeHistory.Lines.Length > maxLines)
                {
                    // 截去顶行
                    //txtBox_veriCodeHistory.Text = txtBox_veriCodeHistory.Text.Substring(txtBox_veriCodeHistory.Lines[0].Length+1);
                    // veriCodeCount = veriCodeCount - 1;
                    // 光标到最后
                    txtBox_veriCodeHistory.Select(txtBox_veriCodeHistory.Text.Length,0);
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

        // 加载ZPL模板按钮
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
                cmd_template.Append(LoadZplTemplate(file));
                if (!(cmd_template.ToString()==""))
                {
                    zplTemplatePath = file.ToString();
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
                MessageBox.Show("ZPL模板加载成功");
            }
            zpl_temp.Close();
            return readStr;
        }

        // 双击控件清空内容
        private void txtBox_veriCodeHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtBox_veriCodeHistory.Clear();
        }

        private void btn_RetryRead_Click(object sender, EventArgs e)
        {
            SetLbReadCode("手动读码");
            btn_RetryRead.Enabled = false;
            btn_RetryRead.BackColor = System.Drawing.SystemColors.ControlDark;
            timer2.Enabled = true;
            timer2.Start();
            // 接收外部的委托：
            btnRetryRead?.Invoke();
        }

        private void btn_RetryChk_Click(object sender, EventArgs e)
        {
            ClearSerial();
            SetLbChkCode("手动验码");
            seriStatus = Form1.STATUS_WAIT;
            btn_RetryChk.Enabled = false;
            btn_RetryChk.BackColor = System.Drawing.SystemColors.ControlDark;
            timer2.Enabled = true;
            timer2.Start();
            // 接收外部的委托:
            btnRetryChk?.Invoke();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            btn_RetryChk.Enabled = true;
            btn_RetryRead.Enabled = true;
            btn_RetryRead.BackColor = System.Drawing.SystemColors.Control;
            btn_RetryChk.BackColor = System.Drawing.SystemColors.Control;
        }

    }
}