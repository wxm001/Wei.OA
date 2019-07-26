/*============================================
*类名称：MyExceptionFilterAttribute
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.Model
{
    using System.Web.Mvc;

    /// <summary>
    /// MyExceptionFilterAttribute
    /// </summary>
    public class MyExceptionFilterAttribute: HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //自己处理异常
            //自己把错误信息写到队列中（之后还要写入日志文件中）
            Common.LogHelper.WriteLog(filterContext.Exception.ToString());

        }
    }
}
