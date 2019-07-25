using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.BLL
{
    using Wei.OA.DALFactory;
    using Wei.OA.EFDAL;
    using Wei.OA.IBLL;
    using Wei.OA.IDAL;
    using Wei.OA.Model;

    public partial class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {
        #region old
        //不仅仅是crud
        //IUserInfoDal userInfoDal=new UserInfoDal();
        //private IUserInfoDal userInfoDal = StaticDalFactory.GetUserInfoDal();
        //DbSession dbSession=new DbSession();
        //IDbSession dbSession=new DbSession();
        //private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();
        //public UserInfo Add(UserInfo userInfo)
        //{
        //    //return this.userInfoDal.Add(userInfo);
        //    this.dbSession.UserInfoDal.Add(userInfo);
        //    this.dbSession.SaveChanges();

        //} 
        #endregion

        //public UserInfoService(IDbSession dbSession):base(dbSession)  //构造函数注入
        //{

        //}

        #region 由模板自动生成
        //public override void SetCurrentDal()
        //{
        //    CurrentDal = DbSession.UserInfoDal;
        //} 
        #endregion
    }
}
