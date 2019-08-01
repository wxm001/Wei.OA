namespace Wei.OA.IBLL
{
    using System.Collections.Generic;

    using Wei.OA.Model;

    public partial interface IActionInfoService:IBaseService<ActionInfo>
    {
        bool SetRole(int actionId, List<int> roleIds);
    }
}