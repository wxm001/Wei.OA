/*============================================
*类名称：BaseDal
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
    using System.Linq.Expressions;
    using Wei.OA.Model;

    /// <summary>
    /// BaseDal:封装所有dal的公共的crud
    /// </summary>
    public class BaseDal<T> where T:class ,new ()
    {
        //DataModelContainer Db = new DataModelContainer();//保证一次请求共用一个上下文实例，不能每次new一个出来
        //因为这里都是用的Db上下文中基类的方法（没有Db.UserInfo这种），所以可以将DataModelContainer换为它的基类DbContext
        //public DataModelContainer Db
        //依赖抽象编程。可以应对变化的时候，改变最小。
        public DbContext Db => DbContextFactory.GetCurrentDbContext();

        #region 查询
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)//用Expression包裹可以实现延迟加载，不用其包裹会直接查询放到内存
        {
            return Db.Set<T>().Where(whereLambda).AsQueryable();
        }

        //通用分页
        public IQueryable<T> GetPageEntities<S>(
            int pageSize,
            int pageIndex,
            out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            total = this.Db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = this.Db.Set<T>().Where(whereLambda).OrderBy<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();

                return temp;
            }
            else
            {
                var temp = this.Db.Set<T>().Where(whereLambda).OrderByDescending<T, S>(orderByLambda)
                    .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();

                return temp;
            }

        }

        #endregion

        #region cud
        public T Add(T entity)
        {
            Db.Set<T>().Add(entity);
            //Db.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            this.Db.Entry(entity).State = EntityState.Modified;
            // return this.Db.SaveChanges() > 0;
            return true;

        }

        public bool Delete(T entity)
        {
            //this.Db.Entry(entity).State = EntityState.Deleted;
            //逻辑删除
            Db.Entry(entity).Property("DelFlag").CurrentValue = (short)Wei.OA.Model.Enum.DelFlagEnum.Deleted;
            Db.Entry(entity).Property("DelFlag").IsModified = true;
            //return this.Db.SaveChanges() > 0;
            return true;
        }

        public bool Delete(int id)
        {
            var entity=Db.Set<T>().Find(id);
            //Db.Set<T>().Remove(entity);
            //逻辑删除
            Db.Entry(entity).Property("DelFlag").CurrentValue = (short)Wei.OA.Model.Enum.DelFlagEnum.Deleted;
            Db.Entry(entity).Property("DelFlag").IsModified = true;
            return true;
        }

        //逻辑删除
        public int DeleteListByLogical(List<int> strIds)
        {
            foreach (var strId in strIds)
            {
                var entity = Db.Set<T>().Find(strId);
                Db.Entry(entity).Property("DelFlag").CurrentValue = (short)Wei.OA.Model.Enum.DelFlagEnum.Deleted;
                Db.Entry(entity).Property("DelFlag").IsModified = true;
            }

            return strIds.Count;
        }

        #endregion

    }
}
