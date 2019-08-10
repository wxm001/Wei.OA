using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class WFTempController : Controller
    {
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        public IWF_TempService WF_TempService { get; set; }

        // GET: WFTemp
        public ActionResult Index()
        {
            return View();
        }

        #region 加载数据

        public ActionResult GetAllTempInfos()
        {
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;

            var temp = WF_TempService.GetPageEntities(
                pageSize,
                pageIndex,
                out total,
                u => u.DelFlag == delFlagNormal,
                u => u.Id,
                true);
            var tempData = temp.Select(a => new { a.Id, a.TempName, a.ActivityType, a.Remark, a.SubTime, a.DelFlag });
            var data = new { total = total, rows = tempData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region 添加
        //显示添加页面
        public ActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)] //特殊字符处理
        public ActionResult Add(WF_Temp temp)
        {
            temp.DelFlag = this.delFlagNormal;
            temp.SubTime=DateTime.Now;

            WF_TempService.Add(temp);
            return Content("ok");
        }

        #endregion

        #region 发起流程

        public ActionResult WFStart()
        {
            ViewData.Model = WF_TempService.GetEntities(u => true).ToList();
            return this.View();
        }

        #endregion

    }
}