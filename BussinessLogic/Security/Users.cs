using EMRModel.Login;
using IBussinessLogic.Secutity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Login
{
    public class Users : IUsers
    {
        public List<UserModel> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public bool SaveUsers(UserModel userModel)
        {
            DataLayer.Security.Users userobj = new DataLayer.Security.Users();
            int userid = userobj.SaveUser(userModel);
            int EntityId = userobj.SaveEntityDetail(userModel);
            return userobj.SaveUserEntityMapping(userid, EntityId);

        }
        public UserModel GetUserDetails(string logiName, string password)
        {
            DataLayer.Security.Users userobj = new DataLayer.Security.Users();
            return userobj.GetUserDetails(logiName, password);
        }

        public bool IsloginNameExist(string loginName)
        {
            DataLayer.Security.Users userobj = new DataLayer.Security.Users();
            return userobj.IsLoginNameExists(loginName);
        }

        public void resetPassword(ResetPasswordViewModel reg)
        {
            DataLayer.Security.Users userobj = new DataLayer.Security.Users();
            userobj.ResetPassword(reg);
        }
    }
}
