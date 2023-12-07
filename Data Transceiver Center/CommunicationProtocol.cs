using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transceiver_Center
{
    class CommunicationProtocol
    {
        #region "定义PLC通信寄存器位置"
        // 相机动作交互寄存器
        public const string camRegister = "D1000";
        // 打印机动作交互寄存器
        public const string prtRegister = "D1001";
        // 扫码枪动作交互寄存器
        public const string scannerRegister = "D1002";
        #endregion

        #region "定义PLC通信信号数值"
        // 相机信号
        //允许相机拍照 = 1
        // 扫描OK放行 = 11
        // 扫描NG报警 取料 = 12
        public const short camReset = 0;
        public const short camAllow = 1;
        public const short camOK = 11;
        public const short camNG = 12;
        public const short camRetry = 13;
        // 打印机信号
        // 取标平台准备好 = 1
        // 标打印完成 = 11
        public const short prtReady = 1;
        public const short prtComplete = 11;
        // 扫码枪信号
        // 扫码开始 = 1
        // 扫码完成，标头返回 = 11
        public const short scannerStart = 1;
        public const short scannerComplete = 11;
        // 核验信号
        // 判定数据OK，放行 = 12
        // 判定数据NG，报警 = 13
        public const short checkOK = 12;
        public const short checkNG = 13;
        #endregion
    }
}
