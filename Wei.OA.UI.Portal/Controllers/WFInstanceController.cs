using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Data.Entity.SqlServer;

    using Wei.OA.BLL;
    using Wei.OA.IBLL;

    public class WFInstanceController : Controller
    {
        short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
        public IUserInfoService UserInfoService { get; set; }
        public IWF_TempService WF_TempService { get; set; }
        // GET: WFInstance
        public ActionResult Index()
        {
            return View();
        }

        //发起流程 id:流程模板的id
        public ActionResult Add(int id)
        {
            var temp = WF_TempService.GetEntities(u => u.Id == id && u.DelFlag == this.delFlagNormal).FirstOrDefault();
            ViewBag.Temp = temp;
            var allUsers = UserInfoService.GetEntities(u => u.DelFlag == this.delFlagNormal).ToList();
            ViewData["UserList"] = (from u in allUsers
                                    select new SelectListItem()
                                    {
                                        Selected = false, Text = u.UName, Value = u.Id+""
                                    })
                                    .ToList();
            return this.View();
        }
    }
}