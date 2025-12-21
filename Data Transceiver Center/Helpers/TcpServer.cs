using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Data_Transceiver_Center
{
    class TCPServer
    {
        // 事件：当收到数据时触发
        public event Action<string, string> DataReceived;  // 客户端接收
        public event Action<string> ClientConnected;       // 客户端连接
        public event Action<string> ClientDisconnected;    // 客户端断开

        private TcpListener _tcpServer;
        private Thread _listenThread;
        private bool _isRunning;
        private readonly Dictionary<string, Socket> _clientSockets = new Dictionary<string, Socket>();
        private readonly Dictionary<string, Thread> _recvThreads = new Dictionary<string, Thread>();

        public bool IsRunning => _isRunning;

        public void Start(string ip, int port)
        {
            if (_isRunning) return;

            try
            {
                _tcpServer = new TcpListener(IPAddress.Parse(ip), port);
                _tcpServer.Start();
                _isRunning = true;

                _listenThread = new Thread(ListenForConnections);
                _listenThread.IsBackground = true;
                _listenThread.Start();

                Console.WriteLine($"TCP服务器已启动，监听 {ip}:{port}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"启动服务器启动失败: {ex.Message}");
                throw;
            }
        }

        public void Stop()
        {
            if (!_isRunning) return;

            _isRunning = false;
            _tcpServer?.Stop();

            // 关闭所有客户端连接
            foreach (var socket in _clientSockets.Values)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch { }
            }

            _clientSockets.Clear();

            // 等待线程结束
            if (_listenThread != null && _listenThread.IsAlive)
            {
                _listenThread.Join(1000);
            }

            Console.WriteLine("TCP服务器已停止");
        }

        private void ListenForConnections()
        {
            while (_isRunning)
            {
                try
                {
                    var clientSocket = _tcpServer.AcceptSocket();
                    var clientInfo = clientSocket.RemoteEndPoint.ToString();

                    // 触发客户端连接事件
                    ClientConnected?.Invoke(clientInfo);

                    // 创建新线程处理客户端通信
                    var recvThread = new Thread(ReceiveMessages);
                    recvThread.IsBackground = true;
                    recvThread.Start(clientSocket);

                    lock (_clientSockets)
                    {
                        _clientSockets[clientInfo] = clientSocket;
                        _recvThreads[clientInfo] = recvThread;
                    }
                }
                catch (Exception ex)
                {
                    if (_isRunning)
                        Console.WriteLine($"接受连接出错: {ex.Message}");
                }
            }
        }

        private void ReceiveMessages(object obj)
        {
            var clientSocket = obj as Socket;
            if (clientSocket == null) return;

            var clientInfo = clientSocket.RemoteEndPoint.ToString(); // 客户端IP+端口（唯一标识）
            byte[] buffer = new byte[1024 * 1024]; // 1MB接收缓冲区，可根据你的数据大小调整

            try
            {
                // 持续接收客户端数据，直到连接断开/服务器停止
                while (_isRunning && clientSocket.Connected)
                {
                    int receiveLength = clientSocket.Receive(buffer); // 阻塞接收数据
                    if (receiveLength <= 0) break; // 接收长度为0表示客户端断开

                    // 将字节数组转为字符串（编码需与客户端一致，通常UTF8/GBK）
                    string receivedData = Encoding.UTF8.GetString(buffer, 0, receiveLength);
                    Console.WriteLine($"{DateTime.Now:HH:mm:ss} 收到[{clientInfo}]数据: {receivedData}");

                    // 触发数据接收事件，把数据传递给主窗体处理
                    DataReceived?.Invoke(clientInfo, receivedData);
                }
            }
            catch (Exception ex)
            {
                // 捕获通信异常（如客户端强制断开）
                Console.WriteLine($"与[{clientInfo}]通信出错: {ex.Message}");
            }
            finally
            {
                // 无论正常断开还是异常，都清理客户端资源
                clientSocket.Close();

                lock (_clientSockets) // 加锁保证线程安全
                {
                    _clientSockets.Remove(clientInfo);
                    _recvThreads.Remove(clientInfo);
                }

                // 触发客户端断开事件
                ClientDisconnected?.Invoke(clientInfo);
                Console.WriteLine($"客户端[{clientInfo}]已断开");
            }
        }

        // 向指定客户端发送数据
        public bool SendToClient(string clientInfo, string data)
        {
            if (!_isRunning || string.IsNullOrEmpty(clientInfo) || string.IsNullOrEmpty(data))
                return false;

            try
            {
                lock (_clientSockets)
                {
                    if (_clientSockets.TryGetValue(clientInfo, out var socket) && socket.Connected)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes(data);
                        socket.Send(bytes);
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"向客户端 {clientInfo} 发送数据失败: {ex.Message}");
            }

            return false;
        }

        // 向所有客户端广播数据
        public void Broadcast(string data)
        {
            if (!_isRunning || string.IsNullOrEmpty(data)) return;

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            List<string> disconnectedClients = new List<string>();

            lock (_clientSockets)
            {
                foreach (var kvp in _clientSockets)
                {
                    try
                    {
                        if (kvp.Value.Connected)
                            kvp.Value.Send(bytes);
                        else
                            disconnectedClients.Add(kvp.Key);
                    }
                    catch
                    {
                        disconnectedClients.Add(kvp.Key);
                    }
                }

                // 清理已断开的客户端
                foreach (var client in disconnectedClients)
                {
                    _clientSockets.Remove(client);
                    _recvThreads.Remove(client);
                    ClientDisconnected?.Invoke(client);
                }
            }
        }
    }
}