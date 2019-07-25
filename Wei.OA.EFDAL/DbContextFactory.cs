/*============================================
*类名称：DbContextFactory
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
    using System.Data.Entity;
    using System.Runtime.Remoting.Messaging;

    using Wei.OA.Model;

    /// <summary>
    /// DbContextFactory
    /// </summary>
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            //一次请求共用一个实例，返回值为DbContext，实现了上下文都可以切换。
            //return new DataModelContainer();
            DbContext db=CallContext.GetData("DbContext") as DbContext;
            if (db==null)
            {
                db=new DataModelContainer();
                CallContext.SetData("DbContext",db);
            }

            return db;
        }
    }
}
