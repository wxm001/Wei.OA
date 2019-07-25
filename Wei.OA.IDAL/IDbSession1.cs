
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.IDAL
{
	public partial interface IDbSession
	{
		IActionInfoDal ActionInfoDal { get; }
		IOrderInfoDal OrderInfoDal { get; }
		IUserInfoDal UserInfoDal { get; }
	}
}