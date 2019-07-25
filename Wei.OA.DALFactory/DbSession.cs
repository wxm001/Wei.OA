/*============================================
*类名称：DbSession
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.DALFactory
{
    using Wei.OA.EFDAL;
    using Wei.OA.IDAL;

    /// <summary>
    /// DbSession
    /// </summary>
    public partial class DbSession:IDbSession
    {
        //简单工厂或抽象工厂的封装，相当于数据库访问层的入口，不用再调用工厂,模板自动生成
        //public IUserInfoDal UserInfoDal => StaticDalFactory.GetUserInfoDal();

        //public IOrderInfoDal OrderInfoDal => StaticDalFactory.GetOrderInfoDal();

        /// <summary>
        /// 拿到当前EF的上下文，然后把修改的实体进行整体提交,把数据提交的权利从数据库访问层提到了业务逻辑层
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }

    }
}
