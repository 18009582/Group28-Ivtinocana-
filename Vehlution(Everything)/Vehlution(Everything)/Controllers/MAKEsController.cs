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
    public class MAKEsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: MAKEs
        public ActionResult CarMakeIndex()
        {
            return View(db.MAKEs.ToList());
        }

        // GET: MAKEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAKE mAKE = db.MAKEs.Find(id);
            if (mAKE == null)
            {
                return HttpNotFound();
            }
            return View(mAKE);
        }

        // GET: MAKEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MAKEs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MAKE_NAME")] MAKE mAKE)
        {
            if (ModelState.IsValid)
            {
                db.MAKEs.Add(mAKE);
                db.SaveChanges();
                return RedirectToAction("CarMakeIndex");
            }

            return View(mAKE);
        }

        // GET: MAKEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAKE mAKE = db.MAKEs.Find(id);
            if (mAKE == null)
            {
                return HttpNotFound();
            }
            return View(mAKE);
        }

        // POST: MAKEs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MAKE_ID,MAKE_NAME")] MAKE mAKE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mAKE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CarMakeIndex");
            }
            return View(mAKE);
        }

        // GET: MAKEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MAKE mAKE = db.MAKEs.Find(id);
            if (mAKE == null)
            {
                return HttpNotFound();
            }
            return View(mAKE);
        }

        // POST: MAKEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MAKE mAKE = db.MAKEs.Find(id);
            db.MAKEs.Remove(mAKE);
            db.SaveChanges();
            return RedirectToAction("CarMakeIndex");
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