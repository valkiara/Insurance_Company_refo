using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class EnquiriesController : Controller
    {
        [CustomAuthorize(menu = 50)]
        public ActionResult AgentList()
        {
            return View();
        }

        [CustomAuthorize(menu = 51)]
        public ActionResult InsCompanyList()
        {
            return View();
        }

        [CustomAuthorize(menu = 37)]
        public ActionResult INSClasses()
        {
            return View();
        }

        [CustomAuthorize(menu = 41)]
        public ActionResult Employees()
        {
            return View();
        }

        [CustomAuthorize(menu = 38)]
        public ActionResult INSSubClasses()
        {
            return View();
        }

        [CustomAuthorize(menu = 40)]
        public ActionResult ClientList()
        {
            return View();
        }

        [CustomAuthorize(menu = 39)]
        public ActionResult CommissionPercentages()
        {
            return View();
        }
    }
}