/*============================================
*类名称：BaseParam
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.Model.Param
{
    /// <summary>
    /// BaseParam
    /// </summary>
    public class BaseParam
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int Total { get; set; }
    }
}
