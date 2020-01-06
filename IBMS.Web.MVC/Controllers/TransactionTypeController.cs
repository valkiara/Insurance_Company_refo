using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class TransactionTypeController : Controller
    {
        [CustomAuthorize(menu = 19)]
        public ActionResult Index()
        {
            return View();
        }
    }
}