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
    public class CUSTOMER_SATISFACTIONController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: CUSTOMER_SATISFACTION
        public ActionResult Index()
        {
            return View(db.CUSTOMER_SATISFACTIONs.ToList());
        }

       


        // GET: CUSTOMER_SATISFACTION/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER_SATISFACTION cUSTOMER_SATISFACTION = db.CUSTOMER_SATISFACTIONs.Find(id);
            if (cUSTOMER_SATISFACTION == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER_SATISFACTION);
        }

        // GET: CUSTOMER_SATISFACTION/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CUSTOMER_SATISFACTION/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RateID,Experience,Recommendation,Website,ResponseTime,CustomerComms")] CUSTOMER_SATISFACTION cUSTOMER_SATISFACTION)
        {
            if (ModelState.IsValid)
            {
                db.CUSTOMER_SATISFACTIONs.Add(cUSTOMER_SATISFACTION);
                db.SaveChanges();
                TempData["AlertMessage"] = "Thank you for your feedback!";
                return RedirectToAction("ClientNav", "Nav");
            }

            return View(cUSTOMER_SATISFACTION);
        }

        // GET: CUSTOMER_SATISFACTION/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER_SATISFACTION cUSTOMER_SATISFACTION = db.CUSTOMER_SATISFACTIONs.Find(id);
            if (cUSTOMER_SATISFACTION == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER_SATISFACTION);
        }

        // POST: CUSTOMER_SATISFACTION/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RateID,Experience,Recommendation,Website,ResponseTime,CustomerComms")] CUSTOMER_SATISFACTION cUSTOMER_SATISFACTION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cUSTOMER_SATISFACTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cUSTOMER_SATISFACTION);
        }

        // GET: CUSTOMER_SATISFACTION/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CUSTOMER_SATISFACTION cUSTOMER_SATISFACTION = db.CUSTOMER_SATISFACTIONs.Find(id);
            if (cUSTOMER_SATISFACTION == null)
            {
                return HttpNotFound();
            }
            return View(cUSTOMER_SATISFACTION);
        }

        // POST: CUSTOMER_SATISFACTION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CUSTOMER_SATISFACTION cUSTOMER_SATISFACTION = db.CUSTOMER_SATISFACTIONs.Find(id);
            db.CUSTOMER_SATISFACTIONs.Remove(cUSTOMER_SATISFACTION);
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

        public ActionResult CustomerSatisfaction()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ChartSatisfaction()
        {
            var list = db.CUSTOMER_SATISFACTIONs.ToList();
            List<CustomerSatisfaction> lst = new List<CustomerSatisfaction>();
            CustomerSatisfaction cs1 = new CustomerSatisfaction();
            if (list != null)
            {
                cs1.CustomerComms = list.Sum(x => x.CustomerComms) ?? 0;
                if (cs1.CustomerComms != 0)
                    cs1.CustomerComms = cs1.CustomerComms / list.Count;
                cs1.Experience = list.Sum(x => x.Experience) ?? 0;
                if (cs1.Experience != 0)
                    cs1.Experience = cs1.Experience / list.Count;
                cs1.Recommendation = list.Sum(x => x.Recommendation) ?? 0;
                if (cs1.Recommendation != 0)
                    cs1.Recommendation = cs1.Recommendation / list.Count;
                cs1.ResponseTime = list.Sum(x => x.ResponseTime) ?? 0;
                if (cs1.ResponseTime != 0)
                    cs1.ResponseTime = cs1.ResponseTime / list.Count;
                cs1.Website = list.Sum(x => x.Website) ?? 0;
                if (cs1.Website != 0)
                    cs1.Website = cs1.Website / list.Count;
            }

            return Json(cs1, JsonRequestBehavior.AllowGet);
        }
    }
    public class CustomerSatisfaction : CUSTOMER_SATISFACTION
    {
        public int Average { get; set; }
        //public int Experience { get; set; }
        //public int Recommendation { get; set; }
        //public int Website { get; set; }
        //public int ResponseTime { get; set; }
        //public int CustomerComms { get; set; }
    }
}
