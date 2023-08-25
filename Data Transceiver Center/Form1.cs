using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private StringBuilder cmd;     // 打印机指令内容，可变字符串类型

        private static string apiToken = "http://192.168.10.26:7199/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=";
        private static string testHttpUrl = "http://www.kuaidi100.com/query?type=shunfeng&postid=367847964498";

        public bool testHttpAPI = false;   // HttpApi 通信功能测试后门，通过ini加载为true时，url使用testHttpUrl

        // 通信和流程标志位状态机，0初始化，1进行中，2完成，3异常
        private int mesComunication1Flag = STATUS_WAIT;
        private int mesComunication2Flag = STATUS_WAIT;
        private int mesComunication3Flag = STATUS_WAIT;
        private int sendFileToPrtFlag = STATUS_WAIT;
        private int readCsvFlag = STATUS_WAIT;

        // 通信标志位数值定义
        private const int STATUS_WAIT = -1;
        private const int STATUS_READY = 0;
        private const int STATUS_WORKING = 1;
        private const int STATUS_EXCEPTION = 2;
        private const int CONNECT_EXCEPTION = 3;
        private const int CONVERT_EXCEPTION = 4;

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
            string postid = Convert.ToString(rnd.Next(999999))+Convert.ToString(rnd.Next(999999));
            //我们的接口
            string url = "http://www.kuaidi100.com/query?type=shunfeng&postid="+ postid;

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

        // Mes通信1
        private void mesCmd1_btn_Click(object sender, EventArgs e)
        {
            MesRoot1 rt = new MesRoot1();
            MesData1 dt = new MesData1();
            rt.data = dt;

            #region SetUrl1
            // http 接口
            string url = mesApi_txtBox.Text;
            string api_url = apiToken + "position&par=" + position_txtBox.Text;
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
            else { url = api_url; mesApi_txtBox.Text = url; }
            #endregion

            // 通过接口，向MES发送通信，收到的回应存入getJson
            if (url != "")
            {
                Task t1 = new Task(() =>
                {
                // 耗费时间的操作
                string getJson = HttpUitls.Get(url);
                mesComunication1Flag = STATUS_WORKING;

                // 跨线程修改UI，使用methodinvoker工具类
                MethodInvoker mi = new MethodInvoker(() =>
                    {
                        JsonMsg_txtBox.Text = "Mes1:\r\n" + getJson;
                        if (getJson == "无法连接到远程服务器")
                        {
                            mesId_txtBox.Text = getJson;
                            this.mesComunication1Flag = CONNECT_EXCEPTION;
                        }
                        else
                        {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                            {
                                rt = JsonConvert.DeserializeObject<MesRoot1>(getJson);
                                mesId_txtBox.Text = rt.data.id;
                                this.mesComunication1Flag = STATUS_WAIT;
                            }
                            catch (Exception)
                            {
                                this.mesComunication1Flag = CONVERT_EXCEPTION;
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
            if (testHttpAPI)
            {
                button4_Click(null, null);
            }
        }

        // Mes通信2
        private void mesCmd2_btn_Click(object sender, EventArgs e)
        {
            MesRoot2 rt = new MesRoot2();
            MesData2 dt = new MesData2();
            rt.data = dt;

            // http 接口
            string url = mesApi_txtBox.Text;
            string api_url = apiToken + "print&par=" + visionCode_txtBox.Text + "," + mesId_txtBox.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (visionCode_txtBox.Text == "") 
            {
                if (!autoRun_checkBox.Checked)
                { MessageBox.Show("视觉码未获取"); }
                JsonMsg_txtBox.Text = "Mes2:\r\n 视觉码未获取";
                return; 
            }
            else { url = api_url; mesApi_txtBox.Text = url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t2 = new Task(() =>
            {
                string getJson = HttpUitls.Get(url);
                this.mesComunication2Flag = STATUS_WORKING;

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    JsonMsg_txtBox.Text = "Mes2:\r\n" + getJson;
                    if (getJson == "无法连接到远程服务器")
                    {
                        fogId_txtBox.Text = getJson;
                        this.mesComunication2Flag = CONNECT_EXCEPTION;
                    }
                    else
                    {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                        {
                            rt = JsonConvert.DeserializeObject<MesRoot2>(getJson);
                            fogId_txtBox.Text = rt.data.fogId;
                            this.mesComunication2Flag = STATUS_WAIT;
                        }
                        catch (Exception)
                        {
                            this.mesComunication2Flag = CONVERT_EXCEPTION;
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
            string url = mesApi_txtBox.Text;
            string api_url = apiToken + "printCallBack&par=" + fogId_txtBox.Text;
            // 设置一个HttpApi测试后门，通过ini改写testHttpAPI为true时，将通过以接通网站测试Json读取。
            if (testHttpAPI) { api_url = testHttpUrl; }

            if (fogId_txtBox.Text == "") 
            {
                if (!autoRun_checkBox.Checked)
                {
                    MessageBox.Show("fogID未获取");
                }
                JsonMsg_txtBox.Text = "Mes3:\r\n fogID未获取"; 
                return; }
            else { url = api_url; mesApi_txtBox.Text = url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t3 = new Task(() =>
            {
                string getJson = HttpUitls.Get(url);
                this.mesComunication3Flag = STATUS_WORKING;

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    JsonMsg_txtBox.Text = "Mes3:\r\n" + getJson;
                    if (getJson == "无法连接到远程服务器")
                    {
                        fogId_txtBox.Text = getJson;
                        this.mesComunication3Flag = CONNECT_EXCEPTION;
                    }
                    else
                    {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                        {
                            rt = JsonConvert.DeserializeObject<MesRoot3>(getJson);
                            JsonMsg_txtBox.Text = rt.data;
                            this.mesComunication3Flag = STATUS_WAIT;
                        }
                        catch (Exception)
                        {
                            this.mesComunication3Flag = CONVERT_EXCEPTION;
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

        // 读取CSV数据
        private void readCsv_btn_Click(object sender, EventArgs e)
        {
            string csvPath = csvPath_txtBox.Text + "\\barcode.csv";
            string barCode = ReadCsvFile(csvPath);
            visionCode_txtBox.Text = barCode;

            if (deleCsv_checkBox.Checked)
            {
                File.Delete(csvPath);
            }
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
                    //openSerial_btn.BackColor = System.Drawing.Color.Khaki;
                    //serialPort_label.BackColor = System.Drawing.Color.Khaki;
                }
                else
                {
                    serialPort1.Close();
                    openSerial_btn.Text = "打开串口";
                    serialPort_label.Text = "串口已关闭";
                    //openSerial_btn.BackColor = System.Drawing.Color.Transparent;
                    //serialPort_label.BackColor = System.Drawing.Color.Transparent;
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

        // 自动选项打开
        private void autoRun_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.autoRun_checkBox.Checked)
            {
                MessageBox.Show("自动模式");
                zplPath_txtBox.Enabled = false;
                prtPath_txtBox.Enabled = false;
                csvPath_txtBox.Enabled = false;
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Enabled = false;
                zplPath_txtBox.Enabled = true;
                prtPath_txtBox.Enabled = true;
                csvPath_txtBox.Enabled = true;
            }
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
                prtCode_label.Text = prtCode_txtBox.Text;  // 保存旧值
            }
            prtCode_txtBox.Text = fogId_txtBox.Text;  // 传入新值
            CheckScnPrtCode();
        }
        private void scnCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            CheckScnPrtCode();
        }
        // 将ZPL指令输出到txt文档
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
                runStatus_lable.Text = "发送成功!";
            }
            catch (Exception ex)
            {
                this.sendFileToPrtFlag = 3;
                if (!this.autoRun_checkBox.Checked) // 自动模式关闭才出弹窗
                {
                    MessageBox.Show(ex.Message);
                }
            }
           
        }

        // http通信类，通过API获得JSON返回数据
        public class HttpUitls
        {
            public static string Get(string Url)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Proxy = null;
                request.KeepAlive = false;
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.AutomaticDecompression = DecompressionMethods.GZip;

                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                    string retString = myStreamReader.ReadToEnd();

                    myStreamReader.Close();
                    myResponseStream.Close();

                    if (response != null)
                    {
                        response.Close();
                    }
                    if (request != null)
                    {
                        request.Abort();
                    }

                    return retString;
                }
                catch (WebException)
                {
                    return "无法连接到远程服务器";
                }
            }

            public static string Post(string Url, string Data, string Referer)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.Referer = Referer;
                byte[] bytes = Encoding.UTF8.GetBytes(Data);
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = bytes.Length;
                Stream myResponseStream = request.GetRequestStream();
                myResponseStream.Write(bytes, 0, bytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
                return retString;
            }
        }

        // 自动功能：自动读csv文件，将csv数据存入visionCode。等待prtCode（打印码来自Mes2消息），有prtCode后，发送给打印机，并清除prtCode
        private void AutoSendFile(string filePathZPL, string mPrintName, string csvPath)
        {
            if (File.Exists(csvPath))
            {
                string visonCode = ReadCsvFile(csvPath);
                
                MethodInvoker mi1 = new MethodInvoker(()=> { visionCode_txtBox.Text = visonCode;  });
                BeginInvoke(mi1);

                string prtCode = prtCode_txtBox.Text;

                // 有打印码，才进行打印；打印完成后清楚prtCode
                if (!(prtCode == ""))
                {
                    CmdToTxt(filePathZPL, prtCode);

                    SendFileToPrinter(filePathZPL, mPrintName);

                    prtCode_label.Text = prtCode;
                    visionCode_label.Text = visonCode;

                    //发送完成后清空旧数据
                    {
                        prtCode_txtBox.Text = "";
                        visionCode_txtBox.Text = "";
                        scnCode_txtBox.Text = "";
                    }

                }
                else
                {
                    runStatus_lable.Text = "wait prtCode";
                }

                if (deleCsv_checkBox.Checked) { File.Delete(csvPath); }
            }
            else
            {
                runStatus_lable.Text = "file not exist";
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
        public void refreshUI(Control C,object o)
        {
            // 这里可以强转任意控件，然后改他们的方法，参数o也可以是任意复杂结构类型，只要你封装好，拆封好就OK
            ((ListBox)C).Items.Add(o.ToString());
        }
        // 在需要调用的地方写
        //this.Invoke(new RefreshUI(refreshUI), new object[] { listBox1,"string" });
        #endregion

        public void refreshPLC(short cam,short prt,short scn)
        {
            this.camValue_label.Text = Convert.ToString(cam);
            this.prtValue_label.Text = Convert.ToString(prt);
            this.scnValue_label.Text = Convert.ToString(scn);
        }

        public void refreshCSV()
        {
            readCsv_btn_Click(null,null);
        }

        public void refreshMes1(string api,string getJson,string mesID)
        {
            this.mesApi_txtBox.Text = api;
            this.JsonMsg_txtBox.Text = "Mes1:\r\n" + getJson;
            this.mesId_txtBox.Text = mesID;
        }

        public void refreshMes2(string api, string getJson,string fogID)
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

        public string GetUrl(string pluginarg="",  string par="", string mesApi = "")
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
            mesId = this.visionCode_txtBox.Text + ',' +  this.mesId_txtBox.Text;
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

       // public void 

        // 读取csv文件
        private string ReadCsvFile(string csvPath)
        {
            // csvPath = csvPath + "\\barcode.csv";

            if (!File.Exists(csvPath))
            {
                if (!this.autoRun_checkBox.Checked)
                {
                    MessageBox.Show("CSV文件未找到");
                }
                runStatus_lable.Text = "CSV文件未找到";
                return "未找到CSV";
            }
            else
            {
                DataTable myTable = new DataTable();
                myTable.Columns.Add("时间");
                myTable.Columns.Add("value0");

                string myLine;
                string[] Ary;

                StreamReader myReader = new StreamReader(csvPath, System.Text.Encoding.Default);

                while ((myLine = myReader.ReadLine()) != null)
                {
                    Ary = myLine.Split(new char[] { ',' });
                    DataRow dr = myTable.NewRow();
                    for (int i = 0; i < 2; i++)
                    {
                        String value = Ary[i];
                        dr[i] = value;
                    }
                    myTable.Rows.Add(dr);
                }
                dataGridView1.DataSource = myTable;
                string barCode = myTable.Rows[1][1].ToString();
                myReader.Close();
                runStatus_lable.Text = "CSV读取完成";
                return barCode;
            }
        }

        // 窗口生成时，需要做的事情
        private void Form1_Load(object sender, EventArgs e)
        {
            reloadPort_btn_Click(null,null);
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
                      //textBox3.Text = serialPort1.ReadExisting(); // 读所有缓存数据
                      serialRead_txtBox.Text = serialPort1.ReadTo("\r"); // 读到0x0d，也就是'\r' 回车结束
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
            string csvPath = csvPath_txtBox.Text + "\\barcode.csv";
            string filePathZPL = zplPath_txtBox.Text + "\\zpl.txt";
            string mPrintName = prtPath_txtBox.Text;

            AutoSendFile(filePathZPL, mPrintName, csvPath);
        }

        // 从ini读出数据到页面
        // static 静态类，不用创建实例，只需要通过(类名.方法名)即可进行调用
        public void LoadIniSettings(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            // string Read(string Key,string Section = null)
            string filePathZPL = myIni.Read("filePathZPL", "Form1");
            string prtName = myIni.Read("prtName", "Form1");
            string csvPath = myIni.Read("csvPath", "Form1");
            string testHttp = myIni.Read("testHttp", "Form1");

            zplPath_txtBox.Text = filePathZPL;
            prtPath_txtBox.Text = prtName;
            csvPath_txtBox.Text = csvPath;
            this.testHttpAPI = Convert.ToBoolean(testHttp);
        }

        // 将页面的数据写入ini保存
        public void SaveIniSettings(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            string filePathZPL = zplPath_txtBox.Text;
            string prtName = prtPath_txtBox.Text;
            string csvPath = csvPath_txtBox.Text;
            //string testHttp = Convert.ToString(this.testHttpAPI);
            string testHttp = "False"; // 确保每次保存ini后关闭调试模式

            myIni.Write("filePathZPL", filePathZPL, "Form1");
            myIni.Write("prtName", prtName, "Form1");
            myIni.Write("csvPath", csvPath, "Form1");
            myIni.Write("testHttp", testHttp, "Form1");
        }

        #region 后台监听文件系统变化（FileSystemWatcher）
        // 参数path是监听的文件或文件夹，filter为监听的文件类别，筛选器
        private static void FileWatcher(string path, string filter)
        {
            FileSystemWatcher fileSysWatcher = new FileSystemWatcher();
            fileSysWatcher.Path = path;
            fileSysWatcher.NotifyFilter = NotifyFilters.LastAccess
                                                                | NotifyFilters.LastWrite
                                                                | NotifyFilters.FileName
                                                                | NotifyFilters.DirectoryName;
            // 文件类型，支持通配符，"*.txt"只监视文本文件
            fileSysWatcher.Filter = filter;     // 要监控的文件格式
            fileSysWatcher.IncludeSubdirectories = false; // 监控子目录
            fileSysWatcher.Changed += new FileSystemEventHandler(OnProcess);
            fileSysWatcher.Created += new FileSystemEventHandler(OnProcess);
            fileSysWatcher.Renamed += new RenamedEventHandler(OnRenamed);
            fileSysWatcher.Deleted += new FileSystemEventHandler(OnProcess);

            //表示当前的路径正式开始被监控，一旦监控的路径出现变更，FileSystemWatcher 中的指定事件将会被触发。
            fileSysWatcher.EnableRaisingEvents = true;
        }

        private static void OnProcess(object source, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created) { OnCreated(source, e); }
            else if (e.ChangeType == WatcherChangeTypes.Changed) { OnChanged(source, e); }
            else if (e.ChangeType == WatcherChangeTypes.Deleted) { OnDeleted(source, e); }
        }

        private static void OnCreated(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File created:{0} {1} {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File changed:{0} {1} {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private static void OnDeleted(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File deleted:{0} {1} {2}", e.ChangeType, e.FullPath, e.Name);
        }

        private static void OnRenamed(object source, FileSystemEventArgs e)
        {
            Console.WriteLine("File renamed:{0} {1} {2}", e.ChangeType, e.FullPath, e.Name);
        }

        #endregion

        // 刷新端口号
        private void reloadPort_btn_Click(object sender, EventArgs e)
        {
            string[] comPort = System.IO.Ports.SerialPort.GetPortNames();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(comPort);
        }

        private void CheckScnPrtCode()
        {
            // prtCode和ScnCode都不为空时，进行一次校验
            if ((prtCode_txtBox.Text != "") & (scnCode_txtBox.Text !=""))
            {
                if (scnCode_txtBox.Text == prtCode_txtBox.Text)
                {
                    chckResult_txtBox.Text = "校验 OK：扫描码与打印码一致";
                }
                else
                {
                    chckResult_txtBox.Text = "校验 NG：扫描码与打印码不同";
                }
                // 校验完成后清空旧数据
               
                prtCode_label.Text = prtCode_txtBox.Text;
                visionCode_label.Text = visionCode_txtBox.Text;

                Task t = Task.Run(() =>
                {
                    Thread.Sleep(500);
                    MethodInvoker mi = new MethodInvoker(() => 
                    {
                        prtCode_txtBox.Text = "";
                        visionCode_txtBox.Text = "";
                        scnCode_txtBox.Text = "";
                    });
                    BeginInvoke(mi);
                });
            }
           
        }

    }
}