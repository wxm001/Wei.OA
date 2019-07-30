/*============================================
*类名称：BaseService
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
    using System.Linq.Expressions;

    using Wei.OA.DALFactory;
    using Wei.OA.IDAL;
    using Wei.OA.Model;

    /// <summary>
    /// BaseService
    /// </summary>
    public abstract class BaseService<T> where T:class ,new ()
    {
        //父类要逼迫子类给父类的一个属性赋值，赋值操作必须在父类的方法调用之前先执行
        //public BaseService(IDbSession dbSession)//基类的构造函数 或者public BaseService(IBaseDal<T> currentDal)
        //{
        //    DbSession = dbSession;
        //    this.SetCurrentDal(); //抽象方法
        //}
        
        public IBaseDal<T> CurrentDal { get; set; }

        //public IDbSession DbSession => DbSessionFactory.GetCurrentDbSession();

        public IDbSession DbSession { get; set; } // 为dbsession实现注入

        public abstract void SetCurrentDal(); //抽象方法：要求子类必须实现

        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);
        }

        public IQueryable<T> GetPageEntities<S>(
            int pageSize,
            int pageIndex,
            out int total,
            Expression<Func<T, bool>> whereLambda,
            Expression<Func<T, S>> orderByLambda,
            bool isAsc)
        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total, whereLambda, orderByLambda,isAsc);
        }

        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            DbSession.SaveChanges();
            return entity;
        }

        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return DbSession.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return DbSession.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            CurrentDal.Delete(id);
            return DbSession.SaveChanges() > 0;
        }

        //批量删除
        public int DeleteList(List<int> strIds)
        {
            foreach (var id in strIds)
            {
                CurrentDal.Delete(id);
            }

            return DbSession.SaveChanges();
        }

        //逻辑删除
        public int DeleteListByLogical(List<int> strIds)
        {
            CurrentDal.DeleteListByLogical(strIds);
            return DbSession.SaveChanges();
        }
    }
}
