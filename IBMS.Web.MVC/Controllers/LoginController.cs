using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;
using IBMS.Web.MVC.Models;
using IBMS.Web.MVC.Security;


namespace IBMS.Web.MVC.Controllers
{
    public class LoginController : Controller
    {
        //LoginData loginData = null;
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CheckAuthentication()
        {
            return Json(CurrentSession.LoggedUserPortal);
        }

        public ActionResult ValidateLoginDetails(LoginData loginData)
        {
            try
            {
                CurrentSession.LoggedUserPortal = loginData;
                return Json(new { status = true, message = "Login Successful" });
            }
            catch (Exception EX)
            {
                return Json(new { status = false, message = "Unknown Error" });
            }
        }

        public ActionResult Logout()
        {
            CurrentSession.LoggedUserPortal = null;
            return RedirectToAction("Index", "Login");
        }
    }
}