
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.DALFactory
{
    using System.Reflection;

    using Wei.OA.EFDAL;
    using Wei.OA.IDAL;

    public partial class StaticDalFactory
    {
		public static IActionInfoDal GetActionInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".ActionInfoDal") as IActionInfoDal;
        }
		public static IOrderInfoDal GetOrderInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".OrderInfoDal") as IOrderInfoDal;
        }
		public static IUserInfoDal GetUserInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal") as IUserInfoDal;
        }
	}
}