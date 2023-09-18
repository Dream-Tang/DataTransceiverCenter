using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Text;
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

        private StringBuilder cmd;     // 打印机指令内容，可变字符串类型

        private string mesAddr =   "https://" +  "192.168.50.7:7199";
        private static string apiToken =  "/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=";
        private static string mes1Par = "position&par=";
        private static string mes2Par = "print&par=";
        private static string mes3Par = "printCallBack&par=";
        private static string testHttpUrl = "http://www.kuaidi100.com/query?type=shunfeng&postid=367847964498";

        public bool testHttpAPI = false;   // HttpApi 通信功能测试后门，通过ini加载为true时，url使用testHttpUrl

        // 通信和流程标志位状态机，0初始化，1进行中，2完成，3异常
        private int mes1Status = STATUS_WAIT;
        private int mes2Status = STATUS_WAIT;
        private int mes3Status = STATUS_WAIT;
        private int prtStatus = STATUS_WAIT;

        // 通信标志位数值定义
        private const int STATUS_WAIT = -1;
        private const int STATUS_READY = 0;
        private const int STATUS_WORKING = 1;
        private const int STATUS_COMPLETE = 2;
        private const int STATUS_EXCEPTION = 3;
        private const int CONNECT_EXCEPTION = 4;
        private const int CONVERT_EXCEPTION = 5;

        // 生成ZPL文档
        public void makeZpl_btn_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.zplPath_txtBox.Text + "\\zpl.txt";
            string line = prtCode_txtBox.Text;
            CmdToTxt(filePathZPL, line);
        }

        // 发送文件到打印机
        public void sendToPrt_btn_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.zplPath_txtBox.Text + "\\zpl.txt";
            string prtName = this.prtPath_txtBox.Text;
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

            this.JsonMsg_txtBox.Text = rt.nu;
            this.fogId_txtBox.Text = rt.nu;
            //这样就可以取出json数据里面的值
            //MessageBox.Show("com=" + rt.com + "\r\n" + "condition=" + rt.condition + "\r\n" + "ischeck=" + rt.ischeck + "\r\n" + "state=" + rt.state + "\r\n" + "status=" + rt.status);
            //由于这个JSON字符串的 public List<DataItem> data 是一个集合，所以我们需要遍历集合里面的所有数据
            for (int i = 0; i < rt.data.Count; i++)
            {
                this.JsonMsg_txtBox.Text = rt.data[i].context;
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
            string api_url = mesApi_txtBox.Text;
            Task t1 = new Task(() => 
            {
                // 耗费时间的操作
                string getJson = HttpUitls.Get(api_url);
                // 跨线程修改UI，使用methodinvoker工具类
                MethodInvoker mi = new MethodInvoker(() =>
                {
                    JsonMsg_txtBox.Text = "Api Test:\r\n" + getJson;
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
            string mesAddr = "https://" +  mesAddr_txtBox.Text;
            string api_url = mesAddr + apiToken +  mes1Par  + position_txtBox.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (position_txtBox.Text == "")
            {
                if (!autoRun_checkBox.Checked)
                {
                    MessageBox.Show("线别未设置");
                }
                JsonMsg_txtBox.Text = "Mes1:\r\n 线别未设置";
                return;
            }
            else { mesApi_txtBox.Text = api_url; }
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
                        JsonMsg_txtBox.Text = "Mes1:\r\n" + getJson;
                        if (getJson == "无法连接到远程服务器")
                        {
                            mesId_txtBox.Text = getJson;
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
                                mesId_txtBox.Text = rt.data.id;
                                this.mes1Status = STATUS_WAIT;
                                Console.WriteLine("mes1Status: {0}", mes1Status);
                            }
                            catch (Exception)
                            {
                                this.mes1Status = CONVERT_EXCEPTION;
                                Console.WriteLine("mes1Status: {0}", mes1Status);
                                if (!this.autoRun_checkBox.Checked) // 自动模式关闭才出弹窗
                            {
                                    MessageBox.Show("JsonConver解析出错");
                                }
                                mesId_txtBox.Text = "##############";
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
            string mesAddr = "https://" + mesAddr_txtBox.Text;
            string api_url = mesAddr + apiToken + mes2Par + veriCode_txtBox.Text + ',' + mesId_txtBox.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (veriCode_txtBox.Text == "")
            {
                if (!autoRun_checkBox.Checked)
                { MessageBox.Show("视觉码未获取"); }
                JsonMsg_txtBox.Text = "Mes2:\r\n 视觉码未获取";
                return;
            }
            else { mesApi_txtBox.Text = api_url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t2 = new Task(() =>
            {
                string getJson = HttpUitls.Get(api_url);
                this.mes2Status = STATUS_WORKING;
                Console.WriteLine("mes2Status: {0}", mes2Status);

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    JsonMsg_txtBox.Text = "Mes2:\r\n" + getJson;
                    if (getJson == "无法连接到远程服务器")
                    {
                        fogId_txtBox.Text = getJson;
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
                            fogId_txtBox.Text = rt.data.fogId;
                            this.mes2Status = STATUS_WAIT;
                            Console.WriteLine("mes2Status: {0}", mes2Status);
                        }
                        catch (Exception)
                        {
                            this.mes2Status = CONVERT_EXCEPTION;
                            Console.WriteLine("mes2Status: {0}", mes2Status);
                            if (!this.autoRun_checkBox.Checked) // 自动模式关闭才出弹窗
                            {
                                MessageBox.Show("JsonConver解析出错");
                            }
                            fogId_txtBox.Text = "##############";
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
            string mesAddr = "https://" + mesAddr_txtBox.Text;
            string api_url = mesAddr + apiToken + mes3Par + fogId_txtBox.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (fogId_txtBox.Text == "")
            {
                if (!autoRun_checkBox.Checked)
                {
                    MessageBox.Show("fogID未获取");
                }
                JsonMsg_txtBox.Text = "Mes3:\r\n fogID未获取";
                return;
            }
            else { mesApi_txtBox.Text = api_url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t3 = new Task(() =>
            {
                string getJson = HttpUitls.Get(api_url);
                this.mes3Status = STATUS_WORKING;
                Console.WriteLine("mes3Status: {0}", mes3Status);

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    JsonMsg_txtBox.Text = "Mes3:\r\n" + getJson;
                    if (getJson == "无法连接到远程服务器")
                    {
                        fogId_txtBox.Text = getJson;
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
                            JsonMsg_txtBox.Text = rt.data;
                            this.mes3Status = STATUS_WAIT;
                            Console.WriteLine("mes3Status: {0}", mes3Status);
                        }
                        catch (Exception)
                        {
                            this.mes3Status = CONVERT_EXCEPTION;
                            Console.WriteLine("mes3Status: {0}", mes3Status);
                            if (!this.autoRun_checkBox.Checked) // 自动模式关闭才出弹窗
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
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                    serialPort1.Open();
                    openSerial_btn.Text = "关闭串口";
                    serialPort_label.Text = "串口已打开";
                }
                else
                {
                    serialPort1.Close();
                    openSerial_btn.Text = "打开串口";
                    serialPort_label.Text = "串口已关闭";
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

        private void LockSetting(string status)
        {
            if (status  == "Lock")
            {
                zplPath_txtBox.Enabled = false;
                prtPath_txtBox.Enabled = false;
                mesAddr_txtBox.Enabled = false;
                position_txtBox.Enabled = false;
            }
            else if (status == "UnLock")
            {
                timer1.Enabled = false;
                zplPath_txtBox.Enabled = true;
                prtPath_txtBox.Enabled = true;
                mesAddr_txtBox.Enabled = true;
                position_txtBox.Enabled = true;
            }
        }


        // 自动选项打开
        private void autoRun_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            string Lock_setting_status = "Lock";
           if(autoRun_checkBox.Checked == true)
            {
                Lock_setting_status = "Lock";
            }
            else
            {
                Lock_setting_status = "UnLock";
            }
            LockSetting(Lock_setting_status);
        }

        private void serialRead_txtBox_TextChanged(object sender, EventArgs e)
        {
            scnCode_txtBox.Text = serialRead_txtBox.Text;
            CheckScnPrtCode();
        }

        private void fogId_txtBox_TextChanged(object sender, EventArgs e)
        {
            if (prtCode_txtBox.Text != "")
            {
                lastPrtCode_label.Text = prtCode_txtBox.Text;  // 保存旧值
            }
            prtCode_txtBox.Text = fogId_txtBox.Text;  // 传入新值
        }

        private void prtCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            CheckScnPrtCode();
            pictureBox1.Image = SetBarCode128(prtCode_txtBox.Text);
        }

        private void scnCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            CheckScnPrtCode();
        }

        // 将ZPL指令输出到txt文档
        // TO DO：后续尝试通过加载模板的方式来更改打印的格式设置
        private void CmdToTxt(string filePathZPL, string line)
        {
            cmd = new StringBuilder();
            cmd.AppendLine("^XA");
            cmd.AppendLine("^FO150,100^BY3");
            cmd.AppendLine("^B3N,20,A,A");
            cmd.AppendLine("^FD" + line + "^FS");
            cmd.AppendLine("^XZ");

            // 异常处理，可能会出现文件路径出错的情况
            try
            {
                // 保存到本地文件，先清空文件
                System.IO.File.WriteAllText(filePathZPL, string.Empty);
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePathZPL, false, Encoding.UTF8))
                {
                    sw.Write(cmd.ToString());
                    sw.Close();
                }
                runStatus_lable.Text = "zpl文件生产成功";
               
                pictureBox1.Image = SetBarCode128(line);
                //MessageBox.Show("zpl文件生产成功，文件位置："+ filePathZPL);
            }
            catch (Exception)
            {
                if (!this.autoRun_checkBox.Checked) // 自动模式关闭才出弹窗
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
                if (!this.autoRun_checkBox.Checked) // 自动模式关闭才出弹窗
                {
                    MessageBox.Show(ex.Message);
                }
                runStatus_lable.Text = "发送打印机失败";
            }

        }

        // 打印条码功能：等待prtCode（打印码来自Mes2消息），有prtCode后，生成ZPL文件，发送给打印机
        private void AutoSendFile(string filePathZPL, string mPrintName)
        {
            string prtCode = prtCode_txtBox.Text;

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
            this.mesApi_txtBox.Text = api;
            this.JsonMsg_txtBox.Text = "Mes1:\r\n" + getJson;
            this.mesId_txtBox.Text = mesID;
        }

        public void refreshMes2(string api, string getJson, string fogID)
        {
            this.mesApi_txtBox.Text = api;
            this.JsonMsg_txtBox.Text = "Mes2:\r\n" + getJson;
            this.fogId_txtBox.Text = fogID;
        }

        public void refreshMes3(string api, string getJson)
        {
            this.mesApi_txtBox.Text = api;
            this.JsonMsg_txtBox.Text = "Mes3:\r\n" + getJson;
        }

        public string GetUrl(string pluginarg = "", string par = "", string mesApi = "")
        {
            // http 接口。若直接参数传入mesApi，则直接用mesApi，若没有直接传入，则根据参数来合成。
            string api_url = apiToken + pluginarg + "&par=" + par;
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

        public string GetMes1prt()
        {
            string position;
            position = this.position_txtBox.Text;
            return position;
        }

        public string GetMes2prt()
        {
            string mesId;
            mesId = this.veriCode_txtBox.Text + ',' + this.mesId_txtBox.Text;
            return mesId;
        }

        public string GetMes3prt()
        {
            string fogId;
            fogId = this.fogId_txtBox.Text;
            return fogId;
        }

        public string GetMesApi()
        {
            string mesApi;
            mesApi = this.mesApi_txtBox.Text;
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

        // 串口中断事件：当有数据收到时执行。将收到的数据按ASCII转换显示
        private void SerialPort1_DataRecived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                // 因为要访问UI资源，所以需要使用invoke方式同步ui
                this.Invoke((EventHandler)(delegate
                  {
                      // textBox3 读出串口缓存内的数据，textBox4 将string数据转换成16进制byte，然后按ASCII转换成string
                      string portData = serialPort1.ReadExisting(); // 读所有缓存数据
                      serialRead_txtBox.Text = portData.Trim(); //  移出头部和尾部空白字符
                      //serialRead_txtBox.Text = serialPort1.ReadTo("\r"); // 读到0x0d，也就是'\r' 回车结束
                      scnCode_txtBox.Text = serialRead_txtBox.Text;

                      // 扫码枪通过串口发送过来的
                      //textBox4.Text = System.Text.Encoding.ASCII.GetString(ToBytesFromHexString(textBox3.Text));
                  })
                    );
            }
            catch (Exception ex)
            {
                if (!this.autoRun_checkBox.Checked)
                {
                    MessageBox.Show(ex.Message);
                }
                return;
            }
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
            string filePathZPL = zplPath_txtBox.Text + "\\zpl.txt";
            string mPrintName = prtPath_txtBox.Text;

            AutoSendFile(filePathZPL, mPrintName);
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

            zplPath_txtBox.Text = filePathZPL;
            prtPath_txtBox.Text = prtName;
            mesAddr_txtBox.Text = mesAddr;
            position_txtBox.Text = lineCount;

            this.testHttpAPI = Convert.ToBoolean(testHttp);
        }

        // 将页面的数据写入ini保存
        public void SaveIniSettings(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            string filePathZPL = zplPath_txtBox.Text+"\r\n";
            string prtName = prtPath_txtBox.Text + "\r\n";
            string mesAddr = mesAddr_txtBox.Text + "\r\n";
            string lineCount = position_txtBox.Text + "\r\n";
            string testHttp = "False" + "\r\n"; // 确保每次保存ini后关闭调试模式

            myIni.Write("filePathZPL", filePathZPL, "Form1");
            myIni.Write("prtName", prtName, "Form1");
            myIni.Write("mesAddr", mesAddr, "Form1");
            myIni.Write("lineCount", lineCount, "Form1");
            myIni.Write("testHttp", testHttp, "Form1");
        }

        // 刷新端口号
        private void reloadPort_btn_Click(object sender, EventArgs e)
        {
            string[] comPort = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(comPort);
        }

        private string CheckScnPrtCode()
        {
            string chckResult = "";
            // prtCode和ScnCode都不为空时，进行一次校验
            if ((prtCode_txtBox.Text != "") & (scnCode_txtBox.Text != ""))
            {
                if (scnCode_txtBox.Text == prtCode_txtBox.Text)
                {
                    chckResult_txtBox.Text = "校验 OK：扫描码与打印码一致";
                    chckResult = "OK";
                }
                else
                {
                    chckResult_txtBox.Text = "校验 NG：扫描码与打印码不同";
                    chckResult = "NG";
                }
                // 校验完成后清空旧数据

                lastPrtCode_label.Text = prtCode_txtBox.Text;

                Task t = Task.Run(() =>
                {
                    Thread.Sleep(500);
                    MethodInvoker mi = new MethodInvoker(() =>
                    {
                        if ( prtStatus == STATUS_COMPLETE)
                        {
                            prtCode_txtBox.Text = "";
                            veriCode_txtBox.Text = "";
                            scnCode_txtBox.Text = "";
                            prtStatus = STATUS_WAIT;
                            Console.WriteLine("prtStatus:"+ STATUS_WAIT);
                        }
                    });
                    BeginInvoke(mi);
                });
            }
            return chckResult;
        }

        public string GetCheckResult()
        {
            string chckResult;
            chckResult = CheckScnPrtCode();
            return chckResult;
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
            if (veriCodeHistory_txtBox.Lines.Length > 0)
            {
                veriCode_txtBox.Text = veriCodeHistory_txtBox.Lines[veriCodeHistory_txtBox.Lines.Length-1];
            }
            else
            {
                veriCode_txtBox.Text = "";
            }
        }

    }
}