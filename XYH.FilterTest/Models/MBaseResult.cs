using System;
using System.Collections.Generic;
using System.Text;

namespace XYH.FilterTest.Model
{
    /// <summary>
    /// 返回模型基类
    /// </summary>
    public class MBaseResult<T>
    {
        /// <summary>
        /// 返回结果编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 处理结果描述信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 返回结果数据模型：具体数据类型根据不同的接口来确定
        /// </summary>
        public T Data { get; set; }
    }
}
