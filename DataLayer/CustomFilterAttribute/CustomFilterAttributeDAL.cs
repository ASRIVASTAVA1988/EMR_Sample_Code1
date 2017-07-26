using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.CustomFilterAttribute
{
    class CustomFilterAttributeDAL
    {
        public static bool IsActionAuthorization(int userId, string actionname)
        {
            bool result = false;
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                var test = "a";
                //var test = (from us in db.users
                //            join usrole in db.UserRoles on us.UserId equals usrole.UserId
                //            join roleaction in db.Ac on usrole.RoleId equals roleaction.RoleId
                //            join action in abhi.Actions on roleaction.ActionId equals action.ActionId
                //            where us.UserId == userId && action.ActionName.ToLower() == actionname.ToLower()
                //            select action).ToList();

                if (test.Count() > 0)
                {
                    //var fielddetails = (from action in abhi.Actions
                    //                    join actionfield in abhi.ActionFields on action.ActionId equals actionfield.ActionId
                    //                    where action.ActionName.ToLower() == actionname.ToLower()
                    //                    select actionfield).ToList();
                    result = true;
                }

            }
            return result;
        }
        //public static List<DS.> fieldDetails(string actionname)
        //{
        //    using (DS.EMRDBEntities db = new DS.EMRDBEntities())
        //    {
        //        var fielddetails = (from action in abhi.Actions
        //                            join actionfield in abhi.ActionFields on action.ActionId equals actionfield.ActionId
        //                            where action.ActionName.ToLower() == actionname.ToLower()
        //                            select actionfield).ToList();

        //        return fielddetails;
        //    }
        //}
    }
}
