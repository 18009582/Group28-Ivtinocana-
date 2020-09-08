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
    public class MODELsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: MODELs
        public ActionResult CarModelIndex()
        {
            var mODELs = db.MODELs.Include(m => m.MAKEs);
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
            if (ModelState.IsValid)
            {
                db.MODELs.Add(mODEL);
                db.SaveChanges();
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
            if (ModelState.IsValid)
            {
                db.Entry(mODEL).State = EntityState.Modified;
                db.SaveChanges();
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
            MODEL mODEL = db.MODELs.Find(id);
            db.MODELs.Remove(mODEL);
            db.SaveChanges();
            return RedirectToAction("CarModelIndex");
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