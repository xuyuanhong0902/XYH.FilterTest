using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using XYH.FilterTest.Model;

namespace XYH.FilterTest
{
    /// <summary>
    /// 授权认证过滤器
    /// </summary>
    public class XYHAPIAuthFilterAttribute : AuthorizationFilterAttribute
    {
        /// <summary>
        /// 认证授权验证
        /// </summary>
        /// <param name="actionContext">请求上下文</param>
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            // 有 AllowAnonymous 属性的接口直接开绿灯
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
            {
                return;
            }

            // 在请求前做一层拦截，主要验证token的有效性和验签
            HttpRequest httpRequest = HttpContext.Current.Request;

            // 获取apikey
            var apikey = httpRequest.QueryString["apikey"];

            // 首先做IP白名单校验 
            MBaseResult<string> result = new AuthCheckService().CheckIpWhitelist(FilterAttributeHelp.GetIPAddress(actionContext.Request), apikey);

            // 检验时间搓
            string timestamp = httpRequest.QueryString["Timestamp"];
            if (result.Code == MResultCodeEnum.successCode)
            {
                // 检验时间搓 
                result = new AuthCheckService().CheckTimestamp(timestamp);
            }

            if (result.Code == MResultCodeEnum.successCode)
            {
                // 做请求频率验证 
                string acitonName = actionContext.ActionDescriptor.ActionName;
                string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                result = new AuthCheckService().CheckRequestFrequency(apikey, $"api/{controllerName.ToLower()}/{acitonName.ToLower()}");
            }

            if (result.Code == MResultCodeEnum.successCode)
            {
                // 签名校验

                // 获取全部的请求参数
                Dictionary<string, string> queryParameters = httpRequest.GetAllQueryParameters();

                result = new AuthCheckService().SignCheck(queryParameters, apikey);

                if (result.Code == MResultCodeEnum.successCode)
                {
                    // 如果有NoChekokenFilterAttribute 标签 那么直接不做token认证
                    if (actionContext.ActionDescriptor.GetCustomAttributes<XYHAPINoChekokenFilterAttribute>().Any())
                    {
                        return;
                    }

                    // 校验token的有效性
                    // 获取一个 token
                    string token = httpRequest.Headers.GetValues("Token") == null ? string.Empty :
                        httpRequest.Headers.GetValues("Token")[0];

                    result = new AuthCheckService().CheckToken(token, apikey, httpRequest.FilePath);
                }
            }

            // 输出
            if (result.Code != MResultCodeEnum.successCode)
            {
                // 一定要实例化一个response,是否最终还是会执行action中的代码
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
                //需要自己指定输出内容和类型
                HttpContext.Current.Response.ContentType = "text/html;charset=utf-8";
                HttpContext.Current.Response.Write(JsonConvert.SerializeObject(result));
                HttpContext.Current.Response.End(); // 此处结束响应，就不会走路由系统
            }
        }
    }
}