using System;
using System.Collections.Generic;
using System.Text;

namespace XYH.FilterTest.Model
{
    /// <summary>
    /// 请求参数基础模型
    /// </summary>
    public class MRequestBase<T>
    {
        /// <summary>
        /// ApiKey 注册应用时分配到的 ApiKey
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 时间戳 UTC，系统时间的秒值
        /// </summary>
        public long Timestamp { get; set; }

        /// <summary>
        /// 签名类型 MD5 或者 SHA1
        /// </summary>
        public string Signtype { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// 具体的请求参数模型
        /// </summary>
        public T Data { get; set; }
    }
}
