
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
	public partial class OrderInfoService : BaseService<OrderInfo>,IOrderInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.OrderInfoDal;
        } 
	}
	public partial class UserInfoService : BaseService<UserInfo>,IUserInfoService
    {
	    public override void SetCurrentDal()
        {
            CurrentDal = DbSession.UserInfoDal;
        } 
	}
}