using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.EFDAL
{
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Wei.OA.IDAL;
    using Wei.OA.Model;

    public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {
        //crud
        //DataModelContainer db = new DataModelContainer();
        #region 查询    
        
        //public UserInfo GetUserInfoById(int id)
        //{            
        //    return db.UserInfo.Find(id);
        //}

        //public List<UserInfo> GetAllUserInfosd()
        //{
        //    return db.UserInfo.ToList();
        //}
        //通用查询
        //public IQueryable<UserInfo> GetUsers(Expression<Func<UserInfo, bool>> whereLambda)//用Expression包裹可以实现延迟加载，不用其包裹会直接查询放到内存
        //{
        //    return db.UserInfo.Where(whereLambda).AsQueryable();
        //}

        //通用分页
        //public IQueryable<UserInfo> GetPageUsers<S>(
        //    int pageSize,
        //    int pageIndex,
        //    out int total,
        //    Expression<Func<UserInfo, bool>> whereLambda,
        //    Expression<Func<UserInfo,S>> orderByLambda,
        //    bool isAsc)
        //{
        //    total = this.db.UserInfo.Where(whereLambda).Count();
        //    if (isAsc)
        //    {
        //        var temp = this.db.UserInfo.Where(whereLambda).OrderBy<UserInfo, S>(orderByLambda)
        //            .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();

        //        return temp;
        //    }
        //    else
        //    {
        //        var temp = this.db.UserInfo.Where(whereLambda).OrderByDescending<UserInfo, S>(orderByLambda)
        //            .Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();

        //        return temp;
        //    }
            
        //}

        #endregion

        #region cud

        //public UserInfo Add(UserInfo userInfo)
        //{
        //    db.UserInfo.Add(userInfo);
        //    db.SaveChanges();
        //    return userInfo;
        //}

        //public bool Update(UserInfo userInfo)
        //{
        //    this.db.Entry(userInfo).State = EntityState.Modified;
        //    return this.db.SaveChanges() > 0;

        //}

        //public bool Delete(UserInfo userInfo)
        //{
        //    this.db.Entry(userInfo).State = EntityState.Deleted;
        //    return this.db.SaveChanges() > 0;
        //}

        #endregion
    }
}
