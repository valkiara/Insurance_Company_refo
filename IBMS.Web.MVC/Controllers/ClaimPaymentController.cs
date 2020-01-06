using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class ClaimPaymentController : Controller
    {
        [CustomAuthorize(menu = 32)]
        public ActionResult ClaimRecording()
        {
            return View();
        }

        public ActionResult claimPaymentType()
        {
            return View();
        }

        [CustomAuthorize(menu = 24)]
        public ActionResult ClaimRejectionReason()
        {
            return View();
        }

        [CustomAuthorize(menu = 25)]
        public ActionResult ClaimReOpenReason()
        {
            return View();
        }

        [CustomAuthorize(menu = 33)]
        public ActionResult PerfomaInvoice()
        {
            return View();
        }
        [CustomAuthorize(menu = 81)]
        public ActionResult ClaimList()
        {
            return View();
        }
    }
}