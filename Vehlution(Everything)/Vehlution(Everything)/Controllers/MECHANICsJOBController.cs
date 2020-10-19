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
    public class MECHANICsJOBController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        static public List<MechTask> NewPartsOrder = new List<MechTask>();
        // GET: MECHANICsJOB
        public ActionResult Index()
        {
            var mECHANIC_JOB = db.MECHANIC_JOB.Include(m => m.CAR).Include(m => m.MECHANIC);
            return View(mECHANIC_JOB.ToList());
        }

        // GET: MECHANICsJOB/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            if (mECHANIC_JOB == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC_JOB);
        }

        // GET: MECHANICsJOB/Create
        public ActionResult Create()
        {
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CarReg");
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_");
            ViewBag.Tasks = new SelectList(db.TASKs, "SERVICE_ID", "SERVICE_NAME");
            ViewBag.CarParts = new SelectList(db.CAR_PARTS, "CARPARTS_ID", "PARTNAME");
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_REG");
            ViewBag.TaskList = NewPartsOrder;
            return View();
        }

        // POST: MECHANICsJOB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
   
        public ActionResult Create([Bind(Include = "MECHANICJOB_ID,CAR_ID,MECHANIC_ID,JOB_DATE,JOB_TIME")] MECHANIC_JOB mECHANIC_JOB)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.MECHANIC_JOB.Add(mECHANIC_JOB);
                    db.SaveChanges();
                    int id = db.MECHANIC_JOB.ToList().Last().MECHANICJOB_ID;
                    foreach (MechTask x in NewPartsOrder)
                    {

                        MECHANIC_TASK c = new MECHANIC_TASK();
                        c.MECHANICJOB_ID = id;
                        RELATIONSHIP_12 d = new RELATIONSHIP_12();
                        d.CARPARTS_ID = x.PartID;
                        db.RELATIONSHIP_12.Add(d);
                        db.SaveChanges();

                        c.CARPARTSUSEDID = db.RELATIONSHIP_12.ToList().Last().CARPARTSUSEDID;
                        c.CARPARTS_ID = x.PartID;
                        c.SERVICE_ID = x.TaskId;
                        db.MECHANIC_TASK.Add(c);
                        db.CAR_PARTS.Find(x.PartID).STOCKONHAND -= x.Qty;
                        db.SaveChanges();
                    }
                    TempData["AlertMessage"] = "Mechanic Job has successfully been added!";
                    return RedirectToAction("AdminNav", "Nav");
                }

                ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", mECHANIC_JOB.CAR_ID);
                ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_", mECHANIC_JOB.MECHANIC_ID);
                TempData["AlertMessage"] = "Mechanic Job has successfully been added!";
                return View(mECHANIC_JOB);
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later" + err;
                return View(mECHANIC_JOB);

            }
            
        }

        // GET: MECHANICsJOB/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            if (mECHANIC_JOB == null)
            {
                return HttpNotFound();
            }
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", mECHANIC_JOB.CAR_ID);
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_", mECHANIC_JOB.MECHANIC_ID);
            return View(mECHANIC_JOB);
        }

        // POST: MECHANICsJOB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MECHANICJOB_ID,CAR_ID,MECHANIC_ID,JOB_DATE,JOB_TIME")] MECHANIC_JOB mECHANIC_JOB)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mECHANIC_JOB).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CAR_ID = new SelectList(db.CARS, "CAR_ID", "CAR_ID", mECHANIC_JOB.CAR_ID);
            ViewBag.MECHANIC_ID = new SelectList(db.MECHANICs, "MECHANIC_ID", "FULL_NAME_", mECHANIC_JOB.MECHANIC_ID);
            return View(mECHANIC_JOB);
        }

        // GET: MECHANICsJOB/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            if (mECHANIC_JOB == null)
            {
                return HttpNotFound();
            }
            return View(mECHANIC_JOB);
        }

        // POST: MECHANICsJOB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MECHANIC_JOB mECHANIC_JOB = db.MECHANIC_JOB.Find(id);
            db.MECHANIC_JOB.Remove(mECHANIC_JOB);
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
        [HttpPost]
        public ActionResult AddToList(int CarParts, int Qty, int Tasks)
        {
            MechTask newOrder = new MechTask();
            foreach (MechTask x in NewPartsOrder)
            {
                if (x.PartID == CarParts && x.TaskId == Tasks)
                {
                    x.Qty = Qty;

                    return RedirectToAction("Create");
                }
            }
            newOrder.PartID = CarParts;
            newOrder.PartName = db.CAR_PARTS.Where(z => z.CARPARTS_ID == CarParts).FirstOrDefault().PARTNAME;
            newOrder.Qty = Qty;
            newOrder.TaskId = Tasks;
            newOrder.TaskName = db.TASKs.Where(z => z.SERVICE_ID == Tasks).FirstOrDefault().SERVICE_NAME;
            NewPartsOrder.Add(newOrder);
            ViewBag.TaskList = NewPartsOrder;
            return RedirectToAction("Create");

        }

        [HttpPost]
        public ActionResult RemoveFromList(int id, int idt)
        {
            try
            {
                foreach (MechTask x in NewPartsOrder)
                {
                    if (x.PartID == id && x.TaskId == idt)
                    {
                        NewPartsOrder.Remove(x);
                        return RedirectToAction("Create");

                    }
                }
                TempData["AlertMessage"] = "Your item has been removed!";
                return RedirectToAction("Create");

            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create");

            }

        }
         }
}
