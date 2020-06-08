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
    /// 不需要做token认证的action
    /// </summary>
    public class XYHAPINoChekokenFilterAttribute : AuthorizationFilterAttribute
    {
      
    }
}