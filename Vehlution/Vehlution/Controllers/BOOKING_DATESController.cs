using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution.Models;

namespace Vehlution.Controllers
{
    public class BOOKING_DATESController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();


        // GET: BOOKING_DATES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BOOKING_DATES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DAY_,DATE")] BOOKING_DATES bOOKING_DATES)
        {
            if (ModelState.IsValid)
            {
                db.BOOKING_DATES.Add(bOOKING_DATES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bOOKING_DATES);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
