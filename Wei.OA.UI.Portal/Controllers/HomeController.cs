using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //if (Session["loginUser"] ==null)
            //{
            //    return RedirectToAction("Index", "UserLogin");
            //}

            return View();
        }

    }
}