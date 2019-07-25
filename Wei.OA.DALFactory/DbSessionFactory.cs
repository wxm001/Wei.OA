/*============================================
*类名称：DbSessionFactory
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
    using System.Runtime.Remoting.Messaging;

    using Wei.OA.IDAL;

    /// <summary>
    /// DbSessionFactory
    /// </summary>
    public class DbSessionFactory
    {
        //实现一次请求共用一个dbSession
        public static IDbSession GetCurrentDbSession()
        {
            IDbSession db = CallContext.GetData("DbSession") as DbSession;
            if (db==null)
            {
                db=new DbSession();
                CallContext.SetData("DbSession",db);
            }

            return db;
        }
    }
}
