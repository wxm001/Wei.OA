
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.IDAL
{
    using Wei.OA.Model;

	public partial interface IActionInfoDal : IBaseDal<ActionInfo>
	{

	}
	public partial interface IOrderInfoDal : IBaseDal<OrderInfo>
	{

	}
	public partial interface IR_UserInfo_ActionInfoDal : IBaseDal<R_UserInfo_ActionInfo>
	{

	}
	public partial interface IRoleInfoDal : IBaseDal<RoleInfo>
	{

	}
	public partial interface IUserInfoDal : IBaseDal<UserInfo>
	{

	}
	public partial interface IUserInfoExtDal : IBaseDal<UserInfoExt>
	{

	}
}