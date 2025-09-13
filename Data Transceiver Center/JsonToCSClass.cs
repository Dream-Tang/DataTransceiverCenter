using System.Collections.Generic;

namespace Data_Transceiver_Center
{
    /// <summary>
    /// 返回JSON包：{"message":"ok","nu":"367847964498","ischeck":"1","com":"shunfeng","status":"200","condition":"F00","state":"3","data":[{"time":"2023-07-20 13:19:55","context":"查无结果","ftime":"2023-07-20 13:19:55"}]}
    /// 转换网址 https://www.bejson.com/convert/json2csharp/
    /// JSON数据的实体类
    /// </summary>
    public class testApiRoot
    {
        /// <summary>
        ///
        /// </summary>
        public string message { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string nu { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ischeck { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string condition { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string com { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string status { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string state { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<testAPIDataItem> data { get; set; }
    }

    public class testAPIDataItem
    {
        /// <summary>
        ///
        /// </summary>
        public string time { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string ftime { get; set; }

        /// <summary>
        /// 已签收,感谢使用顺丰,期待再次为您服务
        /// </summary>
        public string context { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string location { get; set; }
    }

    /// <summary>
    /// MES返回JSON包：{"data":{"id":"8A2Y1Y0229B7AA","lcdType":"MD"},"code":"200","msg":"成功"}
    /// JSON数据的实体类(MES通信使用)
    /// </summary>
    public class MesData1 // 请求打印位置 api Json data数据
    {
        /// <summary>
        ///
        /// </summary>
        public string id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string lcdType { get; set; }
    }

    public class MesData2 // 请求打印 api Json data数据
    {
        /// <summary>
        ///
        /// </summary>
        public string panelId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string fogId { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string lcdType { get; set; }
    }

    public class MesRoot1
    {
        /// <summary>
        ///
        /// </summary>
        public MesData1 data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
    }

    public class MesRoot2
    {
        /// <summary>
        ///
        /// </summary>
        public MesData2 data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
    }

    public class MesRoot3
    {
        /// <summary>
        ///
        /// </summary>
        public string data { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string msg { get; set; }
    }

    public class MesSettiong
    {
        /// <summary>
        ///
        /// </summary>
        public string apiToken { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string mes1Par { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string mes2Par { get; set; }

        /// <summary>
        /// 成功
        /// </summary>
        public string mes3Par { get; set; }

    }
}