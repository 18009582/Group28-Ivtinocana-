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
       
        static public List<NewDefect> NewDefects = new List<NewDefect>();
       static public List<CAR_LOGS> cAR_LOGS = new List<CAR_LOGS>();
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

        private void AddCarLogs(CAR cr, string action)
        {
            cr = db.CARS.Find(cr.CAR_ID);
            CAR_LOGS cAR = new CAR_LOGS();


            cAR.IMAGE = cr.IMAGE;
            cAR.CAR_REG = cr.CAR_REG;
            cAR.CAR_ID = cr.CAR_ID;
            cAR.SEATS_ID = cr.SEATS_ID;
            var seat = db.NUMBER_OF_SEATS.FirstOrDefault(x => x.SEATS_ID == cAR.SEATS_ID);
            cAR.SEATS_NAME = seat == null ? string.Empty : seat.NUMBER_OF_SEATS_.ToString();
            cAR.CAR_TYPEID = cr.CAR_TYPEID;
            var carType = db.CAR_TYPE.FirstOrDefault(x => x.CAR_TYPEID == cr.CAR_TYPEID);
           cAR.CAR_TYPENAME = carType == null ? string.Empty : carType.TYPE_NAME.Trim();
            cAR.COLOUR_ID = cr.COLOUR_ID;
            var color = db.COLOURs.FirstOrDefault(x => x.COLOUR_ID == cr.COLOUR_ID);
           cAR.COLOUR_Name = color == null ? string.Empty : color.COLOUR_NAME.Trim();
            cAR.TRANSMISSION_ID = cr.TRANSMISSION_ID;
            var Transmission = db.TRANSMISSIONs.FirstOrDefault(x => x.TRANSMISSION_ID == cr.TRANSMISSION_ID);
            cAR.TRANSMISSION_NAME = Transmission == null ? string.Empty : Transmission.TRANSMISSION_NAME.Trim();
            cAR.DOORS_ID = cr.DOORS_ID;
            var cardoor = db.NUMBER_OF_DOORS.FirstOrDefault(x => x.DOORS_ID == cAR.DOORS_ID);
           cAR.DOORS_NAME = cardoor == null ? string.Empty : cardoor.NUMBER_OF_DOORS1.ToString();
            cAR.MODEL_ID = cr.MODEL_ID;
            int modelId = cr.MODEL_ID == null ? 0 : Convert.ToInt32(cr.MODEL_ID);
            var model = db.MODELs.FirstOrDefault(x => x.MODEL_ID == modelId);
            cAR.MODEL_NAME = model == null ? string.Empty : model.MODEL_NAME.Trim();
            cAR.STATUS_ID = cr.STATUS_ID = 2;
            var status = db.CAR_STATUS.FirstOrDefault(x => x.STATUS_ID == cr.STATUS_ID);
            cAR.STATUS_NAME = status == null ? string.Empty : status.SASTUS_NAME.Trim();
            cAR.FUELTYPE_ID = cr.FUELTYPE_ID;
            var fuelType = db.FUEL_TYPE.FirstOrDefault(x => x.FUELTYPE_ID == cr.FUELTYPE_ID);
           cAR.FUELTYPE_NAME = fuelType == null ? string.Empty : fuelType.FUELTYPE_NAME.Trim();
            cAR.YEAR = cr.YEAR;
            cAR.MILEAGE = cr.MILAGE_;
            cAR.LISTING_PRICE = cr.LISTING_PRICE;
            int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
            var user = db.USERs.FirstOrDefault(x => x.USER_ID == clientid);
            cAR.AUDITUSER = user == null ? string.Empty : user.FIRSTNAME.Trim() + " " + user.LASTNAME.Trim();
            cAR.USER_ID = clientid;
            cAR.AUDITACTION = action;
            cAR.AUDITDATE = DateTime.Now;
            db.CAR_LOGS.Add(cAR);
            db.SaveChanges();
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
            ViewBag.DEFECTS = new SelectList(db.DEFECTS, "DEFECT_ID", "DEFECTNAME");
            ViewBag.CarParts = new SelectList(db.PART_OF_CAR, "PARTOFCAR_ID", "PARTOFCAR_NAME");
            List<CAR_PARTS_ORDERED> PartsOrdered = db.CAR_PARTS_ORDERED.ToList();
            ViewBag.Defectslist = NewDefects;
            //var carmake = db.CARS.ToList();
            return View();
        }
        [HttpPost]
        public ActionResult AddToList(int DEFECTS, short severity, int CarParts)
        {
            NewDefect dEFECT = new NewDefect();
            foreach (NewDefect x in NewDefects)
            {
                if (x.PartID == CarParts && x.defid==DEFECTS)
                {
                    x.defid = DEFECTS;
                    x.severity = severity;
                    return RedirectToAction("Create");
                }
            }
            dEFECT.PartID = CarParts;
            dEFECT.PartName = db.CAR_PARTS.Where(z => z.CARPARTS_ID == CarParts).FirstOrDefault().PARTNAME;
            dEFECT.defid = DEFECTS;
            dEFECT.defname = db.DEFECTS.Where(z => z.DEFECT_ID == DEFECTS).FirstOrDefault().DEFECTNAME;
            dEFECT.severity = severity;
            NewDefects.Add(dEFECT);
            ViewBag.PartsOrdered = NewDefects;
            return RedirectToAction("Create");

        }

        [HttpPost]
        public ActionResult RemoveFromList(int id,int partid)
        {

            foreach (NewDefect x in NewDefects)
            {
                if (x.PartID == partid && x.defid == id)
                {
                    NewDefects.Remove(x);
                    return RedirectToAction("Create");

                }
            }
              
                return RedirectToAction("Create");
        }
     
        // POST: ClientCARs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string CarReg, int SEATS_ID, int COLOUR_ID, int TRANSMISSION_ID, int DOORS_ID, int FUELTYPE_ID, int CAR_TYPEID, int MODEL_ID, int YEAR, int MILAGE_, float LISTING_PRICE, HttpPostedFileBase file)
        {
            try {

                if (ModelState.IsValid)
                {
                    CAR cAR = new CAR();

                    List<CAR> car = new List<CAR>();
                    car = db.CARS.ToList();
                    foreach(var item in car)
                    {
                        if(item.CAR_REG == CarReg)
                        {
                            TempData["AlertMessage"] = "This car registration already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

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
                    cAR.LISTING_PRICE = Convert.ToInt32(LISTING_PRICE);
                    int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
                    cAR.USER_ID = clientid;
                    db.CARS.Add(cAR);
                    db.SaveChanges();
                    AddCarLogs(cAR, "INSERT");
                    foreach (NewDefect x in NewDefects)
                    {
                        CAR_DEFECTS c = new CAR_DEFECTS();
                        c.DEFECT_ID = x.defid;
                        c.CAR_ID = db.CARS.ToList().Last().CAR_ID;
                        c.SEVERITY = x.severity;
                        c.CARDEFECTID = 1;
                   
                        c.PARTOFCAR_ID = x.PartID;
                        db.CAR_DEFECTS.Add(c);
                      
                        db.SaveChanges();
                    }
                    
                    TempData["AlertMessage"] = "Your car has successfully been added";
                    return RedirectToAction("AdminNav", "Nav");
                }
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminNav", "Nav");
           }
            return RedirectToAction("Create", "ADMINCARs");

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

            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME", cAR.CAR_TYPEID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }

        // POST: AdminCARs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string CarReg, int SEATS_ID, int COLOUR_ID, int TRANSMISSION_ID, int DOORS_ID, int FUELTYPE_ID, int CAR_TYPEID, int MODEL_ID, int YEAR, int MILAGE_, float LISTING_PRICE, HttpPostedFileBase file)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    CAR cAR = new CAR();
                    cAR = db.CARS.Where(zz => zz.CAR_REG == CarReg).First();
                    string pic = null;

                    if (file != null)
                    {
                        pic = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Images/"), pic);
                        file.SaveAs(path);
                        cAR.IMAGE = pic;

                    }


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
                    cAR.LISTING_PRICE = Convert.ToInt32(LISTING_PRICE);
                    AddCarLogs(cAR, "UPDATE");
                    //    int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
                    //    cAR.USER_ID = clientid;
                    db.SaveChanges();

                    TempData["AlertMessage"] = "Your car has successfully been updated";
                    return RedirectToAction("IndexSearchStatus", "CARs");
                }
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("IndexSearchStatus", "CARs");
            }
            return RedirectToAction("IndexSearchStatus", "ADMINCARs");

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
            AddCarLogs(cAR, "DELETE");
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

       
        public ActionResult CarLigs()
        {
            if(cAR_LOGS.Count==0)
            {
                cAR_LOGS = db.CAR_LOGS.ToList();
            }
           
            return View(cAR_LOGS);

        }

        [HttpPost]
        public ActionResult SearchDate(DateTime StartDate, DateTime EndDate)
        {
            cAR_LOGS = db.CAR_LOGS.Where(zz => zz.AUDITDATE >= StartDate && zz.AUDITDATE <= EndDate).ToList();
            if (cAR_LOGS.Count == 0)
            {
                TempData["AlertMessage"] = "No logs found between "+StartDate+" and "+EndDate;
            }
            return RedirectToAction("CarLigs", cAR_LOGS);
        }
    }
}

    
