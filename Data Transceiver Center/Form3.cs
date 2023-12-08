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
    public partial class Form3 : Form
    {
        public Form1 f1;    // 创建窗口1 窗口变量
        public Form2 f2;    // 创建窗口2 窗口变量
        public Form4 f4;    // 创建窗口2 窗口变量

        IPAddress localAddr = IPAddress.Parse("0.0.0.0"); // 本地所有网络地址
        Int32 localPortNum = Int32.Parse("8080");  // TCP服务器的端口

        TcpListener server = null;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Text = Application.ProductName + "  V" + Application.ProductVersion;
            f1 = new Form1();   // 实例化f1
            f2 = new Form2();   // 实例化f2
            f4 = new Form4();   // 实例化f2
            this.tcpServer_checkBox.Checked = true;
            this.trigger1_checkBox.Checked = true;
            this.needCheck_checkBox.Checked = true;

        }

        // 打开页面1
        private void btn_form1Open(object sender, EventArgs e)
        {
            f1.TopLevel = false;
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f1);    // 将窗体1加入容器panel1
            f1.Show();      // 将窗口1进行显示
            btn_RetryRead.Visible = true;
            btn_RetryChk.Visible = true;
        }

        // 打开页面2
        private void btn_form2Open(object sender, EventArgs e)
        {
            f2.TopLevel = false;
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f2);    // 将窗体2加入容器panel1
            f2.Show();      // 将窗口2进行显示
            btn_RetryRead.Visible = false;
            btn_RetryChk.Visible = false;
        }

        // 打开页面3
        private void btn_form3Open(object sender, EventArgs e)
        {
            f4.TopLevel = false;
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f4);    // 将窗体4加入容器panel1
            //f4.Show();      // 将窗口4进行显示
        }

        // 保存配置
        private void btnSaveIni_Click(object sender, EventArgs e)
        {
            // 选择文件夹路径
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            // 提示信息
            dialog.Description = "请选择ini保存位置";
            string iniPath = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                iniPath = dialog.SelectedPath + "\\settings.ini";
            }
            try
            {
                f1.SaveIniSettings(iniPath);
                MessageBox.Show("已保存，文件位置：" + iniPath);
            }
            catch (Exception)
            {
                return;
            }

        }

        // 加载配置
        private void btnLoadIni_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();   // 选择文件
            dialog.Multiselect = false; // 是否可以选择多个 文件
            dialog.Title = "请选择setting.ini文件";
            dialog.Filter = "ini文件(*.ini)|*.ini";
            string file = "";
            try
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    file = dialog.FileName;
                }
            }
            catch (Exception)
            { return; }

            try
            {
                f1.LoadIniSettings(file);
                connectPlc_checkBox.Checked = true;
            }
            catch (Exception)
            {
                return;
            }
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
        // 流程：读CSV->mes1->mes2->  生成打印指令->发送打印->mes3->串口(Event)
        // task0：读PLC
        // task1：（启动条件）读csv=》mes1=》mes2
        // async task2：（启动条件）makeZPL=》发送ZPL=》await mes3
        // task3：refalsh()=>methedInvoke();
        // task4：获取CheckResult，读取判定结果，写PLC
        private async void AutoRunMode()
        {
            short cam=-1, prt=-1, scn=-1;
            Tuple<short,short,short> plcRegValue;

            MethodInvoker mi0 = new MethodInvoker(() =>
            {
                f1.refreshPLC(cam, prt, scn);
            });
            // 读PLC
            if (ignorePlc_checkBox.Checked)
            {
                //(cam, prt, scn) = (-1, -1, -1);   // .net4.8特性
                cam = -1;
                prt = -1;
                scn = -1;

                this.BeginInvoke(mi0);
            }
            else
            {
                //(cam, prt, scn) = f2.ReadPlc();   // .net4.8特性
                plcRegValue = f2.ReadPlc();

                cam = plcRegValue.Item1;
                prt = plcRegValue.Item2;
                scn = plcRegValue.Item3;

                this.BeginInvoke(mi0);

                // PLC未连接，读取数值为-1，直接返回
                if (cam == -1 & prt == -1 & scn == -1)
                {
                    return;
                }
            }

            string url1 = "";
            string url2 = "";
            string url3 = "";

            string getJson1 = "getJson1", getJson2 = "getJson2", getJson3 = "getJson3";

            string mesID = "";
            string fogID = "";

            // MES1 
            var t1 = Task.Run(() =>
            {
                if (f1.testHttpAPI)
                {
                    Random rnd = new Random();
                    string postid = Convert.ToString(rnd.Next(999999)) + Convert.ToString(rnd.Next(999999));
                    //随机数生成快递单号，用来查询数据，测试Json
                    url1 = "http://www.kuaidi100.com/query?type=shunfeng&postid=" + postid;
                    getJson1 = HttpUitls.Get(url1);
                    try
                    {
                        testApiRoot rt = JsonConvert.DeserializeObject<testApiRoot>(getJson1);
                        mesID = rt.nu;
                    }
                    catch (Exception)
                    {
                        mesID = "解析出错";
                    }
                }
                else
                {
                    url1 = f1.GetUrl(f1.GetMesAddr(), "position", f1.GetMes1prt());
                    getJson1 = HttpUitls.Get(url1);
                    try
                    {
                        MesRoot1 msrt1 = JsonConvert.DeserializeObject<MesRoot1>(getJson1);
                        mesID = msrt1.data.id;
                    }
                    catch (Exception)
                    {
                        mesID = "解析出错";
                    }
                }
                MethodInvoker mi1 = new MethodInvoker(() =>
                {
                    f1.refreshMes1(url1, getJson1, mesID);
                });
                BeginInvoke(mi1);
            });
            await t1;
            Console.WriteLine("task t1：Json1:" + getJson1);
            //cam = CommunicationProtocol.camOK;

            // MES2
            var t2 = Task.Run(() =>
            {
                if (f1.testHttpAPI)
                {
                    Random rnd = new Random();
                    string postid = Convert.ToString(rnd.Next(999999)) + Convert.ToString(rnd.Next(999999));
                    //我们的接口
                    url2 = "http://www.kuaidi100.com/query?type=shunfeng&postid=" + postid;
                    getJson2 = HttpUitls.Get(url2);
                    try
                    {
                        testApiRoot rt = JsonConvert.DeserializeObject<testApiRoot>(getJson2);
                        fogID = rt.nu;

                        Action action = () => { f1.SetLbReadCode(CommunicationProtocol.readCodeOK); };
                        Invoke(action);
                    }
                    catch (Exception)
                    {
                        fogID = "解析出错";

                        Action action = () => { f1.SetLbReadCode(CommunicationProtocol.readCodeNG); };
                        Invoke(action);
                    }

                }
                else
                {
                    url2 = f1.GetUrl(f1.GetMesAddr(), "print", f1.GetMes2prt());
                    getJson2 = HttpUitls.Get(url2);
                    try
                    {
                        MesRoot2 msrt2 = JsonConvert.DeserializeObject<MesRoot2>(getJson2);
                        fogID = msrt2.data.fogId;
                        // 发送camOK信号
                        if (!ignorePlc_checkBox.Checked)
                        {
                            // 发送camNG信号
                            plcRegValue = f2.ReadPlc();
                            cam = CommunicationProtocol.camOK;
                            prt = plcRegValue.Item2;
                            scn = plcRegValue.Item3;
                            f2.WritePlc(cam, prt, scn);
                        }
                        Action action = () => { f1.SetLbReadCode(CommunicationProtocol.readCodeOK); };
                        Invoke(action);
                    }
                    catch (Exception)
                    {
                        fogID = "解析出错";
                        if (!ignorePlc_checkBox.Checked)
                        {
                            // 发送camNG信号
                            plcRegValue = f2.ReadPlc();
                            cam = CommunicationProtocol.camNG;
                            prt = plcRegValue.Item2;
                            scn = plcRegValue.Item3;
                            f2.WritePlc(cam, prt, scn);
                        }
                        Action action = () => { f1.SetLbReadCode(CommunicationProtocol.readCodeNG);};
                        Invoke(action);
                    }
                }
                MethodInvoker mi2 = new MethodInvoker(() =>
                {
                    f1.refreshMes2(url2, getJson2, fogID);
                });
                BeginInvoke(mi2);
            });
            await t2;
            Console.WriteLine("task t2：Json2:" + getJson2);

            // 打印（需要等待prt信号为ready）
            if (fogID != "解析出错")
            {
                var t3 = Task.Run(() =>
                {
                    if (!ignorePlc_checkBox.Checked)
                    {
                        prt = f2.ReadPlc().Item2;
                        while (!(prt == CommunicationProtocol.prtReady))
                        {
                            plcRegValue = f2.ReadPlc();

                            cam = plcRegValue.Item1;
                            prt = plcRegValue.Item2;
                            scn = plcRegValue.Item3;

                            this.BeginInvoke(mi0);
                            Thread.Sleep(500);
                        }
                        prt = CommunicationProtocol.prtComplete;
                        f2.WritePlc(cam, prt, scn);
                        this.BeginInvoke(mi0);
                        Console.WriteLine("task t3：已收到prtReady信号");
                    }
                }); 
                await t3;

                f1.makeZpl_btn_Click(null, null);
                f1.sendToPrt_btn_Click(null, null);

                Console.WriteLine("task t3:发送打印机");
                Action action = () => { f1.SetLbPrtCode(CommunicationProtocol.prtCodeOK); };
                Invoke(action);
            }
            else
            {
                Console.WriteLine("task t3：FOG ID解析出错，跳过打印");
                Action action = () => { f1.SetLbPrtCode(CommunicationProtocol.prtCodeNG); };
                Invoke(action);
            }

            // MES3
            var t4 = Task.Run(() =>
            {

                url3 = f1.GetUrl(f1.GetMesAddr(), "printCallBack", f1.GetMes3prt());

                getJson3 = HttpUitls.Get(url3);

                MethodInvoker mi2 = new MethodInvoker(() =>
                {
                    f1.refreshMes3(url3, getJson3);
                });
                BeginInvoke(mi2);
            });
            Console.WriteLine("task t4：Json3:" + getJson3);

        }

        /// <summary>
        /// 文件监控，当有文件改变，则触发事件。filechanged会被多次触发，使用lastRead和lastWrite的时间对比，来避免重复触发
        /// </summary>
        /// 
        DateTime lastRead = DateTime.Now;
        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime != lastRead)
            {
                Console.WriteLine("changend");
                lastRead = lastWriteTime;
                AutoRunMode();
            }
            else
            {
                Console.WriteLine(lastRead);
            }
        }

        private void trigger1_CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;

            // 二维码触发输入
            var t1 = Task.Run(() =>
            {
                while (trigger1_checkBox.Checked)
                {
                    // F1 触发后，将trigSingner置为working状态
                    if (f1.trigSigner == Form1.STATUS_WORKING)
                    {
                        //未禁用PLC，则执行，禁用则跳过
                        if (!ignorePlc_checkBox.Checked)
                        {
                            // 触发放行
                            plcRegValue = f2.ReadPlc();
                            cam = CommunicationProtocol.camReset;
                            prt = plcRegValue.Item2;
                            scn = plcRegValue.Item3;
                            f2.WritePlc(cam, prt, scn);
                        }

                        Console.WriteLine("trigger：自动运行已触发");
                        Action action = () =>
                        {
                            AutoRunMode();
                        };
                        f1.trigSigner = Form1.STATUS_WAIT;
                        //f1.veriCodeCount = f1.veriCodeCount + 1;

                        Invoke(action);
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void t5CheckTask(object sender, EventArgs e)
        {
            // 校验 并与PLC通信
            var t5 = Task.Run(() =>
            {
                while (needCheck_checkBox.Checked)
                {
                    if (f1.seriStatus == Form1.STATUS_READY)
                    {
                        Action action = () =>
                        {
                            t5CheckTask();
                        };
                        f1.seriStatus = Form1.STATUS_WAIT;
                        Invoke(action);
                    }
                    Thread.Sleep(500);
                }
            });
        }

        // 校验任务t5
        private void t5CheckTask()
        {
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;

            string checkResult = f1.GetCheckResult();

            if (checkResult == "OK")
            {
                scn = CommunicationProtocol.checkOK;
                Console.WriteLine("task t5：校验结果OK");
                f1.seriStatus = Form1.STATUS_WAIT;
                Action action = () => { f1.SetLbChkCode(CommunicationProtocol.chkCodeOK); };
                Invoke(action);
            }
            if (checkResult == "NG")
            {
                scn = CommunicationProtocol.checkNG;
                Console.WriteLine("task t5：校验结果NG");
                f1.seriStatus = Form1.STATUS_WAIT;
                Action action = () => { f1.SetLbChkCode(CommunicationProtocol.chkCodeNG); };
                Invoke(action);
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
                plcRegValue = f2.ReadPlc();
                cam = plcRegValue.Item1;
                prt = plcRegValue.Item2;
                f2.WritePlc(cam, prt, scn);
                MethodInvoker mi0 = new MethodInvoker(() =>
                {
                    f1.refreshPLC(cam, prt, scn);
                });
                this.BeginInvoke(mi0);
                Console.WriteLine("task t5：已发送校验结果给PLC");
            }
        }


        private void tcpServer_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (tcpServer_checkBox.Checked)
            {
                server = new TcpListener(localAddr, localPortNum);
                server.Start();
                Task.Run(TcpServer);
            }
            else
            {
                server.Stop();
            }
        }

        private void TcpServer()
        {
            try
            {
                //server = new TcpListener(localAddr, localPortNum);
                //server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                while (true)
                {
                    Console.WriteLine("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while (tcpServer_checkBox.Checked && (i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        if (tcpServer_checkBox.Checked)
                        {
                            // Translate data bytes to a ASCII string.
                            data = System.Text.Encoding.Default.GetString(bytes, 0, i);
                            Console.WriteLine("Received: {0}", data);

                            MethodInvoker mi = new MethodInvoker(() =>
                            {
                                f1.txtBox_veriCodeHistory.AppendText(data + "\r\n");
                            });
                            BeginInvoke(mi);

                            // Process the data sent by the client.
                            //string sendData = data.ToUpper();

                            byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine("Sent: {0}", data);
                        }
                        else
                        {
                            stream.Close();
                            client.Close();
                            server.Stop();
                        }
                        Thread.Sleep(100);
                    }

                    Thread.Sleep(100);

                    client.GetStream().Close();
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }

        }

        private void connectPlc_checkBox_CheckStateChanged(object sender, EventArgs e)
        {
            if (connectPlc_checkBox.Checked)
            {
                f2.btn_Open_Click(null,null);
                if (f2.GetReturnCode()== "0x00000000 [HEX]")
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
                f2.btn_Close_Click(null,null);
                lable_PlcConnectStatus.Text = "PLC 已断开";
                lable_PlcConnectStatus.ForeColor = System.Drawing.Color.White;
                lable_PlcConnectStatus.BackColor = System.Drawing.Color.Black;
            }
        }

        public void btn_Retry_fog_Click(object sender, EventArgs e)
        {
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;

            if (!ignorePlc_checkBox.Checked)
            {
                // 发送camNG信号
                plcRegValue = f2.ReadPlc();
                cam = CommunicationProtocol.camRetry;
                prt = plcRegValue.Item2;
                scn = plcRegValue.Item3;
                f2.WritePlc(cam, prt, scn);
            }
            f1.SetLbReadCode("手动读码");
        }

        public void btn_RetryChk_Click(object sender, EventArgs e)
        {
            short cam = -1, prt = -1, scn = -1;
            Tuple<short, short, short> plcRegValue;

            if (!ignorePlc_checkBox.Checked)
            {
                // 发送camNG信号
                plcRegValue = f2.ReadPlc();
                cam = plcRegValue.Item1;
                prt = plcRegValue.Item2;
                scn = CommunicationProtocol.checkLose;
                f2.WritePlc(cam, prt, scn);
            }
            f1.ClearSerial();
            f1.SetLbReadCode("手动验码");
            f1.seriStatus = Form1.STATUS_READY;
        }
    }
}