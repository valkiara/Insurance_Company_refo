using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
  [CustomAuthorize]
    public class CommisionStructureController : Controller
    {
        [CustomAuthorize(menu = 6)]
        public ActionResult Index()
        {
            return View();
        }
    }
}