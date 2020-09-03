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
    public class CARsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        static public List<CAR> carlist = new List<CAR>();
        // GET: CARs
        public ActionResult Index()
        {
            ViewBag.Makes = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            return View(carlist);
        }
        [HttpPost]
        public ActionResult Search(int Makes)
        {
            carlist = db.CARS.Where(zz => zz.MAKE_ID == Makes).ToList();
            
            return RedirectToAction("Index");
        }
        // GET: CARs/Details/5
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

        // GET: CARs/Create
        public ActionResult Create()
        {
            ViewBag.BOO_BOOKING_ID = new SelectList(db.BOOKING_FOR_POSSIBLE_PURCHASE, "BOOKING_ID", "BOOKING_STATUS");
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "CARBOOKING_ID", "STATUS");
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME");
            ViewBag.PURCHASE_ID = new SelectList(db.PURCHASES, "PURCHASE_ID", "PURCHASE_ID");
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME");
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME");
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME");
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS");
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS");
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME");
            return View();
        }

        // POST: CARs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CAR_ID,SEATS_ID,COLOUR_ID,TRANSMISSION_ID,DOORS_ID,MAKE_ID,CLIENT_ID,STATUS_ID,BOOKING_ID,PURCHASE_ID,FUELTYPE_ID,BOO_BOOKING_ID,YEAR,MILAGE_,LISTING_PRICE,IMAGE")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                db.CARS.Add(cAR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BOO_BOOKING_ID = new SelectList(db.BOOKING_FOR_POSSIBLE_PURCHASE, "BOOKING_ID", "BOOKING_STATUS", cAR.BOO_BOOKING_ID);
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "CARBOOKING_ID", "STATUS", cAR.BOOKING_ID);
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.PURCHASE_ID = new SelectList(db.PURCHASES, "PURCHASE_ID", "PURCHASE_ID", cAR.PURCHASE_ID);
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME", cAR.MAKE_ID);
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME", cAR.CLIENT_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "DOORS_ID", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "SEATS_ID", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // GET: CARs/Edit/5
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
           
            ViewBag.BOO_BOOKING_ID = new SelectList(db.BOOKING_FOR_POSSIBLE_PURCHASE, "BOOKING_ID", "BOOKING_STATUS", cAR.BOO_BOOKING_ID);
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "CARBOOKING_ID", "STATUS", cAR.BOOKING_ID);
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.PURCHASE_ID = new SelectList(db.PURCHASES, "PURCHASE_ID", "PURCHASE_ID", cAR.PURCHASE_ID);
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME", cAR.MAKE_ID);
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME", cAR.CLIENT_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // POST: CARs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CAR_ID,SEATS_ID,COLOUR_ID,TRANSMISSION_ID,DOORS_ID,MAKE_ID,CLIENT_ID,STATUS_ID,BOOKING_ID,PURCHASE_ID,FUELTYPE_ID,BOO_BOOKING_ID,YEAR,MILAGE_,LISTING_PRICE,IMAGE")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BOO_BOOKING_ID = new SelectList(db.BOOKING_FOR_POSSIBLE_PURCHASE, "BOOKING_ID", "BOOKING_STATUS", cAR.BOO_BOOKING_ID);
            ViewBag.BOOKING_ID = new SelectList(db.CAR_BOOKING, "CARBOOKING_ID", "STATUS", cAR.BOOKING_ID);
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.PURCHASE_ID = new SelectList(db.PURCHASES, "PURCHASE_ID", "PURCHASE_ID", cAR.PURCHASE_ID);
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME", cAR.MAKE_ID);
            ViewBag.CLIENT_ID = new SelectList(db.CLIENTs, "CLIENT_ID", "USER_NAME", cAR.CLIENT_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "DOORS_ID", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "SEATS_ID", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // GET: CARs/Delete/5
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

        // POST: CARs/Delete/5
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
