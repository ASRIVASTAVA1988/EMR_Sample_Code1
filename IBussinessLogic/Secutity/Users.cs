using EMRModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBussinessLogic.Secutity
{
    public interface IUsers
    {
        bool IsloginNameExist(string loginName);
        bool SaveUsers(UserModel userModel);
        List<UserModel> GetAllUser();
        UserModel GetUserDetails(string loginName,string password);
        void resetPassword(ResetPasswordViewModel reg);
    }
}
