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
        public bool IsCheckUserLogin = true;

        public UserInfo LoginUser { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (IsCheckUserLogin)
            {
                //校验用户是否已经登录
                //if (filterContext.HttpContext.Session["loginUser"] == null)
                //{
                //    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                //}
                //else
                //{
                //    LoginUser = filterContext.HttpContext.Session["loginUser"] as UserInfo;
                //}


                //使用memcache+cookie代替session
                //从缓存中拿到当前登录的用户信息
                if (Request.Cookies["userLoginId"] ==null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo==null)
                {
                    //用户长时间不操作，超时了
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                LoginUser = userInfo;
                //滑动窗口机制
                Common.Cache.CacheHelper.SetCache(userGuid,userInfo,DateTime.Now.AddMinutes(20));

            }          
        }
    }
}