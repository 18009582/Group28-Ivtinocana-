using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using Vehlution.Models;

namespace Vehlution.Controllers
{
    public class CAR_BOOKING_SLOTSController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: CAR_BOOKING_SLOTS
        public ActionResult BookingSlotsIndex()
        {
            var cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Include(c => c.BOOKING_DATES).Include(c => c.BOOKING_STATUS).Include(c => c.BOOKING_TIMES).Include(c => c.CAR_BOOKING);
            return View(cAR_BOOKING_SLOTS.ToList());
        }

        // GET: CAR_BOOKING_SLOTS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Find(id);
            if (cAR_BOOKING_SLOTS == null)
            {
                return HttpNotFound();
            }
            return View(cAR_BOOKING_SLOTS);
        }

        // GET: CAR_BOOKING_SLOTS/Create
        public ActionResult Create()
        {
            List<BOOKING_DATES> dateslist = new List<BOOKING_DATES>();
            int count = 0;
            List<dynamic> d = new List<dynamic>();
            dateslist = db.BOOKING_DATES.ToList();
            foreach (BOOKING_DATES x in dateslist)
            {
                dynamic dd = new ExpandoObject();
                //  dd."DAY_ = x.DAY_;
                dd.DATE = (x.DATE).Value.ToString("yyyy/mm/dd");
                d.Add(dd);

            }
            ViewBag.DAY_ = new SelectList(d, "DAY_", "DATE");

            //   ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DATE");
            ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "PROPOSED");
            ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", Convert.ToString("START_TIME_"));
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "BOOKING_ID", "STATUS");
            return View();
        }

        // POST: CAR_BOOKING_SLOTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TIME_ID,DAY_,STATUSID,BOOKING_ID")] CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS)
        {
            if (ModelState.IsValid)
            {
                db.CAR_BOOKING_SLOTS.Add(cAR_BOOKING_SLOTS);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DAY_", cAR_BOOKING_SLOTS.DAY_);
            ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "PROPOSED", cAR_BOOKING_SLOTS.STATUSID);
            ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "TIME_ID", cAR_BOOKING_SLOTS.TIME_ID);
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "BOOKING_ID", "STATUS", cAR_BOOKING_SLOTS.BOOKING_ID);
            return View(cAR_BOOKING_SLOTS);
        }

        // GET: CAR_BOOKING_SLOTS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Find(id);
            if (cAR_BOOKING_SLOTS == null)
            {
                return HttpNotFound();
            }
            ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DAY_", cAR_BOOKING_SLOTS.DAY_);
            ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "PROPOSED", cAR_BOOKING_SLOTS.STATUSID);
            ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "TIME_ID", cAR_BOOKING_SLOTS.TIME_ID);
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "BOOKING_ID", "STATUS", cAR_BOOKING_SLOTS.BOOKING_ID);
            return View(cAR_BOOKING_SLOTS);
        }

        // POST: CAR_BOOKING_SLOTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TIME_ID,DAY_,STATUSID,BOOKING_ID")] CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAR_BOOKING_SLOTS).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DAY_", cAR_BOOKING_SLOTS.DAY_);
            ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "PROPOSED", cAR_BOOKING_SLOTS.STATUSID);
            ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "TIME_ID", cAR_BOOKING_SLOTS.TIME_ID);
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "BOOKING_ID", "STATUS", cAR_BOOKING_SLOTS.BOOKING_ID);
            return View(cAR_BOOKING_SLOTS);
        }

        // GET: CAR_BOOKING_SLOTS/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Find(id);
            if (cAR_BOOKING_SLOTS == null)
            {
                return HttpNotFound();
            }
            return View(cAR_BOOKING_SLOTS);
        }

        // POST: CAR_BOOKING_SLOTS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Find(id);
            db.CAR_BOOKING_SLOTS.Remove(cAR_BOOKING_SLOTS);
            db.SaveChanges();
            return RedirectToAction("Index");
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
