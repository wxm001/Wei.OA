
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
		public static IFileInfoDal GetFileInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".FileInfoDal") as IFileInfoDal;
        }
		public static IOrderInfoDal GetOrderInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".OrderInfoDal") as IOrderInfoDal;
        }
		public static IR_UserInfo_ActionInfoDal GetR_UserInfo_ActionInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".R_UserInfo_ActionInfoDal") as IR_UserInfo_ActionInfoDal;
        }
		public static IRoleInfoDal GetRoleInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".RoleInfoDal") as IRoleInfoDal;
        }
		public static IUserInfoDal GetUserInfoDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal") as IUserInfoDal;
        }
		public static IUserInfoExtDal GetUserInfoExtDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoExtDal") as IUserInfoExtDal;
        }
		public static IWF_InstanceDal GetWF_InstanceDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".WF_InstanceDal") as IWF_InstanceDal;
        }
		public static IWF_StepDal GetWF_StepDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".WF_StepDal") as IWF_StepDal;
        }
		public static IWF_TempDal GetWF_TempDal()
        {                   
            return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".WF_TempDal") as IWF_TempDal;
        }
	}
}