﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using IBMS.Web.MVC.Security;

namespace IBMS.Web.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        [CustomAuthorize(menu = 12)]
        public ActionResult Index()
        {
            return View();
        }
    }
}