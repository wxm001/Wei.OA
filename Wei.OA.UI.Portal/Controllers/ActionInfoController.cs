using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.BLL;
    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class ActionInfoController : BaseController
    {
        public IActionInfoService ActionInfoService { get; set; }

        public IRoleInfoService RoleInfoService { get; set; }

        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        // GET: ActionInfo

        #region 获取

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllActionInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = ActionInfoService.GetPageEntities(
                pageSize,
                pageIndex,
                out total,
                u => u.DelFlag == delFlagNormal,
                u => u.Id,
                true);
            var tempData = temp.Select(a => new { a.Id, a.IsMenu, a.Url, a.Remark, a.Sort, a.ActionName, a.ModfiliedOn, a.SubTime, a.HttpMethod, a.MenuIcon });
            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Add


        public ActionResult Add(ActionInfo actionInfo)
        {
            actionInfo.ModfiliedOn=DateTime.Now;
            actionInfo.SubTime=DateTime.Now;
            actionInfo.DelFlag = this.delFlagNormal;
            actionInfo.IsMenu = Request["IsMenu"] == "true" ? true : false;
            ActionInfoService.Add(actionInfo);
            return Content("ok");
        }

        #endregion

        #region Delete

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


            ActionInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }

        #endregion

        #region 修改


        public ActionResult Edit(int id)
        {
            ActionInfo actionInfo = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            actionInfo.SubTime = DateTime.Now;
            ViewData.Model = actionInfo;
            return this.View();
        }

        [HttpPost]
        public ActionResult Edit(ActionInfo actionInfo)
        {
            ActionInfoService.Update(actionInfo);
            return Content("ok");
        }

        #endregion

        #region 上传图片

        public ActionResult UploadImage()
        {
            var file = Request.Files["fileMenuIcon"];
            string path = "/UploadFiles/UploadImages/" + Guid.NewGuid().ToString() + "-" + file.FileName;
            file.SaveAs(Request.MapPath(path));
            return Content(path);
        }

        #endregion

        #region 设置角色

        public ActionResult SetRole(int id)
        {
            int userId = id; //当前设置角色的权限
            ActionInfo actionInfo = ActionInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            //和viewdata.model差不多，把所有角色发到前台
            ViewBag.AllRoles = RoleInfoService.GetEntities(u => u.DelFlag == this.delFlagNormal).ToList();
            //权限已经关联的角色发到前台
            ViewBag.ExitsRoles = (from r in actionInfo.RoleInfo select r.Id).ToList();

            return this.View(actionInfo);
        }

        //设置角色处理
        public ActionResult ProcessSetRole()
        {
            List<int> setRoleIdList = new List<int>();
            //拿到当前权限id
            int actionId = int.Parse(Request.Form["ActionId"]);
            //拿到打勾 的角色id
            foreach (var key in Request.Form.AllKeys)
            {
                if (key.StartsWith("ckb_"))
                {
                    int roleId = int.Parse(key.Replace("ckb_", ""));
                    setRoleIdList.Add(roleId);
                }
            }

            ActionInfoService.SetRole(actionId, setRoleIdList);
            return Content("ok");
        }

        #endregion

    }
}