using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;


namespace IBMS.Web.MVC.Controllers
{
    [CustomAuthorize(menu = 5)]
    public class CommissionHeaderController : Controller
    {
        // GET: CommissionHeader
        public ActionResult Index()
        {
            return View();
        }
    }
}