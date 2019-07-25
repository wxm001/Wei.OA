using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    using Spring.Context;
    using Spring.Context.Support;

    class Program
    {
        static void Main(string[] args)
        {
            //传统的
            //IUserInfoDal userInfoDal=new UserInfoDal();
            //userInfoDal.Show();

            //做容器创建实例
            //初始化容器
            IApplicationContext context = ContextRegistry.GetContext();
            IUserInfoDal dal = context.GetObject("UserInfoDal") as IUserInfoDal;
            dal.Show();

            Console.ReadKey();
        }
    }
}
