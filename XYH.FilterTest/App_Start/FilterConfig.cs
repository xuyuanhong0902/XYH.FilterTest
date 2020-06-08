using System.Web;
using System.Web.Mvc;

namespace XYH.FilterTest
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new XYHMVCAuthorizeAttribute());
            filters.Add(new XYHMVCHandleError());
            filters.Add(new MyCustomerFilterAttribute());
            // filters.Add(new HandleErrorAttribute());
        }
    }
}
