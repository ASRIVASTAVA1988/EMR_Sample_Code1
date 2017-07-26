using EMRModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Menu
    {
        public List<MenuObj> GetMenufromUserId(int userid)
        {
            List<MenuObj> userlst = new List<MenuObj>();
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                userlst = (

                    from user in db.users
                    join userrole in db.UserRoles on user.UserId equals userrole.UserId
                    join r in db.Roles on userrole.RoleId equals r.RoleId
                    join acr in db.ActionControllerRoles on r.RoleId equals acr.RoleId
                    join ac in db.ActionControllers on acr.ActionControllerId equals ac.ActionControllerId
                    join m in db.Menus on ac.ActionControllerId equals m.ActionControllerId
                    where user.UserId == userid
                    select new MenuObj
                    {
                        MenuName = m.MenuName,
                        MenuUrl = ac.ActionControllerName,
                        ActionControllerCode = ac.ActionControllerCode,
                        ParentMenuId=m.ParentMenuId

                    }
                    ).ToList();
            }
            return userlst;
        }
    }
}
