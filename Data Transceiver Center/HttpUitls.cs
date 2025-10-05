using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;

namespace Data_Transceiver_Center
{
    // http通信类，通过API获得JSON返回数据
    public class HttpUitls
    {
        public static string Get(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Proxy = null;
            request.KeepAlive = false;
            request.Method = "GET";
            request.ContentType = "application/json; charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();

                myStreamReader.Close();
                myResponseStream.Close();

                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }

                return retString;
            }
            catch (WebException)
            {
                return "无法连接到远程服务器";
            }
        }

        /// <summary>
        /// 发送HTTP POST请求并获取响应结果
        /// </summary>
        /// <param name="Url">请求的目标URL地址</param>
        /// <param name="Data">需要发送的POST数据（键值对格式，如"key1=value1&key2=value2"）</param>
        /// <param name="Referer">请求的来源页面（Referer头信息，用于模拟服务器识别请求来源）</param>
        /// <returns>服务器返回的响应字符串</returns>
        public static string PostJson(string url, string jsonData, string Referer="")
        {
            // 1. 验证URL格式（确保包含http/https）
            if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                throw new ArgumentException("URL格式无效，请包含http://或https://", nameof(url));
            }

            // 2. 验证JSON格式（简单校验）
            if (string.IsNullOrWhiteSpace(jsonData) ||
                !jsonData.StartsWith("{") || !jsonData.EndsWith("}"))
            {
                throw new ArgumentException("JSON数据格式无效", nameof(jsonData));
            }

            // 创建HTTP请求对象，指定请求的URL
            HttpWebRequest request = null;

            try
            {   // HttpWebRequest是用于发送HTTP请求的类
                request = (HttpWebRequest)WebRequest.Create(url);
                
                request.Method = "POST";// 设置请求方法为POST（区别于GET）
                request.Referer = Referer;// 设置Referer头信息，模拟浏览器的来源页面标识
                request.Proxy = null; // 禁用代理，避免代理导致的问题
                request.KeepAlive = false;
                // 增加了压缩解压支持，应对可能的压缩响应
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                // 关键优化：明确设置Accept头，告诉服务器客户端可接受的响应格式
                request.Accept = "application/json";
                // 设置请求内容类型为JSON格式
                // 这是发送JSON数据的关键，必须指定为application/json
                request.ContentType = "application/json";

                // 5. 处理请求数据
                byte[] dataBytes = Encoding.UTF8.GetBytes(jsonData);
                // 设置请求内容的长度（字节数），服务器据此判断数据是否完整
                request.ContentLength = dataBytes.Length;

                // 6.发送POST数据（使用using确保流正确释放）
                using (Stream requestStream = request.GetRequestStream())
                {
                    // 将JSON字节数组写入请求流
                    // 将字节数组中的数据写入请求流（发送到服务器）
                    // 参数分别为：数据字节数组、起始索引、写入长度
                    requestStream.Write(dataBytes, 0, dataBytes.Length);
                    requestStream.Flush(); // 确保数据全部发送
                }


                // 7.获取响应并处理结果
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // 检查HTTP状态码
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return $"请求失败，状态码: {response.StatusCode}";
                    }

                    using (StreamReader reader = new StreamReader(
                        response.GetResponseStream(), Encoding.UTF8))
                    {
                        return reader.ReadToEnd();
                    }
                }

            }
            catch (WebException ex)
            {

                // 详细捕获Web异常信息，便于调试
                if (ex.Response is HttpWebResponse errorResponse)
                {
                    //return $"错误: {errorResponse.StatusCode} ({(int)errorResponse.StatusCode})";
                    // 读取服务器返回的错误详情（很多服务器会说明不支持的类型）
                    using (StreamReader errorReader = new StreamReader(
                        errorResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        string errorDetails = errorReader.ReadToEnd();
                        return $"错误: {(int)errorResponse.StatusCode} {errorResponse.StatusCode}\n服务器说明: {errorDetails}";
                    }
                }
                return $"网络错误: {ex.Message}";
            }
            catch  (Exception ex)
            {
                return $"发生错误: {ex.Message}";
            }

            finally
            {
                request?.Abort();
            }
        }
    }
}
