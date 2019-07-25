/*============================================
*类名称：IBaseDal
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
    using System.Linq.Expressions;

    /// <summary>
    /// IBaseDal
    /// </summary>
    public interface IBaseDal<T> where T:class ,new ()
    {
        #region 查询

        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda); //用Expression包裹可以实现延迟加载，不用其包裹会直接查询放到内存
        //通用分页
        IQueryable<T> GetPageEntities<S>(
            int pageSize,
            int pageIndex,
            out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc);


        #endregion

        #region cud

        T Add(T entity);


        bool Update(T entity);


        bool Delete(T entity);


        #endregion
    }
}
