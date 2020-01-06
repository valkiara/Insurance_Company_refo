using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBMS.Web.MVC.Controllers
{
    public class BUPAClameController : Controller
    {
        // GET: BUPAClame
        public ActionResult Index()
        {
            return View();
        }

        // GET: BUPAClame/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BUPAClame/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BUPAClame/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BUPAClame/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BUPAClame/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BUPAClame/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BUPAClame/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
