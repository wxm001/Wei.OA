using System;
using System.Collections.Generic;
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
        #region 简单工厂

        //public static IUserInfoDal GetUserInfoDal()
        //{
        //    return new UserInfoDal();
        //}

        //public static IOrderInfoDal GetOrderInfoDal()
        //{
        //    return new OrderInfoDal();
        //}

        #endregion

        #region 抽象工厂
        public static string AssemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];
        #region 有模板自动生成
        //public static IUserInfoDal GetUserInfoDal()
        //{
        //    //把上面的new去掉：希望改一个配置那么创建实例就发生变化，不需要改代码           
        //    return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".UserInfoDal") as IUserInfoDal;
        //}

        //public static IOrderInfoDal GetOrderInfoDal()
        //{
        //    return Assembly.Load(AssemblyName).CreateInstance(AssemblyName + ".OrderInfoDal") as IOrderInfoDal;
        //} 
        #endregion
        #endregion

    }
}
