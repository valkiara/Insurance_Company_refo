using IBMS.Web.MVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBMS.Web.MVC.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize(menu = 82)]
        public ActionResult NewClientList()
        {
            return View();
        }
        [CustomAuthorize(menu = 83)]
        public ActionResult GetCancelledClient()
        {
            return View();
        }
    }
}