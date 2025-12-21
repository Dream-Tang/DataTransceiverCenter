using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transceiver_Center
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
}
