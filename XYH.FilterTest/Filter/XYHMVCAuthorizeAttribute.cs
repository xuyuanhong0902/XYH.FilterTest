using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XYH.FilterTest
{
    /// <summary>
    /// MVC自定义授权
    /// 认证授权有两个重写方法
    /// 具体的认证逻辑实现：AuthorizeCore 这个里面写具体的认证逻辑，认证成功返回true,反之返回false
    /// 认证失败处理逻辑：HandleUnauthorizedRequest 前一步返回 false时，就会执行到该方法中
    /// 但是，我平时在应用过程中，一般都是在AuthorizeCore根据不同的认证结果，直接做认证后的逻辑处理
    /// </summary>
    public class XYHMVCAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 认证逻辑
        /// </summary>
        /// <param name="filterContext">过滤器上下文</param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            // 此处主要写认证授权的相关验证逻辑
            // 该部分的验证一般包括两个部分
            // 登录权限校验
            //   --我们的一般处理方式是，通过header中传递一个token来进行逻辑验证
            //   --当然不同的系统在设计上也不尽相同，有的也会采用session等方式来验证
            //   --所以最终还是根据其项目本身的实际情况来进行对应的逻辑操作

            // 具体的页面权限校验
            // --该部分的验证是具体的到页面权限验证
            // --我看有得小伙伴没有做到这一个程度,直接将这一步放在前端js来验证，这样不是很安全，但是可以拦住小白用户
            // --当然有的系统根本就没有做权限控制，那就更不需要这一个逻辑了。
            // --所以最终还是根据其项目本身的实际情况来进行对应的逻辑操作

            // 现在用一个粗暴的方式来简单模拟实现过，用系统当前时间段秒厨艺3，取余数
            // 当余数为0：认证授权通过
            //         1:代表为登录，调整至登录页面
            //         2:代表无访问权限，调整至无权限提示页面

            // 当然，在这也还可以做一些IP白名单，IP黑名单验证  请求频率验证等等

            // 说到这而，还有一点需要注意，如果我们选择的是全局注册该过滤器，那么如果有的页面根本不需要权限认证，比如登录页面，那么我们可以给不需要权限的认证的控制器或者action添加一个特殊的注解 AllowAnonymous ，来排除

            // 获取Request的几个关键信息
            HttpRequest httpRequest = HttpContext.Current.Request;
            string acitonName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            // 注意：如果认证不通过，需要设置filterContext.Result的值，否则还是会执行action中的逻辑

            filterContext.Result = null;
            int thisSecond = System.DateTime.Now.Second;
            switch (thisSecond % 3)
            {
                case 0:
                    // 认证授权通过
                    break;
                case 1:
                    // 代表为登录，调整至登录页面
                    // 只有设置了Result才会终结操作
                    filterContext.Result = new RedirectResult("/html/Login.html");
                    break;
                case 2:
                    // 代表无访问权限，调整至无权限提示页面
                    filterContext.Result = new RedirectResult("/html/NoAuth.html");
                    break;
            }
        }
    }
}