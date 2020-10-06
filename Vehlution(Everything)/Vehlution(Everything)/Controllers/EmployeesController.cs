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
    public class EmployeesController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: EMPLOYEEs
        public ActionResult Index()
        {
            return View(db.EMPLOYEEs.ToList());
        }


        public ActionResult Create()
        {
            return View();
        }

        // POST: EMPLOYEEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create([Bind(Include = "EMPLYEE_ID,FULL_NAME,CELL_NUMBER,EMAIL,JOB_DESCRIPTION,DATE_HIRED")] EMPLOYEE eMPLOYEE)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.EMPLOYEEs.Add(eMPLOYEE);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A employee has sucessfully been added!";
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }

            return View(eMPLOYEE);
        }

        // GET: EMPLOYEEs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // POST: EMPLOYEEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit([Bind(Include = "EMPLYEE_ID,FULL_NAME,CELL_NUMBER,EMAIL,JOB_DESCRIPTION,DATE_HIRED")] EMPLOYEE eMPLOYEE)
        {
            try
            {
                EMPLOYEE e = new EMPLOYEE();
                e = db.EMPLOYEEs.Where(zz => zz.EMPLYEE_ID == eMPLOYEE.EMPLYEE_ID).FirstOrDefault();
                e.FULL_NAME = eMPLOYEE.FULL_NAME;
                e.EMAIL = eMPLOYEE.EMAIL;
                e.CELL_NUMBER = eMPLOYEE.CELL_NUMBER;
                e.JOB_DESCRIPTION = eMPLOYEE.JOB_DESCRIPTION;
                if (eMPLOYEE.DATE_HIRED != null)
                {
                    e.DATE_HIRED = eMPLOYEE.DATE_HIRED;
                }

                db.SaveChanges();
                TempData["AlertMessage"] = "A employee has sucessfully been updated!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }


        }

        // GET: EMPLOYEEs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
            if (eMPLOYEE == null)
            {
                return HttpNotFound();
            }
            return View(eMPLOYEE);
        }

        // POST: EMPLOYEEs/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                EMPLOYEE eMPLOYEE = db.EMPLOYEEs.Find(id);
                db.EMPLOYEEs.Remove(eMPLOYEE);
                db.SaveChanges();

                TempData["AlertMessage"] = "A employee has sucessfully been deleted!";
                return RedirectToAction("Index");
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
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