using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(menu = 1)]
        public ActionResult Dashboard()
        {
            return View();
        }

    }
}