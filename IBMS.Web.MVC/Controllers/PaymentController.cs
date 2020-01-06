using IBMS.Web.MVC.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBMS.Web.MVC.Controllers
{
    public class PaymentController : Controller
    {
        [CustomAuthorize(menu = 34)]
        public ActionResult DebitNote()
        {
            return View();
        }
    }
}