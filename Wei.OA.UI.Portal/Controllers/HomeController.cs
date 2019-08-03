using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Web.DynamicData;

    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class HomeController : BaseController
    {
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        public IUserInfoService UserInfoService { get; set; }
        public IActionInfoService ActionInfoService { get; set; }

        public ActionResult Index()
        {
            //if (Session["loginUser"] ==null)
            //{
            //    return RedirectToAction("Index", "UserLogin");
            //}
            ViewBag.AllMenu = this.LoadUserMenu();
            return View();
        }

        public List<ActionInfo> LoadUserMenu()
        {
            //拿到当前用户
            var userId = this.LoginUser.Id;
            var user = UserInfoService.GetEntities(u => u.Id == userId).FirstOrDefault();
            //拿到当前用户所有权限（必须是菜单类型的权限）
            //先拿到用户所有角色,然后找到对应权限
            var allRole = user.RoleInfo;
            var allRoleActionIds = (from r in allRole
                                    from a in r.ActionInfo
                                    select a.Id).ToList();
            //拿到用户权限中间表的被限制的权限的id
            var allDenyActionIds = (from r in user.R_UserInfo_ActionInfo
                                   where r.HasPermission == false
                                   select r.ActionInfoId).ToList();
            //权限与被限制的权限做差
            //var allActions = alllRoleActionIds.Where(u => !allDenyActionIds.Contains(u));
            var allActionIds = (from a in allRoleActionIds
                              where !allDenyActionIds.Contains(a)
                              select a).ToList();
            //拿到所有用户权限中间表中被允许的权限id
            var allUserActionIds = (from t in user.R_UserInfo_ActionInfo
                                  where t.HasPermission == true
                                  select t.ActionInfoId).ToList();
            //然后将二者做合并得到当前用户最终的所有权限
            allActionIds.AddRange(allUserActionIds);
            allActionIds = allActionIds.Distinct().ToList(); //去重操作
            //找到具有菜单权限的权限
            var menuActions=ActionInfoService.GetEntities(a => allActionIds.Contains(a.Id) && a.IsMenu == true&&a.DelFlag==this.delFlagNormal).ToList();
            //处理一下格式（links菜单格式），然后返回json数据
            //{ icon: '/Content/lib/images/3DSMAX.png', title: '用户列表', url: '/UserInfo/Index' },
            //var data = from m in menuActions
            //           select new { icon = m.MenuIcon, title = m.ActionName, url = m.Url };
            //return Json(data, JsonRequestBehavior.AllowGet);
            return menuActions;

        }

    }
}