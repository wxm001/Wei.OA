using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wei.OA.IBLL
{
    using Wei.OA.Model;

    public partial interface IUserInfoService:IBaseService<UserInfo>
    {
        IQueryable<UserInfo> LoadPageData(Model.Param.UserQueryParam userQueryParam);

        bool SetRole(int userId, List<int> roleIds);

        List<string> GetRUserAction(UserInfo user);
    }
}
