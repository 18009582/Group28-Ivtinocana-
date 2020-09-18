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
    public class MECHANICsJOBController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: MECHANICsJOB
        public ActionResult Index()
        {
            var mECHANIC_JOB = db.MECHANIC_JOB.Include(m => m.CAR).Include(m => m.MECHANIC);
            return View(mECHANIC_JOB.ToList());
        }

        // GET: MECHANICsJOB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            if (mECHANIC_JOB == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC_JOB);
        }

        // GET: MECHANICsJOB/Create
        public ActionResult Create()
        {
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID");
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_");
            return View();
        }

        // POST: MECHANICsJOB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MECHANICJOB_ID,CAR_ID,MECHANIC_ID,JOB_DATE,JOB_TIME")] MECHANIC_JOB mECHANIC_JOB)
        {
            if (ModelState.IsValid)
            {
                db.MECHANIC_JOB.Add(mECHANIC_JOB);
                db.SaveChanges();
                return RedirectToAction("AdminNav","Nav");
            }

            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", mECHANIC_JOB.CAR_ID);
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_", mECHANIC_JOB.MECHANIC_ID);
            return View(mECHANIC_JOB);
        }

        // GET: MECHANICsJOB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            if (mECHANIC_JOB == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", mECHANIC_JOB.CAR_ID);
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_", mECHANIC_JOB.MECHANIC_ID);
            return View(mECHANIC_JOB);
        }

        // POST: MECHANICsJOB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MECHANICJOB_ID,CAR_ID,MECHANIC_ID,JOB_DATE,JOB_TIME")] MECHANIC_JOB mECHANIC_JOB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mECHANIC_JOB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", mECHANIC_JOB.CAR_ID);
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_", mECHANIC_JOB.MECHANIC_ID);
            return View(mECHANIC_JOB);
        }

        // GET: MECHANICsJOB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            if (mECHANIC_JOB == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC_JOB);
        }

        // POST: MECHANICsJOB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            db.MECHANIC_JOB.Remove(mECHANIC_JOB);
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
