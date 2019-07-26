using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal
{
    using Wei.OA.Model;
    using Wei.OA.UI.Portal.Models;

    public class FilterConfig
    {
        

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionFilterAttribute());
            //filters.Add(new LoginCheckFilterAttribute(){IsCheck = true});
        }
    }
}
