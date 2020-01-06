using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class DesignationController : Controller
    {
        // GET: Designation
        [CustomAuthorize(menu = 13)]
        public ActionResult Index()
        {
            var Name = CurrentSession.LoggedUserPortal.LoginName;

            return View();
        }
    }
}