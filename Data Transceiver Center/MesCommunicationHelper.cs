using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transceiver_Center
{
    // ========== 必须配套的实体类（确保和原有代码一致） ==========
    // MesResult（新增RawResponse字段，其他不变）
    public class MesResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
        public string RawResponse { get; set; } // 新增：存储完整原始响应
    }

    public class MesCommunicationHelper
    {
        // 单例模式（保持全局唯一实例）
        private static readonly Lazy<MesCommunicationHelper> _instance = new Lazy<MesCommunicationHelper>(() => new MesCommunicationHelper());
        public static MesCommunicationHelper Instance => _instance.Value;

        // 静态 HttpClient（解决连接池问题，核心优化）
        private static readonly HttpClient _httpClient;

        // 静态构造函数：初始化 HttpClient（仅执行一次）
        static MesCommunicationHelper()
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(10); // 超时时间和原有一致
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MES-Client/1.0");
            _httpClient.DefaultRequestHeaders.ConnectionClose = false; // 复用连接
                                                                       // 新增：解决中文编码/Content-Type问题
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

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

        /// <summary>
        /// 发送MES请求（包含原始响应返回）
        /// </summary>
        /// <param name="url">MES接口地址</param>
        /// <param name="postData">请求参数</param>
        /// <returns>包含原始响应的MesResult</returns>
        public async Task<MesResult> SendMesRequestWithRawAsync(string url, MsePostData postData)
        {
            // 1. 初始化返回结果（必须赋值默认值，避免null）
            var result = new MesResult
            {
                Success = false,
                Message = "初始状态：未执行请求",
                Data = string.Empty,
                RawResponse = string.Empty
            };

            // 2. 前置校验：URL和参数非空
            if (string.IsNullOrWhiteSpace(url))
            {
                result.Message = "MES请求URL为空";
                Console.WriteLine($"[MES-RAW] 错误：{result.Message}");
                return result;
            }
            if (postData == null)
            {
                result.Message = "MES请求参数（MsePostData）为空";
                Console.WriteLine($"[MES-RAW] 错误：{result.Message}");
                return result;
            }

            try
            {
                // 3. 序列化请求参数（和原有函数逻辑一致）
                string postJson = JsonConvert.SerializeObject(postData);
                Console.WriteLine($"[MES-RAW] 请求URL：{url}");
                Console.WriteLine($"[MES-RAW] 请求参数：{postJson}");

                // 4. 构建请求内容（指定编码，避免中文乱码）
                //var content = new StringContent(postJson, Encoding.UTF8, "application/json");
                // 新增：强制指定Content-Type编码（解决部分MES服务解析失败）
                //content.Headers.ContentType.CharSet = "utf-8";

                // 新代码：强制Content-Type为纯application/json（不带charset）
                var content = new StringContent(postJson, Encoding.UTF8);
                // 手动设置Content-Type，仅保留application/json，去掉charset
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                // 5. 发送POST请求（核心步骤，await必须保留）
                Console.WriteLine("[MES-RAW] 开始发送POST请求...");
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);
                Console.WriteLine($"[MES-RAW] 响应状态码：{response.StatusCode}");

                // 6. 读取原始响应（无论成功/失败，都读取）
                string rawResponse = await response.Content.ReadAsStringAsync();
                result.RawResponse = rawResponse; // 赋值原始响应
                Console.WriteLine($"[MES-RAW] 原始响应内容：{rawResponse}");

                // 7. 处理HTTP成功响应（200-299）
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        // 8. 解析为MesResponseJson（和原有函数逻辑完全一致）
                        var mesResponse = JsonConvert.DeserializeObject<MesResponseJson>(rawResponse);
                        if (mesResponse != null)
                        {
                            // 9. 匹配原有成功判断逻辑（code="0"为成功）
                            if (mesResponse.code == "200")
                            {
                                result.Success = true;
                                result.Data = mesResponse.data ?? string.Empty;
                                result.Message = mesResponse.info ?? "MES响应成功（无信息）";
                            }
                            else
                            {
                                result.Message = $"MES返回错误码：{mesResponse.code}，信息：{mesResponse.info}";
                            }
                        }
                        else
                        {
                            // 解析为空的兜底处理
                            result.Message = "MES响应解析为MesResponseJson失败（空对象）";
                            result.Data = rawResponse; // 原始响应兜底
                        }
                    }
                    catch (JsonException ex)
                    {
                        // JSON解析失败的兜底
                        result.Message = $"MES响应JSON解析失败：{ex.Message}";
                        result.Data = rawResponse; // 保留原始响应
                        Console.WriteLine($"[MES-RAW] 解析异常：{ex}");
                    }
                }
                else
                {
                    // HTTP状态码失败（如404/500）
                    result.Message = $"HTTP请求失败，状态码：{response.StatusCode}，原因：{response.ReasonPhrase}";
                }
            }
            catch (TaskCanceledException ex)
            {
                // 单独捕获超时异常（最常见的问题）
                result.Message = $"MES请求超时（10秒）：{ex.Message}";
                result.RawResponse = ex.ToString(); // 保留异常堆栈
                Console.WriteLine($"[MES-RAW] 超时异常：{ex}");
            }
            catch (HttpRequestException ex)
            {
                // 网络异常（如URL无效、连接拒绝）
                result.Message = $"MES网络请求异常：{ex.Message}";
                result.RawResponse = ex.ToString();
                Console.WriteLine($"[MES-RAW] 网络异常：{ex}");
            }
            catch (Exception ex)
            {
                // 所有其他异常的兜底
                result.Message = $"MES通信未知异常：{ex.Message}";
                result.RawResponse = ex.ToString();
                Console.WriteLine($"[MES-RAW] 未知异常：{ex}");
            }

            // 最终返回结果（无论成功/失败，都有完整信息）
            return result;
        }
    }
    }