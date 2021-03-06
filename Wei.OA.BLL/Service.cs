﻿
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

	public partial class ActionInfoService : BaseService<ActionInfo>,IActionInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.ActionInfoDal;
        } 
	}
	public partial class FileInfoService : BaseService<FileInfo>,IFileInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.FileInfoDal;
        } 
	}
	public partial class OrderInfoService : BaseService<OrderInfo>,IOrderInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.OrderInfoDal;
        } 
	}
	public partial class R_UserInfo_ActionInfoService : BaseService<R_UserInfo_ActionInfo>,IR_UserInfo_ActionInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.R_UserInfo_ActionInfoDal;
        } 
	}
	public partial class RoleInfoService : BaseService<RoleInfo>,IRoleInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.RoleInfoDal;
        } 
	}
	public partial class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        } 
	}
	public partial class UserInfoExtService : BaseService<UserInfoExt>,IUserInfoExtService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoExtDal;
        } 
	}
	public partial class WF_InstanceService : BaseService<WF_Instance>,IWF_InstanceService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.WF_InstanceDal;
        } 
	}
	public partial class WF_StepService : BaseService<WF_Step>,IWF_StepService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.WF_StepDal;
        } 
	}
	public partial class WF_TempService : BaseService<WF_Temp>,IWF_TempService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.WF_TempDal;
        } 
	}
}