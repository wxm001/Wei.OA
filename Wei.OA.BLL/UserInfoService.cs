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
    using Wei.OA.Model.Param;

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


        //多条件查询
        public IQueryable<UserInfo> LoadPageData(UserQueryParam userQueryParam)
        {
            short normalFlag = (short)Wei.OA.Model.Enum.DelFlagEnum.Normal;
            var temp = DbSession.UserInfoDal.GetEntities(u => u.DelFlag == normalFlag);
            //过滤
            if (!string.IsNullOrEmpty(userQueryParam.SchName))
            {
                temp = temp.Where(u => u.UName.Contains(userQueryParam.SchName)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(userQueryParam.SchRemark))
            {
                temp = temp.Where(u => u.Remark.Contains(userQueryParam.SchRemark)).AsQueryable();
            }

            userQueryParam.Total = temp.Count();
            //分页
            return temp.OrderBy(u => u.Id).Skip(userQueryParam.PageSize * (userQueryParam.PageIndex - 1))
                .Take(userQueryParam.PageSize).AsQueryable();

        }
    }
}
