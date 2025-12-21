using System.Collections.Generic;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 返回JSON包：{"message":"ok","nu":"367847964498","ischeck":"1","com":"shunfeng","status":"200","condition":"F00","state":"3","data":[{"time":"2023-07-20 13:19:55","context":"查无结果","ftime":"2023-07-20 13:19:55"}]}
    /// 转换网址 https://www.bejson.com/convert/json2csharp/
    /// JSON数据的实体类
    /// </summary>

    public class MesPostRoot
    {
        public string MesUrl { get; set; }
        public MsePostData MesData { get; set; }
    }

    public class MsePostData
    {
        public MesInputJson input { get; set; }
    }

    public class MesInputJson
    {
        public string stepId { get; set; }
        public string lineId { get; set; }
        public string eqpId { get; set; }
        public string panelId { get; set; }
        public string fixture { get; set; }
    }

    public class MesResponseJson
    {
        public string code { get; set; }
        public string info { get; set; }
        public string data { get; set; }
    }

}