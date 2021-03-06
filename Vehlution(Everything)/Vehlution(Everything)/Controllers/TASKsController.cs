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
    public class TASKsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: TASKs
        public ActionResult Index()
        {
            return View(db.TASKs.ToList());
        }

        // GET: TASKs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TASK tASK = db.TASKs.Find(id);
            if (tASK == null)
            {
                return HttpNotFound();
            }
            return View(tASK);
        }

        // GET: TASKs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TASKs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SERVICE_ID,SERVICE_NAME")] TASK tASK)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.TASKs.Add(tASK);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A mechanic task has sucessfully been added!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }
            return View(tASK);
        }

        // GET: TASKs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TASK tASK = db.TASKs.Find(id);
            if (tASK == null)
            {
                return HttpNotFound();
            }
            return View(tASK);
        }

        // POST: TASKs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SERVICE_ID,SERVICE_NAME")] TASK tASK)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tASK).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A mechanic task has sucessfully been updated!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }
            return View(tASK);
        }

        // GET: TASKs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TASK tASK = db.TASKs.Find(id);
            if (tASK == null)
            {
                return HttpNotFound();
            }
            return View(tASK);
        }

        // POST: TASKs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                MECHANIC_TASK mECHANIC_TASK = new MECHANIC_TASK();
                TASK tASK = db.TASKs.Find(id);

                mECHANIC_TASK = db.MECHANIC_TASK.Where(zz => zz.TASK.SERVICE_ID == tASK.SERVICE_ID).FirstOrDefault();
                if (mECHANIC_TASK != null)
                {
                    TempData["AlertMessage"] = "You can not delete this task because it is linked to a task done by a mechanic in the Mechanic Task table of the database.";
                    return RedirectToAction("Index");
                }
                db.TASKs.Remove(tASK);
                db.SaveChanges();
                TempData["AlertMessage"] = "A mechanic task has sucessfully been deleted!";
                return RedirectToAction("Index");
            }
            
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
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

