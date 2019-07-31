using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class ActionInfoController : BaseController
    {
        public IActionInfoService ActionInfoService { get; set; }
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        // GET: ActionInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllActionInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp=ActionInfoService.GetPageEntities(
                pageSize,
                pageIndex,
                out total,
                u => u.DelFlag == delFlagNormal,
                u => u.Id,
                true);
            var tempData=temp.Select(a => new { a.Id,a.IsMenu,a.Url,a.Remark,a.Sort,a.ActionName,a.ModfiliedOn,a.SubTime,a.HttpMethod,a.MenuIcon});
            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }

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

    }
}