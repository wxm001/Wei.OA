using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Web.Mvc;

    using Wei.OA.Model;

    public class BaseController:Controller
    {
        public bool IsCheckUserLogin { get; set; }

        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            //校验用户是否已经登录
            if (IsCheckUserLogin)
            {
                if (filterContext.HttpContext.Session["loginUser"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                }
                else
                {
                    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo;
                }
            }          
        }
    }
}