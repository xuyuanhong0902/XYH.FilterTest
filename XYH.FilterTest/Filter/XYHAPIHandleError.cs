using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Results;
using XYH.FilterTest.Model;

namespace XYH.FilterTest
{
    /// <summary>
    /// API自定义异常处理机制
    /// 说道异常处理，其实我们脑海中的第一反应，也该是try/cache操作
    /// 但是在实际开发中，很有可能地址错误根本就进入不到try中，又或者没有被try处理到异常
    /// 该类就发挥了作用，能够很好的未经捕获的异常，并做相应的逻辑处理
    /// 自定义异常机制，主要集成ExceptionFilterAttribute 重写其OnException方法
    /// </summary>
    public class XYHAPIHandleError : ExceptionFilterAttribute
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="actionExecutedContext">异常上下文</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            // 我们在平时的项目中，异常处理一般有两个作用
            // 1:记录异常的详细日志，便于事后分析日志
            // 2:对异常的统一友好处理，比如根据异常类型重定向到友好提示页面

            // 在这里面既能获取到未经处理的异常信息，也能获取到请求信息
            // 在此可以根据实际项目需要做相应的逻辑处理
            // 下面简单的列举了几个关键信息获取方式

            // action名称 
            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            // 控制器名称 
            string controllerName =actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

            // url完整地址
            string url = (actionExecutedContext.Request.RequestUri.AbsoluteUri).ExUrlDeCode();

            // 请求方式 post get
            string httpMethod = actionExecutedContext.Request.Method.Method;

            // 请求IP地址
            string ip = actionExecutedContext.Request.GetIPAddress();

            // 获取全部的请求参数
            HttpRequest httpRequest = HttpContext.Current.Request;
            Dictionary<string, string> queryParameters = httpRequest.GetAllQueryParameters();

            // 获取异常对象
            Exception ex = actionExecutedContext.Exception;

            // 异常描述信息
            string exMessage = ex.Message;

            // 异常堆栈信息
            string stackTrace = ex.StackTrace;

            // 根据实际情况记录日志（文本日志、数据库日志，建议具体步骤采用异步方式来完成）
            // 自己的记录日志落地逻辑略 ......

            // 构建统一的内部异常处理机制，相当于对异常做一层统一包装暴露
            MBaseResult<string> result = new MBaseResult<string>()
            {
                Code = MResultCodeEnum.systemErrorCode,
                Message = MResultCodeEnum.systemError
            };

            actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
            //需要自己指定输出内容和类型
            HttpContext.Current.Response.ContentType = "text/html;charset=utf-8";
            HttpContext.Current.Response.Write(JsonConvert.SerializeObject(result));
            HttpContext.Current.Response.End(); // 此处结束响应，就不会走路由系统
        }
    }
}