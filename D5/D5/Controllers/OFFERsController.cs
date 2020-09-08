using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using D5.Models;

namespace D5.Controllers
{
    public class OFFERsController : Controller
    {
        private VehlutionEntities1 db = new VehlutionEntities1();

        // GET: OFFERs
        public ActionResult Index()
        {
            var oFFERS = db.OFFERS.Include(o => o.CAR).Include(o => o.CLIENT).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE).Include(o => o.SALE);
            return View(oFFERS.ToList());
        }

        // GET: OFFERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFERS.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            return View(oFFER);
        }

        // GET: OFFERs/Create
        public ActionResult Create()
        {
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID");
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME");
            ViewBag.STATUS_ID = new SelectList(db.OFFER_STATUS, "STATUS_ID", "STATUS_NAME_");
            ViewBag.OFFERTYPE_ID = new SelectList(db.OFFER_TYPE, "OFFERTYPE_ID", "OFFERTYPE");
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID");
            return View();
        }

        // POST: OFFERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OFFER_ID,CAR_ID,CLIENT_ID,SALES_ID,STATUS_ID,OFFERTYPE_ID,AMOUNT")] OFFER oFFER)
        {
            if (ModelState.IsValid)
            {
                db.OFFERS.Add(oFFER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", oFFER.CAR_ID);
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME", oFFER.CLIENT_ID);
            ViewBag.STATUS_ID = new SelectList(db.OFFER_STATUS, "STATUS_ID", "STATUS_NAME_", oFFER.STATUS_ID);
            ViewBag.OFFERTYPE_ID = new SelectList(db.OFFER_TYPE, "OFFERTYPE_ID", "OFFERTYPE", oFFER.OFFERTYPE_ID);
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID", oFFER.SALES_ID);
            return View(oFFER);
        }

        // GET: OFFERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFERS.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", oFFER.CAR_ID);
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME", oFFER.CLIENT_ID);
            ViewBag.STATUS_ID = new SelectList(db.OFFER_STATUS, "STATUS_ID", "STATUS_NAME_", oFFER.STATUS_ID);
            ViewBag.OFFERTYPE_ID = new SelectList(db.OFFER_TYPE, "OFFERTYPE_ID", "OFFERTYPE", oFFER.OFFERTYPE_ID);
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID", oFFER.SALES_ID);
            return View(oFFER);
        }

        public ActionResult Accept(int id)
        {
            OFFER oFFER = db.OFFERS.Find(id);
            oFFER.STATUS_ID = 1;
            return RedirectToAction("Index");
        }
        public ActionResult Reject(int id)
        {
            OFFER oFFER = db.OFFERS.Find(id);
            oFFER.STATUS_ID = 2;
            return RedirectToAction("Index");
        }

        // POST: OFFERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OFFER_ID,CAR_ID,CLIENT_ID,SALES_ID,STATUS_ID,OFFERTYPE_ID,AMOUNT")] OFFER oFFER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oFFER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", oFFER.CAR_ID);
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME", oFFER.CLIENT_ID);
            ViewBag.STATUS_ID = new SelectList(db.OFFER_STATUS, "STATUS_ID", "STATUS_NAME_", oFFER.STATUS_ID);
            ViewBag.OFFERTYPE_ID = new SelectList(db.OFFER_TYPE, "OFFERTYPE_ID", "OFFERTYPE", oFFER.OFFERTYPE_ID);
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID", oFFER.SALES_ID);
            return View(oFFER);
        }

        // GET: OFFERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OFFER oFFER = db.OFFERS.Find(id);
            if (oFFER == null)
            {
                return HttpNotFound();
            }
            return View(oFFER);
        }

        // POST: OFFERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OFFER oFFER = db.OFFERS.Find(id);
            db.OFFERS.Remove(oFFER);
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
