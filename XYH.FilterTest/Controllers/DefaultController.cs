using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XYH.FilterTest.Controllers
{
    public class DefaultController : ApiController
    {
        /// <summary>
        /// 获取所有资源数据 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public string GetResource()
        {
            return "";
        }
    }
}
