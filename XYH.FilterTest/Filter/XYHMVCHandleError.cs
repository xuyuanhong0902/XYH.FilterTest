using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XYH.FilterTest
{
    /// <summary>
    /// MVC自定义异常处理机制
    /// 说道异常处理，其实我们脑海中的第一反应，也该是try/cache操作
    /// 但是在实际开发中，很有可能地址错误根本就进入不到try中，又或者没有被try处理到异常
    /// 该类就发挥了作用，能够很好的未经捕获的异常，并做相应的逻辑处理
    /// 自定义异常机制，主要集成HandleErrorAttribute 重写其OnException方法
    /// </summary>
    public class XYHMVCHandleError : HandleErrorAttribute
    {
        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="filterContext">异常上下文</param>
        public override void OnException(ExceptionContext filterContext)
        {
            // 我们在平时的项目中，异常处理一般有两个作用
            // 1:记录异常的详细日志，便于事后分析日志
            // 2:对异常的统一友好处理，比如根据异常类型重定向到友好提示页面

            // 在这里面既能获取到未经处理的异常信息，也能获取到请求信息
            // 在此可以根据实际项目需要做相应的逻辑处理
            // 下面简单的列举了几个关键信息获取方式

            // 控制器名称 注意，这样获取出来的是一个文件的全路径 
            string contropath = filterContext.Controller.ToString();

            // 访问目录的相对路径
            string filePath = filterContext.HttpContext.Request.FilePath;

            // url完整地址
            string url = (filterContext.HttpContext.Request.Url.AbsoluteUri).ExUrlDeCode();

            // 请求方式 post get
            string httpMethod = filterContext.HttpContext.Request.HttpMethod;

            // 请求IP地址
            string ip = filterContext.HttpContext.Request.GetIPAddress();

            // 获取全部的请求参数
            HttpRequest httpRequest = HttpContext.Current.Request;
            Dictionary<string, string> queryParameters = httpRequest.GetAllQueryParameters();

            // 获取异常对象
            Exception ex = filterContext.Exception;

            // 异常描述信息
            string exMessage = ex.Message;

            // 异常堆栈信息
            string stackTrace = ex.StackTrace;

            // 根据实际情况记录日志（文本日志、数据库日志，建议具体步骤采用异步方式来完成）


            filterContext.ExceptionHandled = true;

            // 模拟根据不同的做对应的逻辑处理
            int statusCode = filterContext.HttpContext.Response.StatusCode;

            if (statusCode>=400 && statusCode<500)
            {
                filterContext.Result = new RedirectResult("/html/404.html");
            }
            else 
            {
                filterContext.Result = new RedirectResult("/html/500.html");
            }
        }
    }
}