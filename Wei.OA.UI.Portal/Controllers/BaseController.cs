using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Web.Mvc;

    using Spring.Context;
    using Spring.Context.Support;

    using Wei.OA.Common.Cache;
    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class BaseController:Controller
    {
        public bool IsCheckUserLogin = true;

        public UserInfo LoginUser { get; set; }

        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            #region 测试：去掉登录验证

            //return;

            #endregion

            if (IsCheckUserLogin)
            {
                #region 用户登录校验

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
                if (Request.Cookies["userLoginId"] == null)
                {
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }

                string userGuid = Request.Cookies["userLoginId"].Value;
                UserInfo userInfo = Common.Cache.CacheHelper.GetCache(userGuid) as UserInfo;
                if (userInfo == null)
                {
                    //用户长时间不操作，超时了
                    filterContext.HttpContext.Response.Redirect("/UserLogin/Index");
                    return;
                }
                LoginUser = userInfo;
                //滑动窗口机制
                Common.Cache.CacheHelper.SetCache(userGuid, userInfo, DateTime.Now.AddMinutes(20));

                #endregion

                #region 权限校验

                
                if (LoginUser.UName=="wei"|| LoginUser.UName == "张三"|| LoginUser.UName == "李四")
                {
                    return; //后门
                }

                string url = Request.Url.AbsolutePath.ToLower();
                string httpMethod = Request.HttpMethod.ToLower();

                // 基类注入必须通过子类，这里先不用属性注入，用spring容器直接获取
                IApplicationContext context = ContextRegistry.GetContext();
                IActionInfoService actionInfoService = context.GetObject("ActionInfoService") as IActionInfoService; //直接通过容器
                IR_UserInfo_ActionInfoService rUserInfoActionInfoService= context.GetObject("R_UserInfo_ActionInfoService") as IR_UserInfo_ActionInfoService;
                IUserInfoService userInfoService = context.GetObject("UserInfoService") as IUserInfoService;

                //拿到当前请求的权限数据
                var actionInfo =actionInfoService.GetEntities(u => u.Url.ToLower() == url && u.HttpMethod.ToLower() == httpMethod && u.DelFlag == this.delFlagNormal).FirstOrDefault();
                if (actionInfo==null)
                {
                    Response.Redirect("/Error.html");
                }

                //拿到当前用户的特殊权限,然后看一下是否包括上述请求权限
                var rUAs = rUserInfoActionInfoService.GetEntities(
                    u => u.UserInfoId == LoginUser.Id && u.DelFlag == this.delFlagNormal);

                var item = (from r in rUAs
                           where r.ActionInfoId == actionInfo.Id && r.DelFlag == this.delFlagNormal
                           select r).FirstOrDefault();
                if (item != null)
                {
                    if (item.HasPermission==true)
                    {
                        return; //说明有这个权限，放行
                    }
                    else
                    {
                        Response.Redirect("/Error.html"); //说明限制了这个权限，直接到错误页
                    }
                }
                
                //拿到当前用户的普通权限
                var user = userInfoService.GetEntities(u => u.Id == LoginUser.Id && u.DelFlag == this.delFlagNormal).FirstOrDefault();

                var allRoles = from r in user.RoleInfo
                               where r.DelFlag == this.delFlagNormal
                               select r;
                var actions = from r in allRoles
                              from a in r.ActionInfo
                              where a.DelFlag == this.delFlagNormal
                              select a;
                var temp = (from a in actions
                            where a.Id == actionInfo.Id
                            select a).Count();
                if (temp<=0)
                {
                    Response.Redirect("/Error.html"); //说明没有这个权限
                }
                #endregion
            }          
        }
    }
}