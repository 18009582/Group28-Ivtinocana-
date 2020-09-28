using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq.Expressions;

namespace Vehlution_Everything_.Controllers
{
    public class ADMINCARsController : Controller
    {
        VehlutionEntities db = new VehlutionEntities();
        // GET: ADMINCARs
        public ActionResult Index()
        {
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                .Include(c => c.MODEL.MAKE)
                .Include(c => c.USER)
                .Include(c => c.COLOUR)
                .Include(c => c.FUEL_TYPE)
                .Include(c => c.NUMBER_OF_DOORS)
                .Include(c => c.NUMBER_OF_SEATS)
                .Include(c => c.TRANSMISSION);
            return View(cARS.ToList());
        }

        // GET: AdminCARs/Details/5
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

        // GET: AdminCARs/Create
        public ActionResult Create()
        {
           

            
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.USER_ID = new SelectList(db.USERs, "USER_ID", "FIRSTNAME");
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME");
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME");
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1");
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_");
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME");
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");
            //var carmake = db.CARS.ToList();
            return View();
        }

        // POST: ClientCARs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string CarReg, int SEATS_ID, int COLOUR_ID, int TRANSMISSION_ID, int DOORS_ID, int FUELTYPE_ID, int CAR_TYPEID, int MODEL_ID, int YEAR, int MILAGE_, float LISTING_PRICE, HttpPostedFileBase file)
        {
                if (ModelState.IsValid)
                {
                    CAR cAR = new CAR();
                    string pic = null;
                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/"), pic);
                        file.SaveAs(path);
                        cAR.IMAGE = pic;

                    }


                    cAR.CAR_REG = CarReg;
                    cAR.SEATS_ID = SEATS_ID;
                    cAR.CAR_TYPEID = CAR_TYPEID;
                    cAR.COLOUR_ID = COLOUR_ID;
                    cAR.TRANSMISSION_ID = TRANSMISSION_ID;
                    cAR.DOORS_ID = DOORS_ID;
                    cAR.MODEL_ID = MODEL_ID;
                    cAR.STATUS_ID = 2;
                    cAR.FUELTYPE_ID = FUELTYPE_ID;
                    cAR.YEAR = YEAR;
                    cAR.MILAGE_ = MILAGE_;
                    cAR.LISTING_PRICE = LISTING_PRICE;
                    int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
                    cAR.USER_ID = clientid;
                    db.CARS.Add(cAR);
                    db.SaveChanges();
                    TempData["Message"] = "Your car has been Saved Successfully! ";
                    return RedirectToAction("Create");
                }


            return RedirectToAction("Create");
        }

        //Action result for ajax call
        [HttpPost]
        public ActionResult GetModelByMakeId(int makeid)
        {
            List<MODEL> objmodel = new List<MODEL>();
            objmodel = db.MODELs.Where(m => m.MAKE_ID == makeid).ToList();
            SelectList obgmodel = new SelectList(objmodel, "MODEL_ID", "MODEL_NAME", 0);
            return Json(obgmodel);
        }

        // GET: AdminCARs/Edit/5
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
            ViewBag.CLIENT_ID = new SelectList(db.USERs, "CLIENT_ID", "USER_NAME", cAR.USER_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "DOORS_ID", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "SEATS_ID", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // POST: AdminCARs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CAR_ID,SEATS_ID,COLOUR_ID,TRANSMISSION_ID,DOORS_ID,MAKE_ID,CLIENT_ID,STATUS_ID,FUELTYPE_ID,MODEL_ID,YEAR,MILAGE_,LISTING_PRICE,IMAGE")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME", cAR.MODEL_ID);
            ViewBag.CLIENT_ID = new SelectList(db.USERs, "CLIENT_ID", "USER_NAME", cAR.USER_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "DOORS_ID", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "SEATS_ID", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // GET: AdminCARs/Delete/5
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

        // POST: AdminCARs/Delete/5
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

    
