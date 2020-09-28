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
                              .Include(c => c.MODEL.MAKE)

                              .Where(cc => cc.STATUS_ID == 2);
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
            ViewBag.Makes = new SelectList(db.MAKEs, "MAKE_ID", "MAKE_NAME");
            ViewBag.Models = new SelectList(db.MODELs, "MODEL_ID", "MODEL_NAME");
            ViewBag.BodyTypes = new SelectList(db.CAR_TYPE, "CAR_TYPEID", "TYPE_NAME");

            return View(carlist);
        }
        [HttpPost]
        public ActionResult Search(int Makes, int Colours, int BodyTypes, int Milage)
        {
            carlist = db.CARS.Include(zz => zz.MODEL).Where(zz => zz.MODEL.MAKE_ID == Makes && zz.COLOUR_ID == Colours && zz.CAR_TYPE.CAR_TYPEID == BodyTypes && zz.MILAGE_ <= Milage).ToList();
            return RedirectToAction("IndexSearch");
        }

        public ActionResult IndexSearchStatus()
        {
            ViewBag.status = new SelectList(db.CAR_STATUS, "STATUS_ID", "STATUS");

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

                                 .Where(cc => cc.STATUS_ID == 4);
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
            OFFER x = db.OFFERS.Where(zz => zz.CAR_ID == c && (zz.STATUS_ID == 1 || zz.STATUS_ID == 3)).FirstOrDefault();
            if (x != null)
            {
                return RedirectToAction("Index");
            }
            else
            {


                OFFER o = new OFFER();

                o.AMOUNT = offer;
                o.CAR_ID = c;
                o.OFFERTYPE_ID = type;
                o.STATUS_ID = 3;
                db.OFFERS.Add(o);
                db.SaveChanges();
                CAR of = db.CARS.Where(zz => zz.CAR_ID == c).First();
                var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                string email = db.USERs.Where(zz => zz.USER_ID == of.USER_ID).First().EMAIL;
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "Ivtinocana";
                var sub = "Accepted Offer";
                var body = "We have viewed your car  with car registration : " + of.CAR_REG +"Car details: "+of.MODEL.MAKE.MAKE_NAME+" "+of.MODEL.MODEL_NAME + "and would like to offer R "+offer + " for the car. please log onto your Velhution profile to confirm or reject the offer." ;
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

            return RedirectToAction("PendingCarIndex");
        }

        [HttpPost]
        public ActionResult MakeOfferC(int c, float offer, int type)
        {
            OFFER o = new OFFER();

            o.AMOUNT = offer;
            o.CAR_ID = c;
            o.OFFERTYPE_ID = type;
            o.STATUS_ID = 3;
            db.OFFERS.Add(o);
            db.SaveChanges();

            return RedirectToAction("ViewCarsForSaleIndex");
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
            CAR cAR = db.CARS.Find(id);

            //Send email to user saying car was rejected 
            if (cAR.USER_ID != null)
            {
                var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                string email = db.USERs.Where(zz => zz.USER_ID == cAR.USER_ID).First().EMAIL;
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "Ivtinocana";
                var sub = "Car Uploaded to Vehlution";
                var body = "This email is about your car, " +
                    "\n Car ID: " + cAR.CAR_ID + " " +
                    "\n Car registration : " + cAR.CAR_REG + " " +
                    "\n We are not interested in buying your car at this moment in time " +
                    "\n If you want some more information people contact us on 059623015" +
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
            if(oFFER != null)
            {
                db.OFFERS.Remove(oFFER);
                db.SaveChanges();
            }
            
            db.CARS.Remove(cAR);
            db.SaveChanges();

            return RedirectToAction("PendingCarIndex");
        }
    }
}