/*============================================
*类名称：UserInfoDal
*类描述：
*创建人：Administrator
*=============================================*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpringNetDemo
{
    /// <summary>
    /// UserInfoDal
    /// </summary>
    public class UserInfoDal:IUserInfoDal
    {
        public string Name { get; set; }
        public UserInfoDal(string name)
        {
            Name = name;
        }
        public void Show()
        {
            Console.WriteLine("123"+Name);
        }
    }
}
