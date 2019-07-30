using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Wei.OA.UI.Portal.Controllers
{
    using System.Web.UI.WebControls;

    using Wei.OA.BLL;
    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public class UserInfoController : BaseController
    {
        // GET: UserInfo
        //IUserInfoService userInfoService=new UserInfoService();
        public UserInfoService UserInfoService { get; set; } //属性注入，在配置文件中赋值

        #region 获取用户
        public ActionResult Index()
        {
            ViewData.Model = this.UserInfoService.GetEntities(u => true);
            return View();
        }

        public ActionResult GetAllUserInfos()
        {
            //jquery easyui:table:{total:32,rows:[{},{}]}
            //easy ：table在初始化的时候自动发送一下两个参数值
            int pageSize = int.Parse(Request["rows"] ?? "10");
            int pageIndex = int.Parse(Request["page"] ?? "1");
            int total = 0;


            short delFlagNormal = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
            //拿到当前页的数据,带有导航属性时，使用select过滤一下
            var pageData = UserInfoService.GetPageEntities(
                pageSize,
                pageIndex,
                out total,
                u => u.DelFlag == delFlagNormal,
                u => u.Id,
                true).Select(u => new { u.Id, u.UName, u.Remark, u.ShowName, u.SubTime, u.ModfiliedOn, u.Pwd });
            var data = new { total = total, rows = pageData.ToList() };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加用户

        public ActionResult Add(UserInfo userInfo)
        {
            userInfo.ModfiliedOn=DateTime.Now;
            userInfo.SubTime=DateTime.Now;
            userInfo.DelFlag = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;

            UserInfoService.Add(userInfo);

            return Content("ok");
        }

        #endregion

        #region 删除用户

        public ActionResult Delete(string strId)
        {
            if (string.IsNullOrEmpty(strId))
            {
                return Content("请选择要删除的数据！");
            }
            //正常处理
            string[] strIds=strId.Split(',');
            List<int> idList=new List<int>();
            foreach (var str in strIds)
            {
                idList.Add(int.Parse(str));
            }

            UserInfoService.DeleteListByLogical(idList);
            return Content("ok");
        }

        #endregion

        #region Create
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
        #endregion
    }
}