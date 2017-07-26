using BussinessLogic;
using EMRModel;
using EMRModel.Login;
using IBussinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMR.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult _Menu()
        {
            if (Session["UserDetails"] != null)
            {
                IMenu m = new Menu();
                UserModel userobj = (UserModel)Session["UserDetails"];
                List<MenuObj> obj = m.GetMenu(userobj.UserId);

                return View(obj);
            }
            else
            {
                return RedirectToAction("index", "Login");
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("index", "Login");
        }
    }
}