using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;
using System.IO;


namespace Data_Transceiver_Center
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 将生成的条码设置到pictureBox
        /// </summary>
        /// <param name="barCodeStr">条形码对应的字符串</param>

        public static Image SetBarCode128(string barCodeStr, int height=40)
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
                MessageBox.Show("barCode转换出错，存在无法转换的字符");
                Console.WriteLine("barCode转换出错，存在无法转换的字符");
                return null;
            }
        }

        private void prtCode_txtBox_TextChanged(object sender, EventArgs e)
        {
            string barCodeStr = prtCode_txtBox.Text;
            pictureBox2.Image = SetBarCode128(barCodeStr);
        }

        private void fileWatcher_chkbox_CheckedChanged(object sender, EventArgs e)
        {
            if (fileWatcher_chkbox.Checked)
            {
                string appPath = System.AppDomain.CurrentDomain.BaseDirectory;
                string fileWatchPath = appPath.Substring(0, appPath.IndexOf("DataTransceiverCenter") + 21);
                label4_2.Text = "监控：" + fileWatchPath;
                Console.WriteLine(fileWatchPath);
                this.fileSystemWatcher1.Path = fileWatchPath;
                fileSystemWatcher1.EnableRaisingEvents = true;
                MessageBox.Show("文件夹监控已打开，文件夹内有文件修改，将触发事件");
            }
            else
            {
                label4_2.Text = "fileSystemWatcher 关闭";
                fileSystemWatcher1.EnableRaisingEvents = false;
                MessageBox.Show("文件夹监控已关闭");
            }
            
        }

        DateTime lastRead = DateTime.MinValue;

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            DateTime lastWriteTime = File.GetLastWriteTime(e.FullPath);
            if (lastWriteTime!=lastRead)
            {
                Console.WriteLine("changend");
                label4_3.Text = lastWriteTime + ": file changed";
                lastRead = lastWriteTime;
            }
            else
            {
                Console.WriteLine(lastRead);
            }
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Lines.Length > 0)
            {
                textBox2.Text = textBox1.Lines[textBox1.Lines.Length-1];
            }
            else
            {
                textBox2.Text = null;
            }
        }
    }
}
