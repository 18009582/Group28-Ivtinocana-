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
    public class CAR_PARTsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: CAR_PARTS
        public ActionResult Index()
        {
            return View(db.CAR_PARTS.ToList());
        }

        // GET: CAR_PARTS/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR_PARTS cAR_PARTS = db.CAR_PARTS.Find(id);
            if (cAR_PARTS == null)
            {
                return HttpNotFound();
            }
            return View(cAR_PARTS);
        }

        // GET: CAR_PARTS/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CAR_PARTS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CARPARTS_ID,PARTNAME,STOCKONHAND,RESTOCKORDER")] CAR_PARTS cAR_PARTS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CAR_PARTS.Add(cAR_PARTS);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car part has sucessfully been added!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }

            return View(cAR_PARTS);
        }

        // GET: CAR_PARTS/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR_PARTS cAR_PARTS = db.CAR_PARTS.Find(id);
            if (cAR_PARTS == null)
            {
                return HttpNotFound();
            }
            return View(cAR_PARTS);
        }

        // POST: CAR_PARTS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CARPARTS_ID,PARTNAME,STOCKONHAND,RESTOCKORDER")] CAR_PARTS cAR_PARTS)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(cAR_PARTS).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car part has sucessfully been updated!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }
            
            return View(cAR_PARTS);
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

    
