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
    public class MECHANICsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: MECHANICs
        public ActionResult Index()
        {
            return View(db.MECHANICs.ToList());
        }

        // GET: MECHANICs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC mECHANIC = db.MECHANICs.Find(id);
            if (mECHANIC == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC);
        }

        // GET: MECHANICs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MECHANICs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MECHANIC_ID,FULL_NAME_,CELL_NUMBER_,EMAIL_")] MECHANIC mECHANIC)
        {
            if (ModelState.IsValid)
            {
                db.MECHANICs.Add(mECHANIC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mECHANIC);
        }

        // GET: MECHANICs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC mECHANIC = db.MECHANICs.Find(id);
            if (mECHANIC == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC);
        }

        // POST: MECHANICs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MECHANIC_ID,FULL_NAME_,CELL_NUMBER_,EMAIL_")] MECHANIC mECHANIC)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mECHANIC).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mECHANIC);
        }

        // GET: MECHANICs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC mECHANIC = db.MECHANICs.Find(id);
            if (mECHANIC == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC);
        }

        // POST: MECHANICs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MECHANIC mECHANIC = db.MECHANICs.Find(id);
            db.MECHANICs.Remove(mECHANIC);
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
