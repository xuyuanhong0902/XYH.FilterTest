﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace XYH.FilterTest
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Filters.Add(new XYHAPIAuthFilterAttribute());
            config.Filters.Add(new XYHAPICustomActionFilterAttribute());
            config.Filters.Add(new XYHAPIHandleError());

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
