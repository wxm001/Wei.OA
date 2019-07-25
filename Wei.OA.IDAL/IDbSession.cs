/*============================================
*类名称：IDbSession
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.IDAL
{
    /// <summary>
    /// IDbSession
    /// </summary>
    public partial interface IDbSession
    {
        #region 改成了模板自动生成
        //IUserInfoDal UserInfoDal { get; }

        //IOrderInfoDal OrderInfoDal { get; } 
        #endregion

        int SaveChanges();
    }
}
