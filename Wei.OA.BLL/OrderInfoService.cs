/*============================================
*类名称：OrderInfoService
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.BLL
{
    using Wei.OA.DALFactory;
    using Wei.OA.IBLL;
    using Wei.OA.IDAL;
    using Wei.OA.Model;

    /// <summary>
    /// OrderInfoService
    /// </summary>
    public partial class OrderInfoService : BaseService<OrderInfo>,IOrderInfoService
    {
        //public OrderInfoService(IDbSession dbSession):base(dbSession)
        //{

        //}
        #region 由模板自动生成
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = DbSession.OrderInfoDal;
        //} 
        #endregion
    }
}
