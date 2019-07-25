/*============================================
*类名称：OrderInfoDal
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.EFDAL
{
    using Wei.OA.IDAL;
    using Wei.OA.Model;

    /// <summary>
    /// OrderInfoDal
    /// </summary>
    public partial class OrderInfoDal:BaseDal<OrderInfo>,IOrderInfoDal
    {
    }
}
