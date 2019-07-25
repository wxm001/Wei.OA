
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

	public partial class ActionInfoDal:BaseDal<ActionInfo>,IActionInfoDal
    {

	}
	public partial class OrderInfoDal:BaseDal<OrderInfo>,IOrderInfoDal
    {

	}
	public partial class UserInfoDal:BaseDal<UserInfo>,IUserInfoDal
    {

	}
}