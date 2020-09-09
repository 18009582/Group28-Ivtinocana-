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
    public class SALEsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: SALEs
        public ActionResult Index()
        {
            var sALES = db.SALES.Include(s => s.OFFER).Include(s => s.PAYMENT);
            return View(sALES.ToList());
        }

        // GET: SALEs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALES.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }

        // GET: SALEs/Create
        public ActionResult Create()
        {
            ViewBag.OFFER_ID = new SelectList(db.OFFERS, "OFFER_ID", "OFFER_ID");
            ViewBag.PAYMENT_ID = new SelectList(db.PAYMENTs, "PAYMENT_ID", "PAYMENTTYPE");
            return View();
        }

        // POST: SALEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SALES_ID,PAYMENT_ID,OFFER_ID,SALE_DATE_,ACCEPTED_OFFER,CAR_CONTRACT_")] SALE sALE)
        {
            if (ModelState.IsValid)
            {
                db.SALES.Add(sALE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OFFER_ID = new SelectList(db.OFFERS, "OFFER_ID", "OFFER_ID", sALE.OFFER_ID);
            ViewBag.PAYMENT_ID = new SelectList(db.PAYMENTs, "PAYMENT_ID", "PAYMENTTYPE", sALE.PAYMENT_ID);
            return View(sALE);
        }

        // GET: SALEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALES.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            ViewBag.OFFER_ID = new SelectList(db.OFFERS, "OFFER_ID", "OFFER_ID", sALE.OFFER_ID);
            ViewBag.PAYMENT_ID = new SelectList(db.PAYMENTs, "PAYMENT_ID", "PAYMENTTYPE", sALE.PAYMENT_ID);
            return View(sALE);
        }

        // POST: SALEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SALES_ID,PAYMENT_ID,OFFER_ID,SALE_DATE_,ACCEPTED_OFFER,CAR_CONTRACT_")] SALE sALE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sALE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OFFER_ID = new SelectList(db.OFFERS, "OFFER_ID", "OFFER_ID", sALE.OFFER_ID);
            ViewBag.PAYMENT_ID = new SelectList(db.PAYMENTs, "PAYMENT_ID", "PAYMENTTYPE", sALE.PAYMENT_ID);
            return View(sALE);
        }

        // GET: SALEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SALE sALE = db.SALES.Find(id);
            if (sALE == null)
            {
                return HttpNotFound();
            }
            return View(sALE);
        }

        // POST: SALEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SALE sALE = db.SALES.Find(id);
            db.SALES.Remove(sALE);
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
