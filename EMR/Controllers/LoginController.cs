using EMRModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMR.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Index(UserModel userModel)
        {
            IBussinessLogic.Secutity.IUsers users = new BussinessLogic.Login.Users();
            UserModel userobj = users.GetUserDetails(userModel.LoginName, userModel.Password);
            if (userobj != null && userobj.status==LoginEnum.Pass)
            {
                Session["UserDetails"] = userobj;
                return RedirectToAction("index", "home");
            }
            else if(userobj==null)
            {
                ViewBag.Message = "Invalid login Credentials";
            }
            if(userobj.status == LoginEnum.UserPasswordWrong)
            {
                ViewBag.Message = "Username and Password Invalid";
            }
            if (userobj.status == LoginEnum.FailByCount)
            {
                ViewBag.Message = "Your account has been locked out. Please try after 15 sec";
            }
            if (userobj.status == LoginEnum.FailByTime)
            {
                ViewBag.Message = "Your password is expired. Plese reset you password and try ";
                return RedirectToAction("ResetPassword", "Login");

            }
            return View();
        }
        [HttpGet]
        public ActionResult Registrtaion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrtaion(UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                bool loginNameExits = IsLoginNameExits(userModel.LoginName);
                if (loginNameExits == true)
                {
                    ModelState.AddModelError("LoginNameExits", "This login name is already registerd with our system. Please use another login name.");
                    return View(userModel);
                }
                else
                {
                    IBussinessLogic.Secutity.IUsers users = new BussinessLogic.Login.Users();
                    users.SaveUsers(userModel);
                    return RedirectToAction("index", "login");
                }
            }
            return View();
        }
        [NonAction]
        public bool IsLoginNameExits(string loginname)
        {
            IBussinessLogic.Secutity.IUsers users = new BussinessLogic.Login.Users();
            return users.IsloginNameExist(loginname);
        }
        [HttpGet]
        public ActionResult Forgetpassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgetpassword(UserModel userModel)
        {
            return View();
        }
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            bool user = IsLoginNameExits(model.LoginName);
            if (user == false)
            {
                // Don't reveal that the user does not exist
                //return RedirectToAction("ResetPasswordConfirmation", "Account");
                AddErrors("Provided login name does not exists.");
            }
            else
            {
                IBussinessLogic.Secutity.IUsers users = new BussinessLogic.Login.Users();
                users.resetPassword(model);
                return RedirectToAction("ResetPasswordConfirmation", "Login");
            }
            //var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            //if (result.Succeeded)
            //{
            //    return RedirectToAction("ResetPasswordConfirmation", "Account");
            //}
           // AddErrors(result);
            return View();
            
        }
        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
        private void AddErrors(string error)
        {
            //foreach (var error in result.Errors)
            //{
            //    ModelState.AddModelError("", error);
            //}
            ModelState.AddModelError("", error);
        }

    }
}