using System;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace Data_Transceiver_Center
{
    public partial class Form3 : Form
    {
        public Form1 f1;    // 创建窗口1 窗口变量
        public Form2 f2;    // 创建窗口2 窗口变量

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            f1 = new Form1();   // 实例化f1
            f2 = new Form2();   // 实例化f2
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
            panel1.Controls.Clear();    // 清空原容器上的控件
            //panel1.Controls.Add(f4);    // 将窗体4加入容器panel1
            //f4.Show();      // 将窗口4进行显示
        }

        private void btnSaveIni_Click(object sender, EventArgs e)
        {
            // 选择文件夹路径
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            // 提示信息
            dialog.Description = "请选择ini保存位置";
            string iniPath = "";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                iniPath = dialog.SelectedPath+ "\\settings.ini";
            }
            try
            {
                f1.SaveIniSettings(iniPath);
                MessageBox.Show("已保存，文件位置：" + iniPath);
            }
            catch (Exception)
            {
                return ;
            }
           
        }

        private void btnLoadIni_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();   // 选择文件
            dialog.Multiselect = false; // 是否可以选择多个 文件
            dialog.Title = "请选择setting.ini文件";
            dialog.Filter = "ini文件(*.ini)|*.ini";
            string file = "";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file = dialog.FileName;
            }
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
        private async void autoRun_btn_Click(object sender, EventArgs e)
        {
            short cam, prt, scn;
            (cam, prt, scn) = f2.ReadPlc();
            MethodInvoker mi0 = new MethodInvoker(() =>
            {
                f1.refreshPLC(cam, prt, scn);
            });
            this.BeginInvoke(mi0);

            f1.refreshCSV();

            string url1 = "";
            string url2 = "";
            string url3 = "";

            string getJson1= "getJson1", getJson2= "getJson2", getJson3 = "getJson3";

            var t1 = Task.Run(() =>
            {
                MesRoot1 rt = new MesRoot1();
                MesData1 dt = new MesData1();
                rt.data = dt;

                url1 = f1.GetUrl("position", f1.GetMes1prt());

                getJson1 = Form1.HttpUitls.Get(url1);
                MethodInvoker mi1 = new MethodInvoker(() =>
                   {
                       f1.refreshMes1(getJson1);
                   });
                BeginInvoke(mi1);
            });
            await t1;
            Console.WriteLine("Json1:"+getJson1);

            var t2 = Task.Run(() => {

                url2 = f1.GetUrl("print", f1.GetMes2prt());

                getJson2 = Form1.HttpUitls.Get(url2); 
                MethodInvoker mi2 = new MethodInvoker(() =>
                {
                    f1.refreshMes2(getJson2);
                });
                BeginInvoke(mi2);
            });
            await t2;
            Console.WriteLine("Json2:"+getJson2);

            f1.makeZpl_btn_Click(null,null);
            f1.sendToPrt_btn_Click(null, null);

            var t3 = Task.Run(() => {

                url3 = f1.GetUrl("printCallBack", f1.GetMes3prt());

                getJson3 = Form1.HttpUitls.Get(url3);
                MethodInvoker mi2 = new MethodInvoker(() =>
                {
                    f1.refreshMes2(getJson3);
                });
                BeginInvoke(mi2);
            });

            Console.WriteLine("Json3:" + getJson3);

        }
    }
}