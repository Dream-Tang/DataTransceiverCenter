using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Data_Transceiver_Center
{
    public class MesCommunicationHelper
    {
        // 单例模式（保持全局唯一实例）
        private static readonly Lazy<MesCommunicationHelper> _instance = new Lazy<MesCommunicationHelper>(() => new MesCommunicationHelper());
        public static MesCommunicationHelper Instance => _instance.Value;

        // 私有构造函数，禁止外部实例化
        private MesCommunicationHelper() { }

        /// <summary>
        /// 向MES发送请求（复用HttpUitls.PostJson）
        /// </summary>
        /// <param name="postUrl">MES接口地址</param>
        /// <param name="mesData">发送的数据对象（MsePostData）</param>
        /// <returns>(是否成功, 响应数据, 错误信息)</returns>
        public async Task<(bool Success, string Data, string Message)> SendMesRequestAsync(string postUrl, MsePostData mesData)
        {
            try
            {
                // 1. 参数校验
                if (string.IsNullOrEmpty(postUrl))
                {
                    return (false, null, "MES接口URL为空");
                }
                if (mesData == null || mesData.input == null)
                {
                    return (false, null, "发送数据不能为空");
                }

                // 2. 序列化数据（与原有逻辑一致）
                string postJson = JsonConvert.SerializeObject(mesData);
                Console.WriteLine($"发送MES请求: {postJson}");

                // 3. 调用原有HttpUitls.PostJson（核心：复用原有HTTP工具类）
                // 注意：HttpUitls.PostJson是同步方法，用Task.Run包装为异步
                string responseString = await Task.Run(() =>
                    HttpUitls.PostJson(postUrl, postJson)
                );

                // 4. 处理响应（与原逻辑一致）
                if (string.IsNullOrEmpty(responseString))
                {
                    return (false, null, "MES返回空响应");
                }

                // 5. 解析响应JSON
                var mesResponse = JsonConvert.DeserializeObject<MesResponseJson>(responseString);
                if (mesResponse == null)
                {
                    return (false, null, $"解析响应失败，原始响应: {responseString}");
                }

                // 6. 根据MES返回的code判断结果（与原逻辑一致）
                if (mesResponse.code == "0" || mesResponse.code == "200") // 兼容常见成功码
                {
                    return (true, mesResponse.data, $"MES响应成功: {mesResponse.info}");
                }
                else
                {
                    return (false, null, $"MES业务错误: {mesResponse.info} (Code: {mesResponse.code})");
                }
            }
            catch (JsonException ex)
            {
                return (false, null, $"JSON序列化/反序列化错误: {ex.Message}");
            }
            catch (Exception ex)
            {
                return (false, null, $"请求处理失败: {ex.Message}");
            }
        }
    }
}