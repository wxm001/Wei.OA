using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Wei.OA.UnitTest
{
    using System.Linq;

    using Wei.OA.EFDAL;
    using Wei.OA.Model;

    [TestClass]
    public class UserInfoDalTest
    {
        [TestMethod]
        public void TestGetUsers()
        {
            //测试  获取数据的方法
            UserInfoDal dal=new UserInfoDal();

            //单元测试必须自己处理数据，不能依赖第三方数据。若依赖数据，那么先自己创建数据，然后用完之后再清除数据
            //创建测试的数据
            for (int i = 0; i < 10; i++)
            {
                dal.Add(new UserInfo() { UName = i + "wei" });
            }

            IQueryable<UserInfo> temp=dal.GetEntities(u=>u.UName.Contains("w"));

            //断言
            Assert.AreEqual(true,temp.Count()>=10);


        }
    }
}
