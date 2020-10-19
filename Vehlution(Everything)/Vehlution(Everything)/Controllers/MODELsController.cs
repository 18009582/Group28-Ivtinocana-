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
    public class MODELsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: MODELs
        public ActionResult CarModelIndex()
        {
            var mODELs = db.MODELs.Include(m => m.MAKE);
            return View(mODELs.ToList());
        }

        // GET: MODELs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODEL mODEL = db.MODELs.Find(id);
            if (mODEL == null)
            {
                return HttpNotFound();
            }
            return View(mODEL);
        }

        // GET: MODELs/Create
        public ActionResult Create()
        {
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            return View();
        }

        // POST: MODELs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKE_ID,MODEL_NAME")] MODEL mODEL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MODELs.Add(mODEL);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car model has sucessfully been added!";
                    return RedirectToAction("CarModelIndex");
                }
   
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("CarModelIndex");
            }

            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME", mODEL.MAKE_ID);
            return View(mODEL);
        }

        // GET: MODELs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODEL mODEL = db.MODELs.Find(id);
            if (mODEL == null)
            {
                return HttpNotFound();
            }
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME", mODEL.MAKE_ID);
            return View(mODEL);
        }

        // POST: MODELs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MODEL_ID,MAKE_ID,MODEL_NAME")] MODEL mODEL)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mODEL).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car model has sucessfully been updated!";
                    return RedirectToAction("CarModelIndex");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("CarModelIndex");
            }
            
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME", mODEL.MAKE_ID);
            return View(mODEL);
        }

        // GET: MODELs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MODEL mODEL = db.MODELs.Find(id);
            if (mODEL == null)
            {
                return HttpNotFound();
            }
            return View(mODEL);
        }

        // POST: MODELs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                MODEL mODEL = db.MODELs.Find(id);

                CAR cAR = new CAR();
                cAR = db.CARS.Where(zz => zz.MODEL.MODEL_ID == mODEL.MODEL_ID).FirstOrDefault();

                if (cAR != null)
                {
                    TempData["AlertMessage"] = "You can not delete this car model because it is linked to a car in the Cars table of the database.";
                    return RedirectToAction("CarModelIndex");
                }

                db.MODELs.Remove(mODEL);
                db.SaveChanges();
                TempData["AlertMessage"] = "A car model has sucessfully been deleted!";
                return RedirectToAction("CarModelIndex");
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("CarModelIndex");
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