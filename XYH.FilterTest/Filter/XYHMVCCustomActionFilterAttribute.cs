using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XYH.FilterTest
{
    /// <summary>
    /// MVC自定义授权
    /// </summary>
    public class MyCustomerFilterAttribute : ActionFilterAttribute
    {
        public string Message { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string acitonName = filterContext.ActionDescriptor.ActionName;

            base.OnActionExecuted(filterContext);
            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Action finish Execute.....", Message));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string acitonName = filterContext.ActionDescriptor.ActionName;

            CheckMessage(filterContext);
            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Action start Execute.....", Message));
            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Action finish Result.....", Message));
            base.OnResultExecuted(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            filterContext.HttpContext.Response.Write(string.Format("<br/> {0} Action start Execute.....", Message));
            base.OnResultExecuting(filterContext);
        }

        private void CheckMessage(ActionExecutingContext filterContext)
        {
            if (string.IsNullOrEmpty(Message) || string.IsNullOrWhiteSpace(Message))
                Message = filterContext.Controller.GetType().Name + "'s " + filterContext.ActionDescriptor.ActionName;
        }
    }
}