using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using Wei.OA.Common;
    using Wei.OA.IBLL;
    using Wei.OA.UI.Portal.Models;

    [LoginCheckFilter(IsCheck = false)]
    public class UserLoginController : BaseController
    {
        // GET: UserLogin
        public UserLoginController()
        {
            this.IsCheckUserLogin = false;
        }
        public IUserInfoService UserInfoService { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        #region 验证码生成
        public ActionResult ShowVCode()
        {
            Common.ValidateCode validateCode = new ValidateCode();
            string strCode = validateCode.CreateValidateCode(4);

            //验证码放到Session
            Session["VCode"] = strCode;

            byte[] imgBytes = validateCode.CreateValidateGraphicMVC(strCode);

            return File(imgBytes, @"image/jpeg");
        }
        #endregion

        #region 处理登录的表单

        public ActionResult ProcessLogin()
        {
            //1、处理验证码：
            string strCode = Request["vCode"]; //拿到表单中的验证码
            string sessionCode = Session["VCode"] as string; //拿到session中的验证码
            Session["VCode"] = null; //只要取过一次立马设为空

            if (string.IsNullOrEmpty(sessionCode)||strCode!=sessionCode)
            {
                return Content("验证码错误");
            }

            //2、验证用户名密码
            string name = Request["LoginCode"];
            string pwd = Request["LoginPwd"];
            short delNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;

            var userInfo=UserInfoService.GetEntities(u => u.UName == name && u.Pwd == pwd && u.DelFlag == delNormal).FirstOrDefault();
            if (userInfo==null)
            {
                return Content("用户名或密码错误");
            }

            Session["loginUser"] = userInfo;

            //3、正确则跳转到首页
            return Content("ok");
        }

        #endregion
    }
}