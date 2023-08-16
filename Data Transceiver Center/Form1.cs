using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
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

        private static string filePathZPL = @"F:\VS_prj\source\repos\Data Transceiver Center\Test.txt";  // 打印机配置txt文档
        private static string mPrintName = @"F:\VS_prj\source\repos\Data Transceiver Center\zt411\test1.txt";    // 打印机的网络位置
        private static string prtName = @"\XXXX\XXXX"; // 计算机名 + 打印机共享名

        private static string printApi1 = "http://192.168.10.26:7199/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=position&par=01";
        private static string printApi2 = "http://192.168.10.26:7199/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=print&par=800D2AL71YN201A17,id";
        private static string printApi3 = "http://192.168.10.26:7199/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=printCallBack&par=800D2AL71YN201A17";

        private static string apiToken = "http://192.168.10.26:7199/service/BlNC5ActionServlet?token=64FF3EE5BE7FCBDEF35F0E890A5DE47A&path=data&uid=1001A210000000000CY1&pk_corp=1001&pluginarg=";

        // 通信和流程标志位，0为初始化，1为完成，2为异常
        private int mesComunication1Flag = 0;

        private int mesComunication2Flag = 0;
        private int mesComunication3Flag = 0;
        private int sendFileToPrtFlag = 0;
        private int readCsvFlag = 0;

        // 生成ZPL文档
        private void button1_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.zplPathBox.Text + "\\zpl.txt";
            string line = prtCodeBox.Text;
            CmdToTxt(filePathZPL, line);
        }

        // 发送文件到打印机
        private void button2_Click(object sender, EventArgs e)
        {
            string filePathZPL = this.zplPathBox.Text + "\\zpl.txt";
            string prtName = this.prtPathBox.Text;
            SendFileToPrinter(filePathZPL, prtName);
        }

        // 获取Json数据
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
            //我们的接口
            string url = "http://www.kuaidi100.com/query?type=shunfeng&postid=367847964498";

            //将接口传入，这个HttpUitls的类，有兴趣可以研究下，也可以直接用就可以，不用管如何实现。
            string getJson = HttpUitls.Get(url);

            //这个需要引入Newtonsoft.Json这个DLL并using
            //传入我们的实体类还有需要解析的JSON字符串这样就OK了。然后就可以通过实体类使用数据了。
            Root rt = JsonConvert.DeserializeObject<Root>(getJson);

            this.label2.Text = rt.nu;

            //这样就可以取出json数据里面的值
            MessageBox.Show("com=" + rt.com + "\r\n" + "condition=" + rt.condition + "\r\n" + "ischeck=" + rt.ischeck + "\r\n" + "state=" + rt.state + "\r\n" + "status=" + rt.status);
            //由于这个JSON字符串的 public List<DataItem> data 是一个集合，所以我们需要遍历集合里面的所有数据
            for (int i = 0; i < rt.data.Count; i++)
            {
                this.label1.Text = rt.data[i].context;
                MessageBox.Show("Data=" + rt.data[i].context + "\r\n" + rt.data[i].location + "\r\n" + rt.data[i].time + "\r\n" + rt.data[i].ftime);
            }
        }

        // 生成Json数据
        private void button5_Click(object sender, EventArgs e)
        {
        }

        // Mes通信1
        private void mesCmd1Button_Click(object sender, EventArgs e)
        {
            MesRoot1 rt = new MesRoot1();
            MesData1 dt = new MesData1();
            rt.data = dt;

            // http 接口
            string url = mesApiBox.Text;
            string api_url = apiToken + "position&par=" + positionBox.Text;

            if (positionBox.Text == "") { MessageBox.Show("线别未设置"); return; }
            else { url = api_url; mesApiBox.Text = url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t1 = new Task(() =>
            {
                string getJson = HttpUitls.Get(url);

                // 跨线程修改UI，使用methodinvoker工具类
                MethodInvoker mi = new MethodInvoker(() =>
               {
                   if (getJson == "无法连接到远程服务器")
                   {
                       mesIdBox.Text = getJson;
                       this.mesComunication1Flag = 2;
                   }
                   else
                   {
                       // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                       //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                       try
                       {
                           rt = JsonConvert.DeserializeObject<MesRoot1>(getJson);
                           mesIdBox.Text = rt.data.id;
                           this.mesComunication1Flag = 1;
                       }
                       catch (Exception)
                       {
                           this.mesComunication1Flag = 2;
                           MessageBox.Show("JsonConver解析出错");
                           mesIdBox.Text = "##############";
                       }
                   }
               });
                this.BeginInvoke(mi);
            });
            t1.Start();
        }

        // Mes通信2
        private void mesCmd2Button_Click(object sender, EventArgs e)
        {
            MesRoot2 rt = new MesRoot2();
            MesData2 dt = new MesData2();
            rt.data = dt;

            // http 接口
            string url = mesApiBox.Text;
            string api_url = apiToken + "print&par=" + visionCodeBox.Text + "," + mesIdBox.Text;

            if (visionCodeBox.Text == "") { MessageBox.Show("视觉码未获取"); return; }
            else { url = api_url; mesApiBox.Text = url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t2 = new Task(() =>
            {
                string getJson = HttpUitls.Get(url);

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    if (getJson == "无法连接到远程服务器")
                    {
                        mesIdBox.Text = getJson;
                        this.mesComunication1Flag = 2;
                    }
                    else
                    {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                        {
                            rt = JsonConvert.DeserializeObject<MesRoot2>(getJson);
                            fogIdBox.Text = rt.data.fogId;
                            this.mesComunication2Flag = 1;
                        }
                        catch (Exception)
                        {
                            this.mesComunication2Flag = 2;
                            MessageBox.Show("JsonConver解析出错");
                            fogIdBox.Text = "##############";
                        }
                    }
                });
                this.BeginInvoke(mi);
            });
            t2.Start();
        }

        // Mes通信3
        private void mesCmd3Button_Click(object sender, EventArgs e)
        {
            MesRoot3 rt = new MesRoot3();

            // http 接口
            string url = mesApiBox.Text;
            string api_url = apiToken + "printCallBack&par=" + mesIdBox.Text;

            if (mesIdBox.Text == "") { MessageBox.Show("mesID未获取"); return; }
            else { url = api_url; mesApiBox.Text = url; }

            // 通过接口，向MES发送通信，收到的回应存入getJson
            Task t3 = new Task(() =>
            {
                string getJson = HttpUitls.Get(url);

                MethodInvoker mi = new MethodInvoker(() =>
                {
                    if (getJson == "无法连接到远程服务器")
                    {
                        mesIdBox.Text = getJson;
                        this.mesComunication3Flag = 2;
                    }
                    else
                    {
                        // 解析 MES 回应的JSON数据，解析结果存入C#本地的MesRoot类中
                        //MesRoot rt = JsonConvert.DeserializeObject<MesRoot>(getJson);
                        try
                        {
                            rt = JsonConvert.DeserializeObject<MesRoot3>(getJson);
                            printCallBackLable.Text = rt.data;
                            this.mesComunication3Flag = 1;
                        }
                        catch (Exception)
                        {
                            this.mesComunication3Flag = 2;
                            MessageBox.Show("JsonConver解析出错");
                            printCallBackLable.Text = "回调失败";
                        }
                    }
                });
                this.BeginInvoke(mi);
            });
            t3.Start();
        }

        // 读取CSV数据
        private void button7_Click(object sender, EventArgs e)
        {
            string csvPath = csvPathBox.Text + "\\barcode.csv";
            string barCode = ReadCsvFile(csvPath);
            visionCodeBox.Text = barCode;

            if (checkBox2.Checked)
            {
                File.Delete(csvPath);
            }
        }

        // 打开串口
        private void Button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32(comboBox2.Text);
                serialPort1.Open();
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

        // 关闭串口
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                serialPort1.Close();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        // 后台监控csv文档选项，多线程
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked)
            {
                MessageBox.Show("循环检测csv文件");
                timer1.Enabled = true;
                timer1.Start();
            }
            else
            {
                timer1.Enabled = false;
            }
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
                label11.Text = "zpl文件生产成功";
                //MessageBox.Show("zpl文件生产成功，文件位置："+ filePathZPL);
            }
            catch (Exception)
            {
                MessageBox.Show("zpl文件生成失败\r\n" + filePathZPL);
            }
        }

        // 给打印机发送文件
        private void SendFileToPrinter(string filePathZPL, string mPrintName)
        {
            try
            {
                // 将ZPL指令发送到打印机，filePathZPL为ZPL指令文件路径，mPrintName为打印机路径，例如：mPrintName = @"\\192.168.0.132\zt411"
                File.Copy(filePathZPL, mPrintName, true);
                this.sendFileToPrtFlag = 1;
                this.mesComunication1Flag = 0;
                this.mesComunication2Flag = 0;
                this.mesComunication3Flag = 0;
                label11.Text = "发送成功!";
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("无权限，或目标位置已存在同名只读文件\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("路径长度为零，或包含无效字符\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("指定的路径和/或文件名超过了系统定义的最大长度\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("指定的文件或目标无效\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("未找到要发送的文件\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
            }
            catch (IOException)
            {
                MessageBox.Show("发生了I/O错误，目标也应是文件\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("文件格式或目标格式无效\r\n" + filePathZPL + "\r\n" + mPrintName);
                this.sendFileToPrtFlag = 2;
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
                    MessageBox.Show("无法连接到远程服务器");
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

        // 自动功能：自动读csv文件，将csv数据存入visionCode。等待prtCode，有prtCode后，发送给打印机，并清除prtCode
        private void AutoSendFile(string filePathZPL, string mPrintName, string csvPath)
        {
            if (File.Exists(csvPath))
            {
                string visonCode = ReadCsvFile(csvPath);
                visionCodeBox.Text = visonCode;

                string prtCode = prtCodeBox.Text;

                // 有打印码，才进行打印；打印完成后清楚prtCode
                if (!(prtCode == ""))
                {
                    CmdToTxt(filePathZPL, prtCode);

                    SendFileToPrinter(filePathZPL, mPrintName);

                    prtCodelabel.Text = prtCode;
                    visionCodelabel.Text = visonCode;

                    //if ((this.mesComunication1Flag ==1)&(this.mesComunication2Flag==1))
                    {
                        prtCodeBox.Text = "";//发送完成后清空
                        visionCodeBox.Text = "";
                        this.mesComunication1Flag = 0;
                        this.mesComunication2Flag = 0;
                    }
                }
                else
                {
                    label11.Text = "wait prtCode";
                }

                if (checkBox2.Checked) { File.Delete(csvPath); }
            }
            else
            {
                label11.Text = "file not exist";
            }
        }

        // 读取csv文件
        private string ReadCsvFile(string csvPath)
        {
            // csvPath = csvPath + "\\barcode.csv";

            if (!File.Exists(csvPath))
            {
                MessageBox.Show("CSV文件未找到");
                return "未找到CSV";
            }
            else
            {
                DataTable myTable = new DataTable();
                myTable.Columns.Add("时间");
                myTable.Columns.Add("value0");

                string myLine;
                string[] Ary;

                StreamReader myReader = new StreamReader(csvPath);

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
                return barCode;
            }
        }

        // 窗口生成时，需要做的事情
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] baudRate = { "9600", "115200" };
            string[] comPort = System.IO.Ports.SerialPort.GetPortNames();
            comboBox2.Items.AddRange(baudRate);
            comboBox1.Items.AddRange(comPort);
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
                      textBox3.Text = serialPort1.ReadTo("\r"); // 读到0x0d，也就是'\r' 回车结束
                      // 扫码枪通过串口发送过来的
                      //textBox4.Text = System.Text.Encoding.ASCII.GetString(ToBytesFromHexString(textBox3.Text));

                      //MessageBox.Show("收到条码："+ textBox3.Text);
                      if (textBox3.Text == visionCodeBox.Text)
                      {
                          textBox4.Text = "校验OK：扫描码与打印码一致";
                      }
                      else
                      {
                          textBox4.Text = "校验NG：扫描码与打印码不同";
                      }
                  }
                  )
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            string csvPath = csvPathBox.Text + "\\barcode.csv";
            string filePathZPL = zplPathBox.Text + "\\zpl.txt";
            string mPrintName = prtPathBox.Text;

            AutoSendFile(filePathZPL, mPrintName, csvPath);
        }


        public void ReflashSettingByIni(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            // string Read(string Key,string Section = null)
            string filePathZPL = myIni.Read("filePathZPL","Form1");
            string prtName = myIni.Read("prtName", "Form1");
            string csvPath = myIni.Read("csvPath", "Form1");

            zplPathBox.Text = filePathZPL;
            prtPathBox.Text = prtName;
            csvPathBox.Text = csvPath;
        }

        public void SaveSettingsToIni(string iniFile)
        {
            var myIni = new IniFile(iniFile);

            string filePathZPL = zplPathBox.Text;
            string prtName = prtPathBox.Text;
            string csvPath = csvPathBox.Text;

            myIni.Write("filePathZPL", filePathZPL,"Form1");
            myIni.Write("prtName", prtName, "Form1");
            myIni.Write("csvPath", csvPath, "Form1");
        }

    }
}