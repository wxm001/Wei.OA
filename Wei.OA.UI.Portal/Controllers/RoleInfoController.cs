using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class RoleInfoController : BaseController
    {
        // GET: RoleInfo
        public IRoleInfoService RoleInfoService { get; set; }
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllRoleInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = RoleInfoService.GetPageEntities(
                pageSize,
                pageIndex,
                out total,
                u => u.DelFlag == delFlagNormal,
                u => u.Id,
                true);
            var tempData = temp.Select(a => new { a.Id, a.Remark, a.RoleName, a.ModfiliedOn, a.SubTime, a.DelFlag });
            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region 添加

        public ActionResult Add(RoleInfo roleInfo)
        {
            roleInfo.ModfiliedOn = DateTime.Now;
            roleInfo.SubTime = DateTime.Now;            
            roleInfo.DelFlag = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;

            RoleInfoService.Add(roleInfo);

            return Content("ok");
        }

        #endregion

        #region 删除

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

            RoleInfoService.DeleteListByLogical(idList);

            return Content("ok");
        }

        #endregion

        #region 修改


        public ActionResult Edit(int id)
        {
            RoleInfo roleInfo = RoleInfoService.GetEntities(u => u.Id == id).FirstOrDefault();
            roleInfo.SubTime = DateTime.Now;
            ViewData.Model = roleInfo;
            return this.View();
        }

        [HttpPost]
        public ActionResult Edit(RoleInfo roleInfo)
        {
            RoleInfoService.Update(roleInfo);
            return Content("ok");
        }

        #endregion

    }
}