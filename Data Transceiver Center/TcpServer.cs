using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Data_Transceiver_Center
{
    class TCPServer
    {
        public TCPServer()
        {
            // 构造函数，生成对象是初始化的内容
        }

        TcpListener tcpServer;  // 负责监听客户端的连接请求
        Thread listenThread;    // 负责监听的线程
        Dictionary<string, Socket> dicClientSockets = new Dictionary<string, Socket>();     // 客户端套接字集合
        Dictionary<string, Thread> dicRecvMsgThreads = new Dictionary<string, Thread>();     // 接收信息的线程集合


        // 开启服务器
        private void ServerStart(string IP,string port)
        {
            tcpServer = new TcpListener(IPAddress.Parse(IP), int.Parse(port));
            tcpServer.Start();      // 开启连接请求

            ThreadStart threadFun = new ThreadStart(ListenConnRequest);      // 创建一个监听线程执行的委托
            listenThread = new Thread(threadFun);       // 创建一个监听线程
            listenThread.IsBackground = true;       // 线程后台运行
            listenThread.Start();       // 启动线程

            Console.WriteLine("服务器开启！");

        }

        private void ServerStop()
        {
            tcpServer.Stop();       // 关闭监听
            // 断开所有客户端的连接
            //for (int i = 0; i < listb_client.Items.Count; i++)
            //{
            //    disClientSockets[listb_client.Items[i].ToString()].Close();
            //}
        }

        public void ListenConnRequest()
        {
            while (true)
            {
                try
                {
                    Socket clientSocket = tcpServer.AcceptSocket();
                    ParameterizedThreadStart ThreadFun = new ParameterizedThreadStart(RecvMsg);     // 创建一个obj传参的线程委托
                    Thread recvMsgThread = new Thread(ThreadFun);//创建一个线程
                    recvMsgThread.IsBackground = true;//设置线程为后台线程
                    recvMsgThread.Start(clientSocket);//启动线程并且传入参数clientSocket套接字
                    dicClientSockets.Add(clientSocket.RemoteEndPoint.ToString(), clientSocket);//将套接字加入集合
                    dicRecvMsgThreads.Add(clientSocket.RemoteEndPoint.ToString(), recvMsgThread);//将信息接收线程加入集合
                    //listb_client.Items.Add(clientSocket.RemoteEndPoint.ToString());
                }
                catch (Exception)
                {
                }
            }
        }

        public void RecvMsg(Object obj)
            {
                Socket clientSocket = (Socket)obj;//将传入的参数类型转换一下
                string str = clientSocket.RemoteEndPoint.ToString();
                while (true)
                {
                    try
                    {
                        //创建1M的缓存空间
                        byte[] buffer = new byte[1024 * 1024];
                        int length = clientSocket.Receive(buffer);//返回实际接收到的长度
                        string msgStr = Encoding.UTF8.GetString(buffer, 0, length);//转换成UTF-8的格式
                        if (length != 0)
                        {
                             Console.WriteLine(DateTime.Now.ToLongTimeString() + " 客户端" + str + " : " + msgStr);
                        }
                    }
                    catch (Exception)
                    {
                        //清空集合对应的套接字
                        //listb_client.Items.Remove(str);
                        dicClientSockets.Remove(str);
                        dicRecvMsgThreads.Remove(str);
                        break;
                    }
                }
            }


    }
}
