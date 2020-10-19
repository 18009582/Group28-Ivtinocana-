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

        static public List<NewDefect> NewDefects = new List<NewDefect>();
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
            return View();
        }

        // POST: ClientCARs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( string CarReg, int SEATS_ID,int COLOUR_ID, int TRANSMISSION_ID, int DOORS_ID,  int FUELTYPE_ID, int CAR_TYPEID, int MODEL_ID, int YEAR, int MILAGE_, double LISTING_PRICE, HttpPostedFileBase file)
        {
           try
           {

                if (ModelState.IsValid)
                {

                    List<CAR> car = new List<CAR>();
                    car = db.CARS.ToList();
                    foreach (var item in car)
                    {
                        if (item.CAR_REG == CarReg)
                        {
                            TempData["AlertMessage"] = "This car registration already exists in our system";
                            return RedirectToAction("Create", "CLIENTCARs");
                        }
                    }

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
                    cAR.STATUS_ID = 3;
                    cAR.FUELTYPE_ID = FUELTYPE_ID;
                    cAR.YEAR = YEAR;
                    cAR.MILAGE_ = MILAGE_;
                    cAR.LISTING_PRICE = Convert.ToInt32(LISTING_PRICE);
                    int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
                    cAR.USER_ID = clientid;
                    db.CARS.Add(cAR);

                    db.SaveChanges();
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
                    return RedirectToAction("ClientNav", "Nav");
                }
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("Create", "CLIENTCARs");
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
            return RedirectToAction("Create", "CLIENTCARs");
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
        public ActionResult AddToList(int DEFECTS, short severity, int CarParts)
        {
            NewDefect dEFECT = new NewDefect();
            foreach (NewDefect x in NewDefects)
            {
                if (x.PartID == CarParts && x.defid == DEFECTS)
                {
                    x.defid = DEFECTS;
                    x.severity = severity;
                    return RedirectToAction("Create", "CLIENTCARs");
                }
            }
            dEFECT.PartID = CarParts;
            dEFECT.PartName = db.CAR_PARTS.Where(z => z.CARPARTS_ID == CarParts).FirstOrDefault().PARTNAME;
            dEFECT.defid = DEFECTS;
            dEFECT.defname = db.DEFECTS.Where(z => z.DEFECT_ID == DEFECTS).FirstOrDefault().DEFECTNAME;
            dEFECT.severity = severity;
            NewDefects.Add(dEFECT);
            ViewBag.PartsOrdered = NewDefects;
            return RedirectToAction("Create", "CLIENTCARs");

        }

        [HttpPost]
        public ActionResult RemoveFromList(int id, int partid)
        {

            foreach (NewDefect x in NewDefects)
            {
                if (x.PartID == partid && x.defid == id)
                {
                    NewDefects.Remove(x);
                    return RedirectToAction("Create", "CLIENTCARs");

                }
            }

            return RedirectToAction("Create", "CLIENTCARs");
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
