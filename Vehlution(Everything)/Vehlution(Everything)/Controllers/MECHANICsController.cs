﻿using System;
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.MECHANICs.Add(mECHANIC);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A mechanic has sucessfully been added!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mECHANIC).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A mechanic has sucessfully been updated!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
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
            try
            {
                MECHANIC_JOB mECHANIC_JOB = new MECHANIC_JOB();

                MECHANIC mECHANIC = db.MECHANICs.Find(id);

                mECHANIC_JOB = db.MECHANIC_JOB.Where(zz => zz.MECHANIC.MECHANIC_ID == mECHANIC.MECHANIC_ID).FirstOrDefault();
                if (mECHANIC_JOB != null)
                {
                    TempData["AlertMessage"] = "You can not delete this mechanic because it is linked to a mechanic job in the Mechanic Job table of the database.";
                    return RedirectToAction("Index");
                }

                db.MECHANICs.Remove(mECHANIC);
                db.SaveChanges();
                TempData["AlertMessage"] = "A mechanic has sucessfully been deleted!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Index");
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
