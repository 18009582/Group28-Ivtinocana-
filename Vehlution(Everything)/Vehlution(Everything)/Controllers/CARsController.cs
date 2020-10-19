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
using System.Net.Mail;

namespace Vehlution_Everything_.Controllers
{
    public class CARsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        public static List<CAR> carlist = new List<CAR>();
        public static List<CAR> carlists = new List<CAR>();
        // GET: CARs for sale
        public ActionResult ViewCarsForSaleIndex()
        {

            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.Models = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME");
            ViewBag.BodyTypes = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                              .Include(c => c.MODEL)
                              .Include(c => c.COLOUR)
                              .Include(c => c.FUEL_TYPE)
                              .Include(c => c.NUMBER_OF_DOORS)
                              .Include(c => c.NUMBER_OF_SEATS)
                              .Include(c => c.TRANSMISSION)
                              .Include(c => c.MODEL.MAKE)
                              .Where(cc => cc.STATUS_ID == 2);

            if (carlist.Count()==0)
            {
                carlist = cARS.ToList();
            }
            return View(carlist);
        }

        public ActionResult ViewCarsForSaleIndexAdmin()
        {
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.Models = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME");
            ViewBag.BodyTypes = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                               .Include(c => c.MODEL)
                               .Include(c => c.COLOUR)
                               .Include(c => c.FUEL_TYPE)
                               .Include(c => c.NUMBER_OF_DOORS)
                               .Include(c => c.NUMBER_OF_SEATS)
                               .Include(c => c.TRANSMISSION)
                               .Include(c => c.MODEL.MAKE).ToList();
            return View(cARS.ToList());
        }

        public ActionResult AdminCarsForSale()
        {
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                              .Include(c => c.MODEL)
                              .Include(c => c.COLOUR)
                              .Include(c => c.FUEL_TYPE)
                              .Include(c => c.NUMBER_OF_DOORS)
                              .Include(c => c.NUMBER_OF_SEATS)
                              .Include(c => c.TRANSMISSION)
                              .Include(c => c.MODEL.MAKE)

                              .Where(cc => cc.STATUS_ID == 2);
            return View(cARS.ToList());
        }

        public ActionResult IndexSearch()
        {
            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.Models = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME");
            ViewBag.BodyTypes = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");

            return View(carlist);
        }
        [HttpPost]
        public ActionResult Search(int MODEL_ID, int BodyTypes, int Milage)
        {
            carlist = db.CARS.Include(zz => zz.MODEL).Where(zz => zz.MODEL.MODEL_ID == MODEL_ID && zz.CAR_TYPE.CAR_TYPEID == BodyTypes && zz.MILAGE_ <= Milage).ToList();
            if(carlist.Count==0)
            {
                TempData["AlertMessage"] = "No cars matching the criteria were found";
            }
            return RedirectToAction("ViewCarsForSaleIndex");
        }

        public ActionResult IndexSearchStatus()
        {
            ViewBag.status = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME");

            return View(carlists);
        }
        [HttpPost]
        public ActionResult SearchStatus(int Status)
        {
            carlists = db.CARS.Include(zz => zz.MODEL).Where(zz => zz.CAR_STATUS.STATUS_ID == Status).ToList();

            return RedirectToAction("IndexSearchStatus");
        }
        //GET: CARs that are pending
        public ActionResult PendingCarIndex()
        {
            var cARS = db.CARS.Where(cc => cc.STATUS_ID == 3);
            return View(cARS.ToList());
        }

        // GET: CARs/Details/5
        public ActionResult SaleDetails(int? id)
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

        public ActionResult SaleDetailsAdmin(int? id)
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

        public ActionResult PendingDetails(int? id)
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

        [HttpPost]
        public ActionResult MakeOfferA(int c, float offer, int type)
        {
            try
            {
                OFFER x = db.OFFERS.Where(zz => zz.CAR_ID == c && (zz.STATUS_ID == 1 || zz.STATUS_ID == 3) && zz.OFFERTYPE_ID == type).FirstOrDefault();
                if (x != null)
                {
                    TempData["AlertMessage"] = "You have already made an offer on this car";
                    return RedirectToAction("PendingCarIndex");
                }
                else
                {
                    OFFER o = new OFFER();

                    o.AMOUNT = offer;
                    o.CAR_ID = c;
                    o.OFFERTYPE_ID = type;
                    o.STATUS_ID = 3;
                    int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
                    o.USER_ID = clientid;
                    db.OFFERS.Add(o);
                    db.SaveChanges();
                    CAR of = db.CARS.Where(zz => zz.CAR_ID == c).First();
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    string email = db.USERs.Where(zz => zz.USER_ID == of.USER_ID).First().EMAIL;
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Accepted Offer";
                    var body = "Good day " + o.CAR.USER.FIRSTNAME +
                            "\n We hope you are doing well, we have just made an offer on the following car which you have put up on our website" +
                            "\n Car Registration : " + o.CAR.CAR_REG + " " +
                            "\n Car Details: " + o.CAR.MODEL.MAKE.MAKE_NAME + " " + o.CAR.MODEL.MODEL_NAME +
                            "\n Colour: " + o.CAR.COLOUR.COLOUR_NAME +
                            "\n Our Offer Price: R " + o.AMOUNT + "  " +
                            "\n Please login on our website with your details and review our offer we have just made as well as accept of reject our offer" +
                            "\n Yours Faithfully, " +
                            "\n Vehlution ";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }
                TempData["AlertMessage"] = "Your offer has successfully been placed ";
                return RedirectToAction("PendingCarIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("PendingCarIndex");
            }

        }

        [HttpPost]
        public ActionResult MakeOfferC(int c, float offer, int type)
        {
            try
            {
                int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);

                OFFER x = db.OFFERS.Where(zz => zz.CAR_ID == c && (zz.STATUS_ID == 1 || zz.STATUS_ID == 3) && zz.USER_ID == clientid && zz.OFFERTYPE_ID == type).FirstOrDefault();
                if (x != null)
                {
                    TempData["AlertMessage"] = "You have already made an offer on this car";
                    return RedirectToAction("ViewCarsForSaleIndex");
                }
                else
                {
                    OFFER o = new OFFER();
                    o.USER_ID = clientid;
                    o.AMOUNT = offer;
                    o.CAR_ID = c;
                    o.OFFERTYPE_ID = type;
                    o.STATUS_ID = 3;
                    db.OFFERS.Add(o);
                    db.SaveChanges();

                    if (o.USER_ID != 1)
                    {

                        var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                        var receiverEmail = new MailAddress("vehlutionReceive@gmail.com", "Receiver");
                        var password = "Ivtinocana";
                        var sub = "Accepted Offer";
                        var body = "Good day " + o.CAR.USER.FIRSTNAME +
                             "\n I hope you are doing well, I have made an offer on the following car which is on your website: " +
                             "\n Car Registration : " + o.CAR.CAR_REG + " " +
                             "\n Car Details: " + o.CAR.MODEL.MAKE.MAKE_NAME + " " + o.CAR.MODEL.MODEL_NAME +
                             "\n Colour: " + o.CAR.COLOUR.COLOUR_NAME +
                             "\n Price: R " + o.AMOUNT + "  " +
                             "\n Please login on the website to review this offer and make a decision" +
                             "\n Please contact me via email for more details: " + o.CAR.USER.EMAIL +
                             "\n Yours Faithfully, " +
                             "\n" + o.USER.FIRSTNAME + o.USER.LASTNAME;
                        var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = sub,
                            Body = body
                        })
                        {
                            smtp.Send(mess);
                        }
                        
                    }
                    TempData["AlertMessage"] = "Your offer has successfully been placed ";
                    return RedirectToAction("ViewCarsForSaleIndex");
                }

                
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("ViewCarsForSaleIndex");
            }

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

            ViewBag.MAKE_ID = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");

            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME");
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME", cAR.CAR_TYPEID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
        }


        //carlogs
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



        // POST: CARs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
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
                    
                //    int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
                //    cAR.USER_ID = clientid;
                    db.SaveChanges();
                    AddCarLogs(cAR, "UPDATE");

                    TempData["AlertMessage"] = "Your car has successfully been updated";
                    return RedirectToAction("IndexSearchStatus", "CARs");
                }
            }
            catch 
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " ;
                return RedirectToAction("IndexSearchStatus", "CARs");
            }
            return RedirectToAction("IndexSearchStatus", "CARs");

        } 


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
        //CArLOGS

        // POST: AdminCARs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Where(m => m.CAR_ID == id).FirstOrDefault();
                if (oFFER != null)
                {
                    TempData["AlertMessage"] = "You can't delete this car because it is linked to a offer in the Offers table";
                    return RedirectToAction("PendingCarIndex");
                }

                CAR cAR = db.CARS.Find(id);

                //Send email to user saying car was rejected 
                if (cAR.USER_ID != null)
                {
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    string email = db.USERs.Where(zz => zz.USER_ID == cAR.USER_ID).First().EMAIL;
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Car Uploaded to Vehlution";
                    var body = "This email is about the follwoing car which you have placed on our website for sale, " +
                        "\n Car ID: " + cAR.CAR_ID + " " +
                        "\n Car Registration : " + cAR.CAR_REG + " " +
                        "\n Car Details: " + cAR.MODEL.MAKE.MAKE_NAME + " " + cAR.MODEL.MODEL_NAME +
                        "\n Colour: " + cAR.COLOUR.COLOUR_NAME +
                        "\n We are not interested in buying your car at this moment in time, so it will be taken of our system" +
                        "\n If you want some more information please contact us on 059623015" +
                        "\n Yours Faithfully , " +
                        "\n Vehlution ";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                }

                db.CARS.Remove(cAR);
                AddCarLogs(cAR, "DELETE");
                db.SaveChanges();
               

                TempData["AlertMessage"] = "This car successfully been deleted from our system";
                return RedirectToAction("PendingCarIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("PendingCarIndex");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSeats(int NUMBER_OF_SEATS_)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<NUMBER_OF_SEATS> nUMBER_OF_SEATs = new List<NUMBER_OF_SEATS>();
                    nUMBER_OF_SEATs = db.NUMBER_OF_SEATS.ToList();
                    NUMBER_OF_SEATS n = new NUMBER_OF_SEATS();
                    n.NUMBER_OF_SEATS_ = NUMBER_OF_SEATS_;
                    foreach (var item in nUMBER_OF_SEATs)
                    {
                        if (item.NUMBER_OF_SEATS_ == NUMBER_OF_SEATS_)
                        {
                            TempData["AlertMessage"] = "This number of seats already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

                    db.NUMBER_OF_SEATS.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A new number of seats has successfully been added!";
                    return RedirectToAction("Create", "ADMINCARs");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create", "ADMINCARs");
            }


            return View(NUMBER_OF_SEATS_);
        }

        public ActionResult CreateDoors(int NUMBER_OF_DOORS1)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<NUMBER_OF_DOORS> nUMBER_OF_DOORs = new List<NUMBER_OF_DOORS>();
                    nUMBER_OF_DOORs = db.NUMBER_OF_DOORS.ToList();
                    NUMBER_OF_DOORS n = new NUMBER_OF_DOORS();
                    n.NUMBER_OF_DOORS1 = NUMBER_OF_DOORS1;
                    foreach (var item in nUMBER_OF_DOORs)
                    {
                        if (item.NUMBER_OF_DOORS1 == NUMBER_OF_DOORS1)
                        {
                            TempData["AlertMessage"] = "This number of doors already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

                    db.NUMBER_OF_DOORS.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A new number of doors has successfully been added!";
                    return RedirectToAction("Create", "ADMINCARs");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create", "ADMINCARs");
            }


            return View(NUMBER_OF_DOORS1);
        }

        public ActionResult CreateTransmission(string TRANSMISSION_NAME )
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<TRANSMISSION> tRANSMISSIONs = new List<TRANSMISSION>();
                    tRANSMISSIONs = db.TRANSMISSIONs.ToList();
                    TRANSMISSION n = new TRANSMISSION();
                    n.TRANSMISSION_NAME = TRANSMISSION_NAME;
                    foreach (var item in tRANSMISSIONs)
                    {
                        if (item.TRANSMISSION_NAME.Trim() == TRANSMISSION_NAME.Trim())
                        {
                            TempData["AlertMessage"] = "This transmission already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

                    db.TRANSMISSIONs.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A new transmission has successfully been added!";
                    return RedirectToAction("Create", "ADMINCARs");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create", "ADMINCARs");
            }


            return View(TRANSMISSION_NAME);
        }

        public ActionResult CreateColour(string COLOUR_NAME)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    List<COLOUR> cOLOURs = new List<COLOUR>();
                    cOLOURs = db.COLOURs.ToList();
                    COLOUR n = new COLOUR();
                    n.COLOUR_NAME = COLOUR_NAME;
                    foreach (var item in cOLOURs)
                    {
                        if (item.COLOUR_NAME.Trim() == COLOUR_NAME.Trim())
                        {
                            TempData["AlertMessage"] = "This colour already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

                    db.COLOURs.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A new colour has successfully been added!";
                    return RedirectToAction("Create", "ADMINCARs");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create", "ADMINCARs");
            }

            return View(COLOUR_NAME);
        }

        public ActionResult CreateFuelType( string FUELTYPE_NAME)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<FUEL_TYPE> fUEL_TYPEs = new List<FUEL_TYPE>();
                    fUEL_TYPEs = db.FUEL_TYPE.ToList();
                    FUEL_TYPE n = new FUEL_TYPE();
                    n.FUELTYPE_NAME = FUELTYPE_NAME;
                    foreach (var item in fUEL_TYPEs)
                    {
                        if (item.FUELTYPE_NAME.Trim() == FUELTYPE_NAME.Trim())
                        {
                            TempData["AlertMessage"] = "This fuel type already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

                    db.FUEL_TYPE.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A new fuel type has successfully been added!";
                    return RedirectToAction("Create", "ADMINCARs");
                }
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create", "ADMINCARs");
            }

            return View(FUELTYPE_NAME);
        }

        public ActionResult CreateBodyType(string TYPE_NAME)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    List<CAR_TYPE> cAR_TYPEs = new List<CAR_TYPE>();
                    cAR_TYPEs = db.CAR_TYPE.ToList();
                    CAR_TYPE n = new CAR_TYPE();
                    n.TYPE_NAME = TYPE_NAME;
                    foreach (var item in cAR_TYPEs)
                    {
                        if (item.TYPE_NAME.Trim() == TYPE_NAME.Trim())
                        {
                            TempData["AlertMessage"] = "This car type already exists in our system";
                            return RedirectToAction("Create", "ADMINCARs");
                        }
                    }

                    db.CAR_TYPE.Add(n);
                    db.SaveChanges();
                    TempData["AlertMessage"] = "A new body type has successfully been added!";
                    return RedirectToAction("Create", "ADMINCARs");
                }
            }
            catch
            {
               TempData["AlertMessage"] = "Sorry something went wrong, please try again later";
                return RedirectToAction("Create", "ADMINCARs");
            }

            return View(TYPE_NAME);
        }

    }
}