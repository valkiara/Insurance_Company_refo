using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class PolicyCategoryController : Controller
    {
        [CustomAuthorize(menu = 18)]
        public ActionResult Index()
        {
            return View();
        }
    }
}