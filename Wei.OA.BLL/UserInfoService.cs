﻿using System;
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
        
        //用户和角色关联
        public bool SetRole(int userId, List<int> roleIds)
        {
            //找到用户
            var user=DbSession.UserInfoDal.GetEntities(u => u.Id == userId).FirstOrDefault();
            user.RoleInfo.Clear(); //把之前的关联都删了，换下面新的（省的判断）
            //找到所有角色
            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));
            foreach (var role in allRoles)
            {
                user.RoleInfo.Add(role); //加新角色 
            }

            DbSession.SaveChanges();
            return true;
        }
        //找到用户权限中间表中的权限id和状态
        public List<string> GetRUserAction(UserInfo user)
        {
            List<string> res=new List<string>();
            List<int> actList = (from r in user.R_UserInfo_ActionInfo where r.DelFlag==0 select r.ActionInfoId).ToList();
            List<bool> actState = (from r in user.R_UserInfo_ActionInfo where r.DelFlag == 0 select r.HasPermission).ToList();
            if (actList!=null)
            {
                for (int i = 0; i < actList.Count; i++)
                {
                    string str = actState[i] ? "1" : "0";
                    str = str + "," + actList[i];
                    res.Add(str);
                }
            }

            return res;
        }
    }
}
