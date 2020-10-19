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
        public ActionResult Create(string MAKE_NAME)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<MAKE> mAKEs = new List<MAKE>();
                    mAKEs = db.MAKEs.ToList();
                    MAKE n = new MAKE();
                    n.MAKE_NAME = MAKE_NAME;
                    foreach (var item in mAKEs)
                    {
                        if (item.MAKE_NAME.Trim() == MAKE_NAME.Trim())
                        {
                            TempData["AlertMessage"] = "This car make already exists in our system";
                            return RedirectToAction("CarMakeIndex");
                        }
                    }

                    db.MAKEs.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car make has successfully been added!";
                    return RedirectToAction("CarMakeIndex");
                }
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later" + err;
                return RedirectToAction("CarMakeIndex");
            }
            

            return View(MAKE_NAME);
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
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(mAKE).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car make has successfully been updated!";
                    return RedirectToAction("CarMakeIndex");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
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
            try
            {
                CAR cAR = new CAR();

                MAKE mAKE = db.MAKEs.Find(id);

                cAR = db.CARS.Where(zz => zz.MODEL.MAKE_ID == mAKE.MAKE_ID).FirstOrDefault();

                if(cAR != null)
                {
                    TempData["AlertMessage"] = "You can not delete this car make because it is linked to a car in the Cars table of the database.";
                    return RedirectToAction("CarMakeIndex");
                }
                else
                {
                    List<MODEL> mODELs = new List<MODEL>();
                    mODELs = db.MODELs.Where(zz => zz.MAKE_ID == mAKE.MAKE_ID).ToList();

                    foreach(var i in mODELs)
                    {
                        db.MODELs.Remove(i);
                        db.SaveChanges();
                    }

                    db.MAKEs.Remove(mAKE);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A car make has sucessfully been deleted!";
                    return RedirectToAction("CarMakeIndex");
                }
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later" + err;
                return RedirectToAction("CarMakeIndex");
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