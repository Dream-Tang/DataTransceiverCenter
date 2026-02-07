using System;
using System.Threading.Tasks;


namespace Data_Transceiver_Center.Interfaces
{
    /// <summary>
    /// PLC操作抽象接口，定义CheckHelper需要的PLC功能
    /// </summary>
    public interface IPLCService
    {
        // 线程安全地写入PLC扫描器相关寄存器
        void SafeWritePlc(short? cam = null, short? prt = null, short? scn = null);
        // 检查是否需要跨线程调用（用于线程安全操作）
        bool InvokeRequired { get; }
        // 跨线程调用委托（用于线程安全操作）
        object Invoke(Delegate method);
    }

    /// <summary>
    /// 三菱PLC通信服务接口（与MitsubishiPlcHelper完全适配）
    /// </summary>
    public interface IPLCService2 : IDisposable
    {
        #region PLC连接参数
        /// <summary>
        /// PLC逻辑站号
        /// </summary>
        int LogicalStationNumber { get; set; }

        /// <summary>
        /// PLC密码
        /// </summary>
        string Password { get; set; }

        /// <summary>
        /// 是否已连接PLC
        /// </summary>
        bool IsConnected { get; }
        #endregion

        #region 核心异步方法
        /// <summary>
        /// 异步连接PLC
        /// </summary>
        /// <returns>错误码（0=成功）</returns>
        Task<int> ConnectAsync();

        /// <summary>
        /// 异步断开PLC连接
        /// </summary>
        /// <returns>错误码（0=成功）</returns>
        Task<int> DisconnectAsync();

        /// <summary>
        /// 异步读取单个PLC设备值（整型）
        /// </summary>
        /// <param name="deviceName">设备地址</param>
        /// <returns>（错误码，读取值）</returns>
        Task<(int ErrorCode, int Value)> ReadDeviceAsync(string deviceName);

        /// <summary>
        /// 异步读取单个PLC设备值（短整型）
        /// </summary>
        /// <param name="deviceName">设备地址</param>
        /// <returns>（错误码，读取值）</returns>
        Task<(int ErrorCode, short Value)> ReadDevice2Async(string deviceName);

        /// <summary>
        /// 异步写入单个PLC设备值（整型）
        /// </summary>
        /// <param name="deviceName">设备地址</param>
        /// <param name="value">要写入的值</param>
        /// <returns>错误码（0=成功）</returns>
        Task<int> WriteDeviceAsync(string deviceName, int value);

        /// <summary>
        /// 异步写入单个PLC设备值（短整型）
        /// </summary>
        /// <param name="deviceName">设备地址</param>
        /// <param name="value">要写入的值</param>
        /// <returns>错误码（0=成功）</returns>
        Task<int> WriteDevice2Async(string deviceName, short value);

        /// <summary>
        /// 异步批量读取PLC连续地址块（整型）
        /// </summary>
        /// <param name="startDevice">起始地址</param>
        /// <param name="size">读取长度</param>
        /// <returns>（错误码，读取数组）</returns>
        Task<(int ErrorCode, int[] Data)> ReadDeviceBlockAsync(string startDevice, int size);

        /// <summary>
        /// 异步批量写入PLC连续地址块（整型）
        /// </summary>
        /// <param name="startDevice">起始地址</param>
        /// <param name="data">要写入的数组</param>
        /// <returns>错误码（0=成功）</returns>
        Task<int> WriteDeviceBlockAsync(string startDevice, int[] data);

        /// <summary>
        /// 异步读取PLC CPU型号
        /// </summary>
        /// <returns>（错误码，CPU名称，CPU编码）</returns>
        Task<(int ErrorCode, string CpuName, int CpuCode)> GetCpuTypeAsync();

        /// <summary>
        /// 异步读取PLC时钟
        /// </summary>
        /// <returns>（错误码，PLC时钟）</returns>
        Task<(int ErrorCode, DateTime Clock)> GetPlcClockAsync();

        /// <summary>
        /// 异步设置PLC时钟
        /// </summary>
        /// <param name="dateTime">要设置的时间</param>
        /// <returns>错误码（0=成功）</returns>
        Task<int> SetPlcClockAsync(DateTime dateTime);
        #endregion
    }
}
