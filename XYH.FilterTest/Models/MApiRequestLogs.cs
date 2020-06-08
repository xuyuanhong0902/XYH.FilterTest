using System;
namespace XYH.FilterTest.Model
{
    /// <summary>
    /// API请求日志
    /// </summary>
    public class MApiRequestLogs
    {
        /// <summary>
        /// 主键ID 
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 请求IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// Token 
        /// </summary>
        public string TOKEN { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// API_KEY
        /// </summary>
        public string API_KEY { get; set; }

        /// <summary>
        /// 请求的API接口
        /// </summary>
        public string API { get; set; }

        /// <summary>
        /// 请求参数
        /// </summary>
        public string REQUEST_INFOR { get; set; }

        /// <summary>
        /// 请求返回值
        /// </summary>
        public string RESPONSE_INFOR { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREAT_TIME { get; set; }
    }
}