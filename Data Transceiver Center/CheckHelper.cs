using System;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 校验功能辅助类，统一处理校验逻辑
    /// </summary>
    public class CheckHelper
    {
        private readonly Form1 _form1;
        private readonly Form2 _form2;
        private readonly ILogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="form1">主窗体实例</param>
        /// <param name="form2">PLC窗体实例</param>
        /// <param name="logger">日志接口</param>
        public CheckHelper(Form1 form1, Form2 form2, ILogger logger)
        {
            _form1 = form1 ?? throw new ArgumentNullException(nameof(form1));
            _form2 = form2 ?? throw new ArgumentNullException(nameof(form2));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 执行校验逻辑
        /// </summary>
        /// <returns>校验结果</returns>
        public string ExecuteCheck()
        {
            try
            {
                _logger.Log("校验", "INFO", "开始执行校验逻辑");

                // 线程安全获取校验结果
                string checkResult = GetCheckResultSafely();

                _logger.Log("校验", "INFO", $"校验结果: {checkResult}");
                return checkResult;
            }
            catch (Exception ex)
            {
                _logger.Log("校验", "ERROR", $"校验执行失败: {ex.Message}");
                _logger.Log("校验", "ERROR", $"异常堆栈: {ex.StackTrace}");
                return "ERROR";
            }
        }

        /// <summary>
        /// 线程安全获取校验结果
        /// </summary>
        private string GetCheckResultSafely()
        {
            if (_form1.InvokeRequired)
            {
                return (string)_form1.Invoke(new Func<string>(() => _form1.GetCheckResult()));
            }
            return _form1.GetCheckResult();
        }

        /// <summary>
        /// 根据校验校验结果更新PLC状态
        /// </summary>
        /// <param name="checkResult">校验结果</param>
        /// <param name="ignorePlc">是否忽略PLC操作</param>
        public void UpdatePlcByCheckResult(string checkResult, bool ignorePlc)
        {
            try
            {
                if (ignorePlc) // 屏蔽PLC时直接返回
                {
                    _logger.Log("校验", "INFO", "已忽略PLC，不更新校验结果");
                    return;
                }

                // 屏蔽校验时，强制checkResult为OK
                if (_form1.ignoreCheck)
                {
                    checkResult = "OK";
                    _logger.Log("校验", "INFO", "屏蔽校验状态，强制结果为OK");
                }

                short scnValue;
                switch (checkResult)
                {
                    case "OK":
                        scnValue = CommunicationProtocol.checkOK;
                        break;
                    case "NG":
                        scnValue = CommunicationProtocol.checkNG;
                        break;
                    default:
                        scnValue = CommunicationProtocol.checkIgnore;
                        break;
                }

                // 直接调用Form2的重载方法，仅更新scn寄存器（其他寄存器保持原值）
                if (_form2.InvokeRequired)
                {
                    _form2.Invoke(new Action(() => _form2.SafeWritePlc(scn: scnValue)));
                }
                else
                {
                    _form2.SafeWritePlc(scn: scnValue);
                }
                _logger.Log("校验", "INFO", $"已发送校验结果到PLC: {scnValue}");
            }
            catch (Exception ex)
            {
                _logger.Log("校验", "ERROR", $"更新PLC校验状态失败: {ex.Message}");
                _logger.Log("校验", "ERROR", $"异常堆栈: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// 线程安全更新PLC
        /// </summary>
        private void UpdatePlcSafely(short cam, short prt, short scn)
        {
            if (_form2.InvokeRequired)
            {
                _form2.Invoke(new Action(() => _form2.SafeWritePlc(cam, prt, scn)));
            }
            else
            {
                _form2.SafeWritePlc(cam, prt, scn);
            }
        }
    }

    /// <summary>
    /// 日志接口，统一日志调用
    /// </summary>
    public interface ILogger
    {
        void Log(string module, string level, string message);
    }
}