using System;
using System.Windows.Forms;

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
            
            //f4 = new Form4();
            //f1.TopLevel = false;
            //f2.TopLevel = false;
            //f4.TopLevel = false;
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
    }
}