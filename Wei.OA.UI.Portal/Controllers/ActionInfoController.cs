using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.IBLL;

    public class ActionInfoController : BaseController
    {
        public IActionInfoService ActionInfoService { get; set; }

        // GET: ActionInfo
        public ActionResult Index()
        {
            try
            {
                throw new Exception("error!!!");
            }
            catch (Exception e)
            {
                Common.LogHelper.WriteLog(e.ToString());
            }            
            ViewData.Model = ActionInfoService.GetEntities(u => true).ToList();
            return View();
        }
    }
}