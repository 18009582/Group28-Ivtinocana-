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
    public class EMPLOYEEsController : Controller
    {
        private VehlutionEntities1 db = new VehlutionEntities1();

        // GET: EMPLOYEEs
        public ActionResult Index()
        {
            return View(db.EMPLOYEEs.ToList());
        }

        // GET: EMPLOYEEs/Details/5
        public ActionResult Details(int? id)
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

        // GET: EMPLOYEEs/Create
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
            if (ModelState.IsValid)
            {
                db.EMPLOYEEs.Add(eMPLOYEE);
                db.SaveChanges();
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

            EMPLOYEE e = new EMPLOYEE();
            e = db.EMPLOYEEs.Where(zz=>zz.EMPLYEE_ID==eMPLOYEE.EMPLYEE_ID).FirstOrDefault();
            e.FULL_NAME=eMPLOYEE.FULL_NAME;
            e.EMAIL = eMPLOYEE.EMAIL;
            e.CELL_NUMBER = eMPLOYEE.CELL_NUMBER;
            e.JOB_DESCRIPTION = eMPLOYEE.JOB_DESCRIPTION;
            if(eMPLOYEE.DATE_HIRED!=null)
            {
 e.DATE_HIRED = eMPLOYEE.DATE_HIRED;
            }
           

            db.SaveChanges();
                return RedirectToAction("Index");
           
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
                ViewBag.DelId = 1;
                ViewBag.DelConfirm = "Deletion Successful";
                  return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                
                ViewBag.DelId = 1;
                ViewBag.DelConfirm = "Deletion unsuccessful";
                return RedirectToAction("Index");
                throw;
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
