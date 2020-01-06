using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class InternalPolicyNumberSetupController : Controller
    {
        [CustomAuthorize(menu = 22)]
        public ActionResult Index()
        {
            return View();
        }
    }
}