﻿using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Data_Transceiver_Center
{
    public partial class Form3 : Form
    {
        public Form1 f1;    // 创建窗口1 窗口变量
        public Form2 f2;    // 创建窗口2 窗口变量
        public Form4 f4;    // 创建窗口2 窗口变量

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            f1 = new Form1();   // 实例化f1
            f2 = new Form2();   // 实例化f2
            f4 = new Form4();   // 实例化f2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            f1.TopLevel = false;
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f1);    // 将窗体1加入容器panel1
            f1.Show();      // 将窗口1进行显示
        }

        private void button2_Click(object sender, EventArgs e)
        {
            f2.TopLevel = false;
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f2);    // 将窗体2加入容器panel1
            f2.Show();      // 将窗口2进行显示
        }

        private void button3_Click(object sender, EventArgs e)
        {
            f4.TopLevel = false;
            panel1.Controls.Clear();    // 清空原容器上的控件
            panel1.Controls.Add(f4);    // 将窗体4加入容器panel1
            f4.Show();      // 将窗口4进行显示
        }

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
            }
            catch (Exception)
            {
                return;
            }
        }

        // 自动模式：
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
            short cam, prt, scn;
            // 读PLC
            (cam, prt, scn) = f2.ReadPlc();
            MethodInvoker mi0 = new MethodInvoker(() =>
            {
                f1.refreshPLC(cam, prt, scn);
            });
            this.BeginInvoke(mi0);

            // 读CSV
            f1.refreshCSV();

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
                    getJson1 = Form1.HttpUitls.Get(url1);
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
                    url1 = f1.GetUrl("position", f1.GetMes1prt());
                    getJson1 = Form1.HttpUitls.Get(url1);
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
            Console.WriteLine("Json1:" + getJson1);

            // MES2
            var t2 = Task.Run(() =>
            {
                if (f1.testHttpAPI)
                {
                    Random rnd = new Random();
                    string postid = Convert.ToString(rnd.Next(999999)) + Convert.ToString(rnd.Next(999999));
                    //我们的接口
                    url2 = "http://www.kuaidi100.com/query?type=shunfeng&postid=" + postid;
                    getJson2 = Form1.HttpUitls.Get(url2);
                    try
                    {
                        testApiRoot rt = JsonConvert.DeserializeObject<testApiRoot>(getJson2);
                        fogID = rt.nu;
                    }
                    catch (Exception)
                    {
                        fogID = "解析出错"; 
                    }
                    
                }
                else
                {
                    url2 = f1.GetUrl("print", f1.GetMes2prt());
                    getJson2 = Form1.HttpUitls.Get(url2);
                    try
                    {
                        MesRoot2 msrt2 = JsonConvert.DeserializeObject<MesRoot2>(getJson2);
                        fogID = msrt2.data.fogId;
                    }
                    catch (Exception)
                    {
                        fogID = "解析出错";
                    }
                }
                MethodInvoker mi2 = new MethodInvoker(() =>
                {
                    f1.refreshMes2(url2, getJson2, fogID);
                });
                BeginInvoke(mi2);
            });
            await t2;
            Console.WriteLine("Json2:" + getJson2);

            // 打印
            f1.makeZpl_btn_Click(null, null);
            f1.sendToPrt_btn_Click(null, null);
            prt = CommunicationProtocol.prtComplete;

            // 校验 并与PLC通信
            var t4 = Task.Run(() =>
            {
                string result = f1.GetCheckResult();
                if (result == "OK")
                {
                    scn = CommunicationProtocol.checkOK;
                }
                if (result == "NG")
                {
                    scn = CommunicationProtocol.checkNG;
                }
                f2.WritePlc(cam, prt, scn);
                this.BeginInvoke(mi0);
            });

            // MES3
            var t3 = Task.Run(() =>
            {

                url3 = f1.GetUrl("printCallBack", f1.GetMes3prt());

                getJson3 = Form1.HttpUitls.Get(url3);

                MethodInvoker mi2 = new MethodInvoker(() =>
                {
                    f1.refreshMes3(url3, getJson3);
                });
                BeginInvoke(mi2);
            });

            Console.WriteLine("Json3:" + getJson3);

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

        ///  CSV监控选框
        private void fileWatcher_chkbox_CheckedChanged(object sender, EventArgs e)
        {
            string fileWatchPath= f1.GetCsvPath();
            if (fileWatchPath == "")
            {
                string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
                fileWatchPath = appPath.Substring(0,appPath.IndexOf("DataTransceiverCenter")+21);
                Console.WriteLine(fileWatchPath);
            }
            fileSystemWatcher1.Path = fileWatchPath;

            if (fileWatcher_chkbox.Checked == true)
            {
                fileSystemWatcher1.EnableRaisingEvents = true;
                Console.WriteLine("AutoMode is running");
            }
            else
            {
                fileSystemWatcher1.EnableRaisingEvents = false;
                Console.WriteLine("AutoMode is closing");
            }
        }
    }
}