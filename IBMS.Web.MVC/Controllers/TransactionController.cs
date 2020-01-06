using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class TransactionController : Controller
    {
        [CustomAuthorize(menu = 28)]
        public ActionResult ClientRequest()
        {
            return View();
        }

        [CustomAuthorize(menu = 28)]
        public ActionResult ManageClientRequest()
        {
            return View();
        }

        [CustomAuthorize(menu = 29)]
        public ActionResult Quatation()
        {
            return View();
        }

        [CustomAuthorize(menu = 29)]
        public ActionResult ManageQuotation()
        {
            return View();
        }

        [CustomAuthorize(menu = 34)]
        public ActionResult ManagePayment()
        {
            return View();
        }

        [CustomAuthorize(menu = 35)]
        public ActionResult ManagePolicyInfoRecording()
        {
            return View();
        }

        [CustomAuthorize(menu = 36)]
        public ActionResult ManagePolicyRenewalHistory()
        {
            return View();
        }

        [CustomAuthorize(menu = 60)]
        public ActionResult ManageClaimRecording()
        {
            return View();
        }

        [CustomAuthorize(menu = 62)]
        public ActionResult ManageAvivaClaimRecording()
        {
            return View();
        }

        [CustomAuthorize(menu = 46)]
        public ActionResult ManageTransaction()
        {
            return View();
        }

        [CustomAuthorize(menu = 45)]
        public ActionResult ManageBUPATransaction()
        {
            return View();
        }

        [CustomAuthorize(menu = 41)]
        public ActionResult ManageTransactionPayment()
        {
            return View();
        }

        [CustomAuthorize(menu = 64)]
        public ActionResult ManagePilotTransaction()
        {
            return View();
        }
        public ActionResult ManagePaymentInvoice()
        {
            return View();
        }

        public ActionResult CommissionExcelUpload()
        {
            return View();
        }

        public ActionResult ManageQuotationPrint()
        {
            return View();
        }
        [CustomAuthorize(menu = 67)]
        public ActionResult ManageBUPAClaimRecording()
        {
            return View();
        }

        [CustomAuthorize(menu = 68)]
        public ActionResult ManageNestleRecording()
        {
            return View();
        }


        [CustomAuthorize(menu = 70)]
        public ActionResult ManageNestleClaimRecording()
        {
            return View();
        }

        [CustomAuthorize(menu = 69)]
        public ActionResult ManagePilotClaimRecording()
        {
            return View();
        }

        [CustomAuthorize(menu = 71)]
        public ActionResult ManagePilotRenewal()
        {
            return View();
        }

        [CustomAuthorize(menu = 72)]
        public ActionResult ManageNestleRenewal()
        {
            return View();
        }

        [CustomAuthorize(menu = 73)]
        public ActionResult ManageAvivaRenewal()
        {
            return View();
        }

        [CustomAuthorize(menu = 74)]
        public ActionResult ManageBUPARenewal()
        {
            return View();
        }


        [CustomAuthorize(menu = 76)]
        public ActionResult ManageViewBUPARenewal()
        {
            return View();
        }

        [CustomAuthorize(menu = 75)]
        public ActionResult ManageCMAClaimRecording()
        {
            return View();
        }
        //[CustomAuthorize(menu = 76)]
        //public ActionResult ManageViewBUPARenewal()
        //{
        //    return View();
        //}


        public ActionResult ReceivedQuotation()
        {
            return View();
        }

    }
}