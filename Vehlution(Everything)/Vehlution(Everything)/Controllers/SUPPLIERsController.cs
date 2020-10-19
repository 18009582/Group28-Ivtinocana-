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
    public class SUPPLIERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: SUPPLIERs
        public ActionResult Index()
        {
            return View(db.SUPPLIERs.ToList());
        }

        // GET: SUPPLIERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUPPLIERs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SUPPLIER_ID,NAME_,CELL_NUMBER_,EMAIL_,ADDRESS")] SUPPLIER sUPPLIER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.SUPPLIERs.Add(sUPPLIER);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A supplier has sucessfully been added!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Index");
            }
            

            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // POST: SUPPLIERs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SUPPLIER_ID,NAME_,CELL_NUMBER_,EMAIL_,ADDRESS")] SUPPLIER sUPPLIER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(sUPPLIER).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A supplier has sucessfully been updated!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Index");
            }
            
            return View(sUPPLIER);
        }

        // GET: SUPPLIERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
            if (sUPPLIER == null)
            {
                return HttpNotFound();
            }
            return View(sUPPLIER);
        }

        // POST: SUPPLIERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ORDER oRDER = new ORDER();

                SUPPLIER sUPPLIER = db.SUPPLIERs.Find(id);
                oRDER = db.ORDERs.Where(zz => zz.SUPPLIER.SUPPLIER_ID == sUPPLIER.SUPPLIER_ID).FirstOrDefault();
                if (oRDER != null)
                {
                    TempData["AlertMessage"] = "You can not delete this supplier because it is linked to an order in the Orders table of the database.";
                    return RedirectToAction("Index");
                }
                
                db.SUPPLIERs.Remove(sUPPLIER);
                db.SaveChanges();
                TempData["AlertMessage"] = "A supplier has sucessfully been deleted!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
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