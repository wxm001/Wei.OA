namespace Wei.OA.BLL
{
    using System.Collections.Generic;
    using System.Linq;

    using Wei.OA.IBLL;
    using Wei.OA.Model;

    public partial class ActionInfoService:BaseService<ActionInfo>,IActionInfoService
    {
        //权限和角色关联
        public bool SetRole(int actionId, List<int> roleIds)
        { 
            //找到权限
            var actionInfo = DbSession.ActionInfoDal.GetEntities(u => u.Id == actionId).FirstOrDefault();
            actionInfo.RoleInfo.Clear(); //把之前的关联都删了，换下面新的（省的判断）
            //找到所有角色
            var allRoles = DbSession.RoleInfoDal.GetEntities(r => roleIds.Contains(r.Id));
            foreach (var role in allRoles)
            {
                actionInfo.RoleInfo.Add(role); //加新角色 
            }

            DbSession.SaveChanges();
            return true;
            
        }
    }
}