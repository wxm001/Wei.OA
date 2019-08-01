using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Web.UI.WebControls;

    using Wei.OA.BLL;
    using Wei.OA.IBLL;
    using Wei.OA.Model;
    using Wei.OA.Model.Param;

    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        //IUserInfoService userInfoService=new UserInfoService();
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        public UserInfoService UserInfoService { get; set; } //属性注入，在配置文件中赋值
        public IRoleInfoService RoleInfoService { get; set; }//也要注入
        public IActionInfoService ActionInfoService { get; set; }//也要注入
        public IR_UserInfo_ActionInfoService R_UserInfo_ActionInfoService { get; set; }//也要注入
        #region 获取用户
        public ActionResult Index()
        {
            ViewData.Model = this.UserInfoService.GetEntities(u => true);
            return View();
        }

        public ActionResult GetAllUserInfos()
        {
            //jquery easyui:table:{total:32,rows:[{},{}]}
            //easy ：table在初始化的时候自动发送一下两个参数值
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;


            //过滤用户名  过滤备注 ,没有过滤信息直接拿到所有
            //拿到当前页的数据,带有导航属性时，使用select过滤一下
            string schName = Request["schName"];
            string schRemark = Request["schRemark"];



            var queryParam = new UserQueryParam()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Total = 0,
                SchName = schName,
                SchRemark = schRemark
            };
            var pageData = UserInfoService.LoadPageData(queryParam);
            var temp = pageData.Select(
                u => new
                {
                    u.Id,
                    u.UName,
                    u.Remark,
                    u.ShowName,
                    u.SubTime,
                    u.ModfiliedOn,
                    u.Pwd
                });
            ////拿到当前页的数据,带有导航属性时，使用select过滤一下
            //var pageData = UserInfoService.GetPageEntities(
            //    pageSize,
            //    pageIndex,
            //    out total,
            //    u => u.DelFlag == delFlagNormal,
            //    u => u.Id,
            //    true).Select(u => new { u.Id, u.UName, u.Remark, u.ShowName, u.SubTime, u.ModfiliedOn, u.Pwd });
            var data = new { total = queryParam.Total, rows = temp.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加用户

        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.SubTime = DateTime.Now;
            userInfo.ModfiliedOn = DateTime.Now;
            userInfo.DelFlag = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userInfo);

            return Content("ok");
        }

        #endregion

        #region 删除用户

        public ActionResult Delete(string strId)
        {
            if (string.IsNullOrEmpty(strId))
            {
                return Content("请选择要删除的数据！");
            }
            //正常处理
            string[] strIds = strId.Split(',');
            List<int> idList = new List<int>();
            foreach (var str in strIds)
            {
                idList.Add(int.Parse(str));
            }

            UserInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }

        #endregion

        #region 修改用户


        public ActionResult Edit(int id)
        {
            UserInfo userInfo = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            userInfo.SubTime = DateTime.Now;
            ViewData.Model = userInfo;
            return this.View();
        }

        [HttpPost]
        public ActionResult Edit(UserInfo userInfo)
        {
            UserInfoService.Update(userInfo);
            return Content("ok");
        }

        #endregion

        #region Create
        public ActionResult Create()
        {
            return this.View();
        }
        [HttpPost]
        public ActionResult Create(UserInfo userInfo)
        {
            if (ModelState.IsValid)
            {
                this.UserInfoService.Add(userInfo);
            }

            return RedirectToAction("Index");
        }
        #endregion

        #region 设置角色

        public ActionResult SetRole(int id)
        {
            int userId = id; //当前设置角色的用户
            UserInfo userInfo = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            //和viewdata.model差不多，把所有角色发到前台
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == this.delFlagNormal).ToList();
            //用户已经关联的角色发到前台
            ViewBag.ExitsRoles = (from r in userInfo.RoleInfo select r.Id).ToList();

            return this.View(userInfo);
        }

        //设置角色处理
        public ActionResult ProcessSetRole()
        {
            List<int> setRoleIdList = new List<int>();
            //拿到当前用户id
            int uId = int.Parse(Request.Form["UId"]);
            //拿到打勾 的角色id
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }

            UserInfoService.SetRole(uId, setRoleIdList);
            return Content("ok");
        }

        #endregion

        #region 设置特殊权限

        public ActionResult SetAction(int id)
        {
            ViewBag.User = UserInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            ViewData.Model = ActionInfoService.GetEntities(a => a.DelFlag == this.delFlagNormal).ToList();
            return this.View();
        }
        //删除特殊权限
        public ActionResult DeleteUserAction(int uId,int actionId)
        {
            var rUserAction = R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoId == actionId && r.UserInfoId == uId).FirstOrDefault();
            if (rUserAction!=null)
            {
                //rUserAction.DelFlag = (short)Wei.OA.Model.Enum.DelFlagEnum.Deleted;
                R_UserInfo_ActionInfoService.DeleteListByLogical(new List<int>() { rUserAction.Id });
            }
            return Content("ok");
        }

        //设置当前用户的特殊权限
        public ActionResult SetUserAction(int uId,int actionId,int value)
        {
            var rUserAction = R_UserInfo_ActionInfoService.GetEntities(r => r.ActionInfoId == actionId && r.UserInfoId == uId).FirstOrDefault();
            if (rUserAction != null)
            {
               
            }
        }

        #endregion
    }
}