using IBMS.Web.MVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace IBMS.Web.MVC.Controllers
{
    public class RenewalListController : Controller
    {
        // GET: ClientRenewal
        [CustomAuthorize(menu = 80)]
        public ActionResult Index()
        {
            return View();
        }
    }
}