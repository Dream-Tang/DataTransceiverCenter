using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing.Common;
using System.IO;
using System.Net;
using System.Net.Sockets;


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

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(TcpServer);
            if (button1.Text=="开启TCP接收")
            {
                button1.Text = "关闭TCP接收";
            }
            else
            {
                button1.Text = "开启TCP接收";
            }
            
        }

        // 打开tcpServer服务
        private void TcpServer()
        {

            TcpListener server = null;
            try
            {
                var ip = "0.0.0.0";//服务器侦听所有地址，服务器不用设置地址，客户端设置服务器地址即可。
                string port = tcpPort_txtBox.Text;
                // Set the TcpListener on port 13000.
                Int32 localPort  = Int32.Parse(port) ;
                IPAddress localAddr = IPAddress.Parse(ip);

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, localPort);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
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
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                        Console.WriteLine("Received: {0}", data);

                        MethodInvoker mi = new MethodInvoker(()=>
                        {
                            textBox3.Text = data;
                        });
                        BeginInvoke(mi);

                        // Process the data sent by the client.
                        string sendData = data.ToUpper();

                        byte[] msg = System.Text.Encoding.ASCII.GetBytes(sendData);

                        // Send back a response.
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine("Sent: {0}", data);

                        Thread.Sleep(100);
                    }

                    Thread.Sleep(100);

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

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();

    }

        private void button2_Click(object sender, EventArgs e)
        {
            GetLocalIp();
        }

        public string GetLocalIp() 
        {
            string AddressIP = string.Empty;
            IPAddr_cobBox.Items.Clear();

            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
                if (AddressIP!="")
                {
                    IPAddr_cobBox.Items.Add(AddressIP);
                }
            }

            IPAddr_cobBox.SelectedIndex = 0;
            return AddressIP;
        }

    }
}
