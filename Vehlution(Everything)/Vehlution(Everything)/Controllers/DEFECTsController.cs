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
    public class DEFECTsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: DEFECTs
        public ActionResult Index()
        {
            return View(db.DEFECTS.ToList());
        }

        // GET: DEFECTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEFECT dEFECT = db.DEFECTS.Find(id);
            if (dEFECT == null)
            {
                return HttpNotFound();
            }
            return View(dEFECT);
        }

        // GET: DEFECTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DEFECTs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DEFECT_ID,DEFECTNAME")] DEFECT dEFECT)
        {
            if (ModelState.IsValid)
            {
                db.DEFECTS.Add(dEFECT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dEFECT);
        }

        // GET: DEFECTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEFECT dEFECT = db.DEFECTS.Find(id);
            if (dEFECT == null)
            {
                return HttpNotFound();
            }
            return View(dEFECT);
        }

        // POST: DEFECTs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DEFECT_ID,DEFECTNAME")] DEFECT dEFECT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dEFECT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dEFECT);
        }

        // GET: DEFECTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DEFECT dEFECT = db.DEFECTS.Find(id);
            if (dEFECT == null)
            {
                return HttpNotFound();
            }
            return View(dEFECT);
        }

        // POST: DEFECTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DEFECT dEFECT = db.DEFECTS.Find(id);
            db.DEFECTS.Remove(dEFECT);
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
