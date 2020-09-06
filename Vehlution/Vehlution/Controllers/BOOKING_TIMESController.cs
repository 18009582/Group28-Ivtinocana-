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
    public class BOOKING_TIMESController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();


        // GET: BOOKING_TIMES/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BOOKING_TIMES/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TIME_ID,START_TIME_,END_TIME")] BOOKING_TIMES bOOKING_TIMES)
        {
            if (ModelState.IsValid)
            {
                db.BOOKING_TIMES.Add(bOOKING_TIMES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bOOKING_TIMES);
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
