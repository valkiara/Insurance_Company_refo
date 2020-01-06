using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBMS.Web.MVC.Controllers
{
    public class LocalAdmissionController : Controller
    {
        // GET: LocalAdmission
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult SingaporeAdmission()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetInvoice()
        {
            return View();
        }

        
        public ActionResult GetSingaporeInvoice()
        {
            return View();
        }
    }
}