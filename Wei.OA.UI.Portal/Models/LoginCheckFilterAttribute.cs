using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wei.OA.UI.Portal.Models
{
    using System.Web.Http.Controllers;
    using System.Web.Http.Filters;
    using System.Web.Mvc;

    using ActionFilterAttribute = System.Web.Mvc.ActionFilterAttribute;

    public class LoginCheckFilterAttribute:ActionFilterAttribute
    {
        public bool IsCheck { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            
            //校验用户是否已经登录
            if (IsCheck)
            {
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
            }
        }
    }
}