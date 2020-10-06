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

        public ActionResult ViewCarsForSaleIndexAdmin()
        {
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
        public ActionResult Search( int MODEL_ID, int BodyTypes, int Milage)
        {
            carlist = db.CARS.Include(zz => zz.MODEL).Where(zz => zz.MODEL.MODEL_ID == MODEL_ID  && zz.CAR_TYPE.CAR_TYPEID == BodyTypes && zz.MILAGE_ <= Milage).ToList();
            return RedirectToAction("IndexSearch");
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
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                                 .Include(c => c.MODEL)
                                 .Include(c => c.COLOUR)
                                 .Include(c => c.FUEL_TYPE)
                                 .Include(c => c.NUMBER_OF_DOORS)
                                 .Include(c => c.NUMBER_OF_SEATS)
                                 .Include(c => c.TRANSMISSION)
                                 .Include(c => c.MODEL.MAKE)

                                 .Where(cc => cc.STATUS_ID == 3);
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
                }
                TempData["AlertMessage"] = "Your offer has successfully been placed ";
                return RedirectToAction("ViewCarsForSaleIndex");
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
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME", cAR.CAR_TYPEID);
            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME", cAR.MODEL_ID);
            ViewBag.USER_ID = new SelectList(db.USERs, "USER_ID", "FIRSTNAME", cAR.USER_ID);
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
        public ActionResult Edit([Bind(Include = "CAR_REG,SEATS_ID,COLOUR_ID,TRANSMISSION_ID,DOORS_ID,CLIENT_ID,STATUS_ID,FUELTYPE_ID,MODEL_ID,YEAR,MILAGE_,LISTING_PRICE,IMAGE,CAR_ID,CAR_TYPEID")] CAR cAR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("AdminCarsForSale");
            }
            ViewBag.STATUS_ID = new SelectList(db.CAR_STATUS, "STATUS_ID", "SASTUS_NAME", cAR.STATUS_ID);
            ViewBag.CAR_TYPEID = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME", cAR.CAR_TYPEID);
            ViewBag.MODEL_ID = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME", cAR.MODEL_ID);
            ViewBag.CLIENT_ID = new SelectList(db.USERs, "CLIENT_ID", "USER_NAME", cAR.USER_ID);
            ViewBag.COLOUR_ID = new SelectList(db.COLOURs, "COLOUR_ID", "COLOUR_NAME", cAR.COLOUR_ID);
            ViewBag.FUELTYPE_ID = new SelectList(db.FUEL_TYPE, "FUELTYPE_ID", "FUELTYPE_NAME", cAR.FUELTYPE_ID);
            ViewBag.DOORS_ID = new SelectList(db.NUMBER_OF_DOORS, "DOORS_ID", "NUMBER_OF_DOORS1", cAR.DOORS_ID);
            ViewBag.SEATS_ID = new SelectList(db.NUMBER_OF_SEATS, "SEATS_ID", "NUMBER_OF_SEATS_", cAR.SEATS_ID);
            ViewBag.TRANSMISSION_ID = new SelectList(db.TRANSMISSIONs, "TRANSMISSION_ID", "TRANSMISSION_NAME", cAR.TRANSMISSION_ID);
            return View(cAR);
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

        // POST: AdminCARs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
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


                OFFER oFFER = db.OFFERS.Where(m => m.CAR_ID == id).FirstOrDefault();
                if (oFFER != null)
                {
                    db.OFFERS.Remove(oFFER);
                    db.SaveChanges();
                }

                db.CARS.Remove(cAR);
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
    }
}