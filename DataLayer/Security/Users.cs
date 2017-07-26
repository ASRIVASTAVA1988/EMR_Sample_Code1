using DataLayer.DS;
using EMRModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Security
{
    public class Users
    {
        public bool IsLoginNameExists(string loginName)
        {

            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                var data = db.users.Where(a => a.LoginName == loginName).FirstOrDefault();
                return data == null ? false : true;
            }

        }
        public int SaveEntityDetail(UserModel reg)
        {
            int EntityId = 0;

            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                Entity entity = new Entity();
                entity.FirstName = reg.FirstName;
                entity.LastName = reg.LastName;
                db.Entities.Add(entity);
                db.SaveChanges();
                EntityId = entity.EntityId;
            }
            return EntityId;
        }
        public int SaveUser(UserModel reg)
        {
            int UserId = 0;
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                user user = new user();
                user.FirstName = reg.FirstName;
                user.LastName = reg.LastName;
                user.LoginName = reg.LoginName;
                user.Password = reg.Password;
                // user.Password = Utility.PasswordHashing.ComputeHash(reg.Password,null,null);
                user.LastPasswordResetDate = DateTime.Now;
                db.users.Add(user);
                db.SaveChanges();
                UserId = user.UserId;
            }
            return UserId;
        }
        public bool SaveUserEntityMapping(int UserId, int EntityId)
        {
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                UserEntityMapping user = new UserEntityMapping();
                user.EntityId = EntityId;
                user.UserId = UserId;
                user.EntityType = "";
                db.UserEntityMappings.Add(user);
                db.SaveChanges();
            }
            return true;
        }
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> userlst = new List<UserModel>();
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                userlst = (from uslst in db.users
                           select new UserModel
                           {
                               FirstName = uslst.FirstName,
                               LastName = uslst.LastName,
                               IsActive = uslst.IsActive,
                               UserId = uslst.UserId

                           }).ToList();
            }
            return userlst;
        }
        public UserModel GetUserDetails(string loginName, string password)
        {
            UserModel userModel = new UserModel();
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                var user = db.users.Where(m => m.LoginName == loginName).Select(m => m).FirstOrDefault();
                if (user != null)
                {
                    if (user.LastLoginAttemptDate == null)
                    {
                        user.LastLoginAttemptDate = System.DateTime.Now;
                    }
                    System.TimeSpan logintimeDifference = System.DateTime.Now.Subtract(user.LastLoginAttemptDate.Value);
                    System.TimeSpan loginPasswordsetDifference = System.DateTime.Now.Subtract(user.LastPasswordResetDate.Value);

                    if (user.FailedPasswordCount == null || user.FailedPasswordCount < 3 || (user.IsLocked == "Y" && logintimeDifference.TotalSeconds > 15))
                    {
                        if (user.LastLoginAttemptDate == null || (loginPasswordsetDifference.TotalDays < 15))
                        {
                            userModel = (from usr in db.users
                                         join userrole in db.UserRoles on usr.UserId equals userrole.UserId
                                         join r in db.Roles on userrole.RoleId equals r.RoleId
                                         where usr.LoginName == loginName && usr.Password == password
                                         select new UserModel
                                         {
                                             LastName = usr.LastName,
                                             FirstName = usr.FirstName,
                                             IsLocked = usr.IsLocked,
                                             IsActive = usr.IsActive,
                                             UserId = usr.UserId,
                                             status = LoginEnum.Pass
                                         }).FirstOrDefault();

                            if (userModel == null)
                            {
                                userModel = new UserModel();
                                userModel.status = LoginEnum.UserPasswordWrong;
                                int count = (user.FailedPasswordCount == null) ? 0 : (user.FailedPasswordCount.Value);
                                user.FailedPasswordCount = (count + 1);
                                db.SaveChanges();
                            }
                            else
                            {
                                user.IsLocked = "N";
                                user.FailedPasswordCount = 0;
                                db.SaveChanges();
                            }
                        }
                        else
                        {
                            userModel.status = LoginEnum.FailByTime;
                            db.SaveChanges();
                        }
                    }
                    else
                    {
                        userModel.status = LoginEnum.FailByCount;
                        user.IsLocked = "Y";
                        db.SaveChanges();
                    }


                    user.LastLoginAttemptDate = System.DateTime.Now;
                    db.SaveChanges();
                }
                else
                {
                    userModel = null;
                }
            }
            return userModel;
        }

        public void ResetPassword(ResetPasswordViewModel reg)
        {
            
            using (DS.EMRDBEntities db = new DS.EMRDBEntities())
            {
                var user = db.users.Where(m => m.LoginName == reg.LoginName).Select(m => m).FirstOrDefault();
                if (user != null)
                {
                    user.Password = reg.Password;
                    user.LastPasswordResetDate = System.DateTime.Now;
                    db.SaveChanges();


                }
            }

        }
    }
}
