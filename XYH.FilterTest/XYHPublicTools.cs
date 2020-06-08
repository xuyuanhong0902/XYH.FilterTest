using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;

namespace XYH.FilterTest
{
    /// <summary>
    /// 公用操作帮助类
    /// </summary>
    public static class XYHPublicTools
    {
        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="encoding">解码方式 默认为utf8解码</param>
        /// <returns>解码结果</returns>
        public static string ExUrlDeCode(this string str, Encoding encoding = null)
        {
            // 默认为utf8解码
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return HttpUtility.UrlDecode(str, encoding);
        }

        /// <summary>
        /// url编码
        /// </summary>
        /// <param name="str">待加密字符串</param>
        /// <param name="encoding">解码方式 默认为utf8解码</param>
        /// <returns>编码结果</returns>
        public static string ExUrlEncode(this string str, Encoding encoding = null)
        {
            // 默认为utf8解码
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }

            return HttpUtility.UrlEncode(str, encoding);
        }

        /// <summary>
        /// 获取请求的所有参数键值对集合
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>所有参数键值对字典集合</returns>
        public static Dictionary<string, string> GetAllQueryParameters(this HttpRequest request)
        {
            // 请求参数对象
            NameValueCollection queryString = HttpContext.Current.Request.QueryString;

            // 请求表单
            NameValueCollection form = HttpContext.Current.Request.Form;

            // 请求参数字典集合
            Dictionary<string, string> queryParameters = new Dictionary<string, string>();

            // 获取url请求参数
            foreach (string key in queryString)
            {
                if (!queryParameters.ContainsKey(key))
                {
                    queryParameters.Add(key, queryString[key].ExUrlDeCode());
                }
            }

            // 获取表单参数
            foreach (string key in form)
            {
                if (!queryParameters.ContainsKey(key))
                {
                    queryParameters.Add(key, form[key]);
                }
            }

            return queryParameters;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>ip</returns>
        public static string GetIPAddress(this HttpRequestMessage request)
        {
            string ip = "";

            try
            {
                if (request.Properties.ContainsKey("MS_OwinContext"))
                {
                    ip = ((OwinContext)request.Properties["MS_OwinContext"]).Request.RemoteIpAddress;
                }
                else if (request.Properties.ContainsKey("MS_HttpContext"))
                {
                    ip = ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
                }
                else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    ip = ((RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name]).Address;
                }

                if (string.IsNullOrEmpty(ip))
                {
                    string strHostName = System.Net.Dns.GetHostName();
                    ip = System.Net.Dns.GetHostAddresses(strHostName).GetValue(0).ToString();
                }

                if (ip == "::1")
                {
                    ip = "127.0.0.1";
                }

                if (string.IsNullOrEmpty(ip) || !IsIP(ip))
                {
                    ip = "127.0.0.1";
                }
            }
            catch (Exception ex)
            {

                ip = "127.0.0.1";
            }

            return ip;
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>ip</returns>
        public static string GetIPAddress(this HttpRequestBase request)
        {
            string ip = string.Empty;
            try
            {
                ip = request.Headers.Get("x-forwarded-for");

                if (ip == null || ip.Length == 0 || string.Equals("unknown", ip, StringComparison.OrdinalIgnoreCase))
                {
                    ip = request.Headers.Get("Proxy-Client-IP");
                }
                if (ip == null || ip.Length == 0 || string.Equals("unknown", ip, StringComparison.OrdinalIgnoreCase))
                {
                    ip = request.Headers.Get("WL-Proxy-Client-IP");

                }
                if (ip == null || ip.Length == 0 || string.Equals("unknown", ip, StringComparison.OrdinalIgnoreCase))
                {
                    ip = request.UserHostAddress;
                }
            }
            catch (Exception ex)
            {
                ip = "127.0.0.1";
            }

            return ip;
        }

        /// <summary>
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip">ip地址</param>
        /// <returns>检查结果</returns>
        private static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}