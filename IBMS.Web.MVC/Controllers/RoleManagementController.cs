using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class RoleManagementController : Controller
    {
        // GET: RoleManagement
        [CustomAuthorize (menu = 44)]
        public ActionResult RoleManagement()
        {
            return View();
        }
    }
}