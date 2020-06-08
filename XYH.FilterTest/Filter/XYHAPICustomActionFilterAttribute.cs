using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XYH.FilterTest.Model;

namespace XYH.FilterTest
{
    /// <summary>
    /// Action过滤器
    /// </summary>
    public class XYHAPICustomActionFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Action执行开始
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

        }

        /// <summary>
        /// action执行以后
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            try
            {
                // 构建一个日志数据模型
                MApiRequestLogs apiRequestLogsM = new MApiRequestLogs();

                // API名称
                apiRequestLogsM.API = actionContext.Request.RequestUri.AbsolutePath;

                // apiKey
                apiRequestLogsM.API_KEY = HttpContext.Current.Request.QueryString["ApiKey"];

                // IP地址
                apiRequestLogsM.IP = FilterAttributeHelp.GetIPAddress(actionContext.Request);

                // 获取token
                string token = HttpContext.Current.Request.Headers.GetValues("Token") == null ? string.Empty :
                              HttpContext.Current.Request.Headers.GetValues("Token")[0];
                apiRequestLogsM.TOKEN = token;

                // URL
                apiRequestLogsM.URL = actionContext.Request.RequestUri.AbsoluteUri;

                // 返回信息
                var objectContent = actionContext.Response.Content as ObjectContent;
                var returnValue = objectContent.Value;
                apiRequestLogsM.RESPONSE_INFOR = returnValue.ToString();

                // 由于数据库中最大只能存储4000字符串，所以对返回值做一个截取
                if (!string.IsNullOrEmpty(apiRequestLogsM.RESPONSE_INFOR) &&
                    apiRequestLogsM.RESPONSE_INFOR.Length > 4000)
                {
                    apiRequestLogsM.RESPONSE_INFOR = apiRequestLogsM.RESPONSE_INFOR.Substring(0, 2000);
                }

                // 请求参数
                apiRequestLogsM.REQUEST_INFOR = actionContext.Request.RequestUri.Query;

                // 定义一个异步委托 ,异步记录日志
                //  Func<MApiRequestLogs, string> action = AddApiRequestLogs;//声明一个委托
                // IAsyncResult ret = action.BeginInvoke(apiRequestLogsM, null, null);

            }
            catch (Exception ex)
            {

            }
        }
    }
}