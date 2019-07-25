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

    public class UserInfoController : Controller
    {
        // GET: UserInfo
        //IUserInfoService userInfoService=new UserInfoService();
        public UserInfoService UserInfoService { get; set; } //属性注入，在配置文件中赋值

        public ActionResult Index()
        {
            ViewData.Model = this.UserInfoService.GetEntities(u => true);
            return View();
        }

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
    }
}