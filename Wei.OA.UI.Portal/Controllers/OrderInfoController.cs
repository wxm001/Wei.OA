using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.IBLL;

    public class OrderInfoController : Controller
    {
        public IOrderInfoService OrderInfoService { get; set; }//属性注入

        // GET: OrderInfo
        public ActionResult Index()
        {
            ViewData.Model=OrderInfoService.GetEntities(u => true).ToList();
            return View();
        }
    }
}