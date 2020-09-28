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
    public class EMPLYEE_SALESController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        public static List<EMPLYEE_SALES> employeelist = new List<EMPLYEE_SALES>();
        // GET: EMPLYEE_SALES
       // public ActionResult Index()
        //{
          //  var eMPLYEE_SALES = db.EMPLYEE_SALES.Include(e => e.EMPLOYEE).Include(e => e.SALE);
            //return View(eMPLYEE_SALES.ToList());
        //}

        public ActionResult Index()
        {
            ViewBag.Models = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME");
            return View(employeelist);
        }
        [HttpPost]
        public ActionResult Search(int Models)
        {
           
            employeelist = db.EMPLYEE_SALES.Where(zz => zz.EMPLYEE_ID == Models).ToList();

            return RedirectToAction("Index");
        }


        // GET: EMPLYEE_SALES/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLYEE_SALES eMPLYEE_SALES = db.EMPLYEE_SALES.Find(id);
            if (eMPLYEE_SALES == null)
            {
                return HttpNotFound();
            }
            return View(eMPLYEE_SALES);
        }

        // GET: EMPLYEE_SALES/Create
        public ActionResult Create()
        {
            ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME");
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID");
            return View();
        }

        // POST: EMPLYEE_SALES/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EMPLYEE_ID,SALES_ID,EMPLOYEESALESID")] EMPLYEE_SALES eMPLYEE_SALES)
        {
            if (ModelState.IsValid)
            {
                db.EMPLYEE_SALES.Add(eMPLYEE_SALES);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME", eMPLYEE_SALES.EMPLYEE_ID);
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID", eMPLYEE_SALES.SALES_ID);
            return View(eMPLYEE_SALES);
        }

        // GET: EMPLYEE_SALES/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLYEE_SALES eMPLYEE_SALES = db.EMPLYEE_SALES.Find(id);
            if (eMPLYEE_SALES == null)
            {
                return HttpNotFound();
            }
            ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME", eMPLYEE_SALES.EMPLYEE_ID);
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID", eMPLYEE_SALES.SALES_ID);
            return View(eMPLYEE_SALES);
        }

        // POST: EMPLYEE_SALES/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EMPLYEE_ID,SALES_ID,EMPLOYEESALESID")] EMPLYEE_SALES eMPLYEE_SALES)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eMPLYEE_SALES).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EMPLYEE_ID = new SelectList(db.EMPLOYEEs, "EMPLYEE_ID", "FULL_NAME", eMPLYEE_SALES.EMPLYEE_ID);
            ViewBag.SALES_ID = new SelectList(db.SALES, "SALES_ID", "SALES_ID", eMPLYEE_SALES.SALES_ID);
            return View(eMPLYEE_SALES);
        }

        // GET: EMPLYEE_SALES/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLYEE_SALES eMPLYEE_SALES = db.EMPLYEE_SALES.Find(id);
            if (eMPLYEE_SALES == null)
            {
                return HttpNotFound();
            }
            return View(eMPLYEE_SALES);
        }

        // POST: EMPLYEE_SALES/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EMPLYEE_SALES eMPLYEE_SALES = db.EMPLYEE_SALES.Find(id);
            db.EMPLYEE_SALES.Remove(eMPLYEE_SALES);
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
