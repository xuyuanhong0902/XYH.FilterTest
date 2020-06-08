using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace XYH.FilterTest.Controllers
{
    /// <summary>
    /// API 测试控制器
    /// </summary>
    public class APITestController : ApiController
    {
        /// <summary>
        ///  获取数据
        /// </summary>
        /// <returns>获取到数据</returns>
        [HttpGet]
        public string GetData()
        {
            // 业务代码省略 ...
            return $"获取到的数据为：{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        /// <summary>
        /// 登录-授权 不需要做认证授权验证
        /// </summary>
        /// <returns>结果</returns>
        [HttpGet]
        [XYHAPINoChekokenFilter]
        public string Login()
        {
            // 业务代码省略 ...
            return $"登录成功：{System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        /// <summary>
        ///  未处理异常测试
        /// </summary>
        /// <returns>获取到数据</returns>
        [HttpGet]
        public string ExTest()
        {
            // 测试一下异常过滤器
            int num = 0;
            num = 1 / num;

            // 业务代码省略 ...
            return string.Empty ;
        }
    }
}
