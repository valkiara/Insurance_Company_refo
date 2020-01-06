using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class UserController : Controller
    {
        [CustomAuthorize(menu = 42)]
        public ActionResult Profile()
        {
            return View();
        }
    }
}