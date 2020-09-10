using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using Vehlution_Everything_.ViewModel;

namespace Vehlution_Everything_.Controllers
{
    public class CLIENTCARsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: ClientCARs
        public ActionResult Index()
        {
            var cARS = db.CARS.Include(c => c.CAR_STATUS).Include(c => c.MODEL).Include(c => c.USER).Include(c => c.COLOUR).Include(c => c.FUEL_TYPE).Include(c => c.NUMBER_OF_DOORS).Include(c => c.NUMBER_OF_SEATS).Include(c => c.TRANSMISSION);
            return View(cARS.ToList());
        }

        // GET: ClientCARs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CARS.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
        }

        // GET: ClientCARs/Create
        public ActionResult Create()
        {
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME");
            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME");
            ViewBag.USER_ID = new SelectList(db.USERs, "USER_ID", "FIRSTNAME");
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME");
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME");
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1");
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_");
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME");
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");
            return View();
        }

        // POST: ClientCARs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( string CarReg, int SEATS_ID,int COLOUR_ID, int TRANSMISSION_ID, int DOORS_ID,  int FUELTYPE_ID, int CAR_TYPEID, int MODEL_ID, int YEAR, int MILAGE_, float LISTING_PRICE,HttpPostedFileBase IMAGE)
        {

            if (ModelState.IsValid)
            {
                string pic = null;
                if (IMAGE != null)
                {
                    pic = System.IO.Path.GetFileName(IMAGE.FileName);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Images/"), pic);
                    IMAGE.SaveAs(path);

                }
              
                UserInfo usr = new UserInfo();
                CAR cAR = new CAR();
                cAR.CAR_REG = CarReg;
                cAR.SEATS_ID = SEATS_ID;
                cAR.CAR_TYPEID = CAR_TYPEID;
                cAR.COLOUR_ID = COLOUR_ID;
                cAR.TRANSMISSION_ID = TRANSMISSION_ID;
                cAR.DOORS_ID = DOORS_ID;
                cAR.MODEL_ID = MODEL_ID;
                cAR.USER_ID = usr.UserID;
                cAR.STATUS_ID = 4;
                cAR.FUELTYPE_ID = FUELTYPE_ID;
                cAR.YEAR = YEAR;
                cAR.MILAGE_ = MILAGE_;
                cAR.LISTING_PRICE = LISTING_PRICE;
                db.CARS.Add(cAR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            //ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME", cAR.MODEL_ID);
            //ViewBag.USER_ID = new SelectList(db.USERs, "USER_ID", "FIRSTNAME", cAR.USER_ID);
            //ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            //ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            //ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1", cAR.DOORS_ID);
            //ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            //ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            // return View(cAR);
            return RedirectToAction("Index");
        }

        // GET: ClientCARs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CARS.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME", cAR.MODEL_ID);
            ViewBag.USER_ID = new SelectList(db.USERs, "USER_ID", "FIRSTNAME", cAR.USER_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // POST: ClientCARs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CAR_ID,SEATS_ID,COLOUR_ID,TRANSMISSION_ID,DOORS_ID,MAKE_ID,USER_ID,STATUS_ID,FUELTYPE_ID,MODEL_ID,YEAR,MILAGE_,LISTING_PRICE,IMAGE")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME", cAR.MODEL_ID);
            ViewBag.USER_ID = new SelectList(db.USERs, "USER_ID", "FIRSTNAME", cAR.USER_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // GET: ClientCARs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CARS.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
        }

        // POST: ClientCARs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAR cAR = db.CARS.Find(id);
            db.CARS.Remove(cAR);
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
