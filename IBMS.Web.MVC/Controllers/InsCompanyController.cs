using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class InsCompanyController : Controller
    {
        [CustomAuthorize(menu = 8)]
        public ActionResult Index()
        {
            return View();
        }
    }
}