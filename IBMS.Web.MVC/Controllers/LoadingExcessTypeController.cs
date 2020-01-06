using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class LoadingExcessTypeController : Controller
    {
        [CustomAuthorize(menu = 27)]
        public ActionResult Index()
        {
            return View();
        }
    }
}