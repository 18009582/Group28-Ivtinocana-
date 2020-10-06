using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;

namespace Vehlution_Everything_.Controllers
{
    public class CAR_BOOKING_SLOTSController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: CAR_BOOKING_SLOTS
        public ActionResult BookingSlotsIndex()
        {
            var cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Include(c => c.BOOKING_DATES).Include(c => c.BOOKING_STATUS).Include(c => c.BOOKING_TIMES).Include(c => c.EMPLOYEE);
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
            ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DATE");
            ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "STATUSNAME");
            ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "START_TIME_");
            ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME");
            return View();
        }

        // POST: CAR_BOOKING_SLOTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TIME_ID,DAY_,STATUSID,EMPLYEE_ID,CARBOOKINGSLOTID")] CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CAR_BOOKING_SLOTS.Add(cAR_BOOKING_SLOTS);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A booking time slot has sucessfully been added!";
                    return RedirectToAction("BookingSlotsIndex");
                }

                ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DAY_", cAR_BOOKING_SLOTS.DAY_);
                ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "STATUSNAME", cAR_BOOKING_SLOTS.STATUSID);
                ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "TIME_ID", cAR_BOOKING_SLOTS.TIME_ID);
                ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME", cAR_BOOKING_SLOTS.EMPLYEE_ID);
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("BookingSlotsIndex");
            }
            

            
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
            ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "STATUSNAME", cAR_BOOKING_SLOTS.STATUSID);
            ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "TIME_ID", cAR_BOOKING_SLOTS.TIME_ID);
            ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME", cAR_BOOKING_SLOTS.EMPLYEE_ID);
            return View(cAR_BOOKING_SLOTS);
        }

        // POST: CAR_BOOKING_SLOTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TIME_ID,DAY_,STATUSID,EMPLYEE_ID,CARBOOKINGSLOTID")] CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cAR_BOOKING_SLOTS).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A booking time slot has sucessfully been updated!";
                    return RedirectToAction("BookingSlotsIndex");
                }

                ViewBag.DAY_ = new SelectList(db.BOOKING_DATES, "DAY_", "DATE", cAR_BOOKING_SLOTS.DAY_);
                ViewBag.STATUSID = new SelectList(db.BOOKING_STATUS, "STATUSID", "STATUSNAME", cAR_BOOKING_SLOTS.STATUSID);
                ViewBag.TIME_ID = new SelectList(db.BOOKING_TIMES, "TIME_ID", "START_TIME_", cAR_BOOKING_SLOTS.TIME_ID);
                ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME", cAR_BOOKING_SLOTS.EMPLYEE_ID);
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("BookingSlotsIndex");
            }
            
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
            try
            {
                CAR_BOOKING_SLOTS cAR_BOOKING_SLOTS = db.CAR_BOOKING_SLOTS.Find(id);
                db.CAR_BOOKING_SLOTS.Remove(cAR_BOOKING_SLOTS);
                db.SaveChanges();
                TempData["AlertMessage"] = "A booking time slot has sucessfully been updated!";
                return RedirectToAction("BookingSlotsIndex");
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("BookingSlotsIndex");
            }
            
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
