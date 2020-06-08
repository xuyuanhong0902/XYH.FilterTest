using System;
using System.Collections.Generic;
using System.Text;

namespace XYH.FilterTest.Model
{
    /// <summary>
    /// 请求频率控制模型
    /// </summary>
    public class MRequestFrequency
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string APIName { get; set; }

        /// <summary>
        /// 最大访问数
        /// </summary>
        public int MaxRequestNum { get; set; }

        /// <summary>
        /// 限制请求秒（x秒内只能访问多少次）
        /// </summary>
        public int FrequencyUnit { get; set; }

        /// <summary>
        /// 限制结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 已经访问量
        /// </summary>
        public int HasRequestNum { get; set; }
    }
}
