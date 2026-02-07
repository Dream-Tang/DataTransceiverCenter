using AxActUtlTypeLib;
using Data_Transceiver_Center.Interfaces;
using System;
using System.Threading.Tasks;

namespace DataTransceiverCenter.Plc
{
    /// <summary>
    /// 三菱PLC通信助手类
    /// 实现IPLCService2接口，基于ActUtlType控件封装三菱PLC的异步通信操作
    /// 特性：
    /// 1. 线程安全：所有PLC操作通过锁机制保证线程安全
    /// 2. 异步优先：所有核心操作提供异步实现，适配现代异步编程模型
    /// 3. 资源安全：实现Dispose模式，确保资源正确释放
    /// 4. 状态校验：操作前校验连接状态，避免无效调用
    /// </summary>
    public class MitsubishiPlcHelper : IPLCService2, IDisposable
    {
        /// <summary>
        /// 三菱PLC通信核心控件（ActUtlType ActiveX控件）
        /// 用于实现与三菱PLC的底层通信
        /// </summary>
        private AxActUtlType _axActUtlType;

        /// <summary>
        /// PLC连接状态标识
        /// 标记当前是否已与PLC建立有效连接
        /// </summary>
        private bool _isConnected;

        /// <summary>
        /// 线程安全锁对象
        /// 用于保护多线程环境下的PLC操作，避免并发冲突
        /// </summary>
        private readonly object _lockObj = new object();

        #region 接口属性实现
        /// <summary>
        /// PLC逻辑站号
        /// 对应PLC的通信站号，需与PLC侧配置一致
        /// </summary>
        public int LogicalStationNumber
        {
            get => _axActUtlType.ActLogicalStationNumber;
            set => _axActUtlType.ActLogicalStationNumber = value;
        }

        /// <summary>
        /// PLC访问密码
        /// 若PLC设置了访问密码，需在此配置
        /// </summary>
        public string Password
        {
            get => _axActUtlType.ActPassword;
            set => _axActUtlType.ActPassword = value;
        }

        /// <summary>
        /// 当前PLC连接状态
        /// 只读属性，外部可通过此属性判断连接是否有效
        /// </summary>
        public bool IsConnected => _isConnected;
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化三菱PLC助手类实例
        /// 内部创建ActUtlType控件实例并初始化句柄
        /// </summary>
        public MitsubishiPlcHelper()
        {
            // 创建ActUtlType控件实例
            _axActUtlType = new AxActUtlType();
            // 触发控件句柄初始化，确保控件可正常使用
            var handle = _axActUtlType.Handle;
        }
        #endregion

        #region 核心通信方法（接口异步实现）
        /// <summary>
        /// 异步建立与PLC的连接
        /// 执行逻辑：Open()打开通信通道 → Connect()建立连接
        /// </summary>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="Exception">连接过程中发生异常时抛出</exception>
        public async Task<int> ConnectAsync()
        {
            // 封装为异步任务，使用Task.Run避免阻塞调用线程
            return await Task.Run(() =>
            {
                // 加锁保证线程安全，避免多线程同时执行连接操作
                lock (_lockObj)
                {
                    try
                    {
                        // 第一步：打开PLC通信通道
                        int openResult = _axActUtlType.Open();
                        if (openResult != 0)
                        {
                            _isConnected = false;
                            return openResult; // 通道打开失败，返回错误码
                        }

                        // 第二步：建立与PLC的连接
                        int connectResult = _axActUtlType.Connect();
                        _isConnected = connectResult == 0; // 连接成功则标记为已连接
                        return connectResult;
                    }
                    catch (Exception ex)
                    {
                        _isConnected = false;
                        throw new Exception("PLC连接失败：" + ex.Message, ex);
                    }
                }
            });
        }

        /// <summary>
        /// 异步断开与PLC的连接
        /// 执行逻辑：Disconnect()断开连接 → Close()关闭通信通道
        /// </summary>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="Exception">断开连接过程中发生异常时抛出</exception>
        public async Task<int> DisconnectAsync()
        {
            return await Task.Run(() =>
            {
                lock (_lockObj)
                {
                    // 未连接状态下直接返回成功
                    if (!_isConnected) return 0;

                    try
                    {
                        // 第一步：断开与PLC的连接
                        int disconnectResult = _axActUtlType.Disconnect();
                        // 第二步：关闭通信通道
                        _axActUtlType.Close();
                        _isConnected = false; // 标记为未连接
                        return disconnectResult;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("PLC断开连接失败：" + ex.Message, ex);
                    }
                }
            });
        }

        /// <summary>
        /// 异步读取PLC单个寄存器（int类型）
        /// </summary>
        /// <param name="deviceName">寄存器名称（如"D100"、"M10"、"Y0"）</param>
        /// <returns>元组(错误码, 寄存器值)：错误码0表示成功，非0为三菱PLC错误码</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<(int ErrorCode, int Value)> ReadDeviceAsync(string deviceName)
        {
            return await Task.Run(() =>
            {
                // 校验连接状态，未连接则抛出异常
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法读取寄存器值
                    int errorCode = _axActUtlType.GetDevice(deviceName, out int value);
                    return (errorCode, value);
                }
            });
        }

        /// <summary>
        /// 异步读取PLC单个寄存器（short类型）
        /// </summary>
        /// <param name="deviceName">寄存器名称（如"D100"、"M10"、"Y0"）</param>
        /// <returns>元组(错误码, 寄存器值)：错误码0表示成功，非0为三菱PLC错误码</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<(int ErrorCode, short Value)> ReadDevice2Async(string deviceName)
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法读取short类型寄存器值
                    int errorCode = _axActUtlType.GetDevice2(deviceName, out short value);
                    return (errorCode, value);
                }
            });
        }

        /// <summary>
        /// 异步写入PLC单个寄存器（int类型）
        /// </summary>
        /// <param name="deviceName">寄存器名称（如"D100"、"M10"、"Y0"）</param>
        /// <param name="value">要写入的整数值</param>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<int> WriteDeviceAsync(string deviceName, int value)
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法写入int类型值
                    return _axActUtlType.SetDevice(deviceName, value);
                }
            });
        }

        /// <summary>
        /// 异步写入PLC单个寄存器（short类型）
        /// </summary>
        /// <param name="deviceName">寄存器名称（如"D100"、"M10"、"Y0"）</param>
        /// <param name="value">要写入的短整数值</param>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<int> WriteDevice2Async(string deviceName, short value)
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法写入short类型值
                    return _axActUtlType.SetDevice2(deviceName, value);
                }
            });
        }

        /// <summary>
        /// 异步读取PLC寄存器块（批量读取）
        /// </summary>
        /// <param name="startDevice">起始寄存器名称（如"D100"）</param>
        /// <param name="size">读取的寄存器数量</param>
        /// <returns>元组(错误码, 数据数组)：错误码0表示成功，数据数组为读取的int类型数据</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<(int ErrorCode, int[] Data)> ReadDeviceBlockAsync(string startDevice, int size)
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 初始化接收数据数组
                    int[] data = new int[size];
                    // 调用控件方法读取寄存器块
                    int errorCode = _axActUtlType.ReadDeviceBlock(startDevice, size, out int rawData);

                    // 读取成功时解析原始数据到数组
                    if (errorCode == 0)
                    {
                        // 将原始int转换为字节数组，便于拆分多个int值
                        var bytes = BitConverter.GetBytes(rawData);
                        for (int i = 0; i < size; i++)
                        {
                            // 按4字节为单位拆分（int占4字节）
                            if (i * 4 < bytes.Length)
                            {
                                data[i] = BitConverter.ToInt32(bytes, i * 4);
                            }
                        }
                    }

                    return (errorCode, data);
                }
            });
        }

        /// <summary>
        /// 异步写入PLC寄存器块（批量写入）
        /// </summary>
        /// <param name="startDevice">起始寄存器名称（如"D100"）</param>
        /// <param name="data">要写入的int类型数据数组</param>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="ArgumentNullException">数据数组为空时抛出</exception>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<int> WriteDeviceBlockAsync(string startDevice, int[] data)
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 校验输入参数有效性
                    if (data == null || data.Length == 0)
                        throw new ArgumentNullException(nameof(data), "写入数据不能为空");

                    // 将数据数组转换为原始字节数组
                    int rawData = 0;
                    var bytes = new byte[data.Length * 4];
                    for (int i = 0; i < data.Length; i++)
                    {
                        // 逐个int转换为字节并复制到总字节数组
                        BitConverter.GetBytes(data[i]).CopyTo(bytes, i * 4);
                    }
                    // 将字节数组转换回int（控件要求的参数格式）
                    rawData = BitConverter.ToInt32(bytes, 0);

                    // 调用控件方法写入寄存器块
                    return _axActUtlType.WriteDeviceBlock(startDevice, data.Length, ref rawData);
                }
            });
        }

        /// <summary>
        /// 异步获取PLC的CPU型号信息
        /// </summary>
        /// <returns>元组(错误码, CPU名称, CPU代码)：错误码0表示成功</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<(int ErrorCode, string CpuName, int CpuCode)> GetCpuTypeAsync()
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法获取CPU类型信息
                    int errorCode = _axActUtlType.GetCpuType(out string cpuName, out int cpuCode);
                    return (errorCode, cpuName, cpuCode);
                }
            });
        }

        /// <summary>
        /// 异步读取PLC的系统时钟
        /// </summary>
        /// <returns>元组(错误码, PLC时钟)：错误码0表示成功，时钟为DateTime类型</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<(int ErrorCode, DateTime Clock)> GetPlcClockAsync()
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法读取PLC时钟各字段
                    int errorCode = _axActUtlType.GetClockData(
                        out short year, out short month, out short day,
                        out short dayOfWeek, out short hour, out short minute, out short second);

                    // 初始化默认时间，读取成功时赋值
                    DateTime clock = default;
                    if (errorCode == 0)
                    {
                        clock = new DateTime(year, month, day, hour, minute, second);
                    }

                    return (errorCode, clock);
                }
            });
        }

        /// <summary>
        /// 异步设置PLC的系统时钟
        /// </summary>
        /// <param name="dateTime">要设置的日期时间</param>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        public async Task<int> SetPlcClockAsync(DateTime dateTime)
        {
            return await Task.Run(() =>
            {
                CheckConnectionState();
                lock (_lockObj)
                {
                    // 调用控件方法设置PLC时钟各字段
                    return _axActUtlType.SetClockData(
                        (short)dateTime.Year, (short)dateTime.Month, (short)dateTime.Day,
                        (short)dateTime.DayOfWeek, (short)dateTime.Hour, (short)dateTime.Minute, (short)dateTime.Second);
                }
            });
        }
        #endregion

        #region 同步兜底方法（私有）
        /// <summary>
        /// 同步读取PLC单个寄存器（int类型）
        /// 私有方法，仅内部/特殊场景使用（接口未暴露，优先使用异步方法）
        /// </summary>
        /// <param name="deviceName">寄存器名称</param>
        /// <param name="value">输出参数：读取到的寄存器值</param>
        /// <returns>操作结果码（0表示成功，非0为三菱PLC错误码）</returns>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        private int ReadDeviceSync(string deviceName, out int value)
        {
            CheckConnectionState();
            lock (_lockObj)
            {
                return _axActUtlType.GetDevice(deviceName, out value);
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 校验PLC连接状态
        /// 若未建立有效连接，抛出InvalidOperationException异常
        /// </summary>
        /// <exception cref="InvalidOperationException">PLC未连接时抛出</exception>
        private void CheckConnectionState()
        {
            if (!_isConnected)
                throw new InvalidOperationException("PLC未建立有效连接，请先调用ConnectAsync()方法");
        }
        #endregion

        #region 资源释放（IDisposable实现）
        /// <summary>
        /// 释放当前实例占用的所有资源
        /// 遵循IDisposable规范，确保异步资源同步释放
        /// </summary>
        public void Dispose()
        {
            // 若处于连接状态，先断开连接（异步转同步）
            if (_isConnected)
            {
                DisconnectAsync().Wait();
            }
            // 释放ActUtlType控件资源
            _axActUtlType?.Dispose();
        }
        #endregion
    }
}