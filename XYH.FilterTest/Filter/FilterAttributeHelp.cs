using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace XYH.FilterTest
{
    /// <summary>
    /// 过滤器帮助类
    /// </summary>
    public class FilterAttributeHelp
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="request">请求对象</param>
        /// <returns>ip</returns>
        public static string GetIPAddress(HttpRequestMessage request)
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
        /// 检查IP地址格式
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>检查结果</returns>
        private static bool IsIP(string ip)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(ip, @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$");
        }
    }
}