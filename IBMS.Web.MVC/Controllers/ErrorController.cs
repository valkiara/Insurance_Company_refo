using IBMS.Web.MVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBMS.Web.MVC.Controllers
{
    public class ErrorController : Controller
    {
        [CustomAuthorize(menu = 0)]
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}