using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class PolicyController : Controller
    {
        [CustomAuthorize(menu = 17)]
        public ActionResult Index()
        {
            return View();
        }

        [CustomAuthorize(menu = 18)]
        public ActionResult PolicyCategory()
        {
            return View();
        }

        public ActionResult PolicyInfRecording()
        {
            return View();
        }

        public ActionResult PolicyRenewal()
        {
            return View();
        }
    }
}