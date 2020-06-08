using System;
using System.Collections.Generic;
using System.Text;

namespace XYH.FilterTest.Model
{
    /// <summary>
    /// 处理结果编码枚举关系
    /// </summary>
    public class MResultCodeEnum
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        public const string successCode = "10000";

        /// <summary>
        /// 处理成功
        /// </summary>
        public const string success = "处理成功";

        /// <summary>
        /// 时间戳失效->更改时间戳 
        /// </summary>
        public const string timestampInvalidCode = "10001";

        /// <summary>
        /// 时间戳失效->更改时间戳 
        /// </summary>
        public const string timestampInvalid = "时间戳失效";

        /// <summary>
        /// 接口不被支持->传递正确的请求接口
        /// </summary>
        public const string methodErrorCode = "10002";

        /// <summary>
        /// 接口不被支持->传递正确的请求接口
        /// </summary>
        public const string methodError = "接口不被支持";

        /// <summary>
        /// 应用对 web api 接口的调用请求数达->暂停暂停 10s 在访问，并降低访问频率
        /// </summary>
        public const string frequencyTooFastCode = "10003";

        /// <summary>
        /// 应用对 web api 接口的调用请求数达->暂停暂停 10s 在访问，并降低访问频率
        /// </summary>
        public const string frequencyTooFast = "应用对web api接口的调用请求数达到上限";

        /// <summary>
        /// 调用端的 IP 未被授权-> 将请求 IP 给我方，系统配置到 IP 白名单
        /// </summary>
        public const string IPIllegalCode = "10004";

        /// <summary>
        /// 调用端的 IP 未被授权-> 将请求 IP 给我方，系统配置到 IP 白名单
        /// </summary>
        public const string IPIllegal = "IP 未被授权";

        /// <summary>
        /// 参数无效或缺失 -> 参照 Message 做参数调整
        /// </summary>
        public const string parameterErrorCode = "10005";

        /// <summary>
        /// 参数无效或缺失 -> 参照 Message 做参数调整
        /// </summary>
        public const string parameterError = "参数无效或缺失";

        /// <summary>
        /// Api key 无效 -> 传递正确的 Api Key
        /// </summary>
        public const string apiKeyErrorCode = "10006";

        /// <summary>
        /// Api key 无效 -> 传递正确的 Api Key
        /// </summary>
        public const string apiKeyError = "Api key 无效";

        /// <summary>
        /// 签名无效 -> 参照要求进行签名
        /// </summary>
        public const string signErrorCode = "10007";

        /// <summary>
        /// 签名无效 -> 参照要求进行签名
        /// </summary>
        public const string signError = "签名无效";

        /// <summary>
        /// 参数签名算法未被平台所支持 -> 修改签名方式
        /// </summary>
        public const string signTypeErrorCode = "10008";

        /// <summary>
        /// 参数签名算法未被平台所支持 -> 修改签名方式
        /// </summary>
        public const string signTypeError = "参数签名算法未被平台所支持";

        /// <summary>
        /// 未被授权 -> 传递正确的授权token
        /// </summary>
        public const string tokenErrorCode = "10009";

        /// <summary>
        /// 未被授权 -> 传递正确的授权token
        /// </summary>
        public const string tokenError = "未被授权,传递正确的授权token";

        /// <summary>
        /// 系统错误
        /// </summary>
        public const string systemErrorCode = "10010";

        /// <summary>
        /// 系统错误
        /// </summary>
        public const string systemError = "系统错误";
    }
}