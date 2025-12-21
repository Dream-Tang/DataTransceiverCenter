using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 校验功能辅助类，统一处理校验逻辑
    /// </summary>
    public class CheckHelper
    {
        private readonly IDataProvider _dataProvider;   // 依赖接口，而非具体Form1
        private readonly IPLCService _plcService;       // 依赖抽象接口
        private readonly LogHelper _logHelper;          // 日志助手

        /// <summary>
        /// 构造函数注入：要求外部提供IDataProvider实现（如Form1）
        /// </summary>
        /// <param name="plcService">PLC服务接口</param>
        /// <param name="logger">日志接口</param>
        public CheckHelper(IDataProvider dataProvider, IPLCService plcService)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider)); // 验证接口是否存在
            _plcService = plcService ?? throw new ArgumentNullException(nameof(plcService)); // 验证接口是否存在
            _logHelper = LogHelper.Instance; // 单例LogHelper
        }

        // 1. 定义结果与PLC值的映射关系（公共配置）
        private readonly Dictionary<string, short> _resultToPlcValueMap = new Dictionary<string, short>
        {
            {"OK", CommunicationProtocol.checkOK},
            {"NG", CommunicationProtocol.checkNG},
            {"忽略", CommunicationProtocol.checkIgnore}
        };

        /// <summary>
        /// 执行校验逻辑
        /// </summary>
        /// <returns>校验结果（"验码OK"/"验码NG"/"屏蔽校验"）</returns>
        public string ExecuteCheck()
        {
            try
            {
                // 通过接口获取代码，而非直接访问Form1控件
                string scnCode = _dataProvider.GetScnCode();
                string prtCode = _dataProvider.GetPrtCode();
                bool ignoreCheck = _dataProvider.GetIgnoreCheck();

                _logHelper.Log("校验", "INFO", "开始执行扫码码/打印码校验");

                // 忽略校验时，强制返回OK
                if (ignoreCheck)
                {
                    _logHelper.Log("校验", "WARN", "已勾选忽略校验，强制返回OK");
                    return "OK";
                }

                // 校验时，若校验数据为空，返回NG
                if (string.IsNullOrEmpty(scnCode) || string.IsNullOrEmpty(prtCode))
                {
                    _logHelper.Log("校验", "WARN", "扫码码/打印码为空，返回NG");
                    return "NG";
                }

                // 其余时候，根据校验是否相等返回OK或NG
                string result = scnCode.Equals(prtCode, StringComparison.OrdinalIgnoreCase) ? "OK" : "NG";
                _logHelper.Log("校验", "INFO", $"校验结果：{result}（扫码码：{scnCode} | 打印码：{prtCode}）");
                return result;
            }
            catch (Exception ex)
            {
                _logHelper.Log("校验", "ERROR", $"校验执行失败: {ex.Message}");
                return "校验异常";
            }
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
                if (ignorePlc || !_resultToPlcValueMap.ContainsKey(checkResult))
                    return;

                // 2. 直接使用映射关系，避免重复判断
                short scnValue = _resultToPlcValueMap[checkResult];

                // 通过接口操作PLC，而非直接依赖Form2
                if (_plcService.InvokeRequired)
                {
                    _plcService.Invoke(new Action(() => _plcService.SafeWritePlc(scnValue)));
                }
                else
                {
                    _plcService.SafeWritePlc(scnValue);
                }
                _logHelper.Log("校验->PLC", "INFO", $"已发送校验结果到PLC: {scnValue}");
            }
            catch (Exception ex)
            {
                _logHelper.Log("校验->PLC", "ERROR", $"更新PLC校验状态失败: {ex.Message}");
                _logHelper.Log("校验->PLC", "ERROR", $"异常堆栈: {ex.StackTrace}");
            }
        }
    }
}