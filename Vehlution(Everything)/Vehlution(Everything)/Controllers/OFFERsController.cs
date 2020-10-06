using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using System.Net.Mail;
using Microsoft.AspNet.Identity;

namespace Vehlution_Everything_.Controllers
{
    public class OFFERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: OFFERs
        public ActionResult AdminIndex()
        {
            //where pending //where type = 1 client 3=admin
            var oFFERS = db.OFFERS.Where(zz => zz.STATUS_ID == 3 && zz.OFFERTYPE_ID == 1).Include(o => o.CAR).Include(o => o.USER).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE);
            return View(oFFERS.ToList());
        }
        //need to take in client id as a parameter
        public ActionResult ClientIndex()
        {
            //where pending //where type = 1 client 2=admin
            int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
            var oFFERS = db.OFFERS.Include(o => o.CAR)
                .Include(o => o.USER)
                .Include(o => o.OFFER_STATUS)
                .Include(o => o.OFFER_TYPE)
                .Where(zz => zz.STATUS_ID == 3 && zz.CAR.USER_ID == clientid && zz.OFFER_TYPE.OFFERTYPE_ID == 2);

            return View(oFFERS.ToList());
        }

        public ActionResult AcceptA(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 1;
                db.SaveChanges();
                if (oFFER.USER_ID != null)
                {
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    string email = db.USERs.Where(zz => zz.USER_ID == oFFER.USER_ID).First().EMAIL;
                    var receiverEmail = new MailAddress(email, "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Accepted Offer";
                    var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                        "\n We hope you are doing well, you have make the following offer on one of our cars." +
                        "\n Offer ID: " + oFFER.OFFER_ID + " " +
                        "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                        "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                        "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME + 
                        "\n Price: R " + oFFER.AMOUNT + "  " +
                        "\n This offer has been accepted! " +
                        "\n Please click on this link to download the car contract" +
                        "\n https://localhost:44348/DownloadContract/Download" +
                        "\n Please contact your salesperson if you have any questions" +
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
                TempData["AlertMessage"] = "You have just accepted this offer";
                return RedirectToAction("AdminIndex");
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
            }
            
        }
        public ActionResult RejectA(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 2;
                db.SaveChanges();
                if (oFFER.USER_ID != null)
                {
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    if (oFFER.USER_ID != null)
                    {
                        string email = db.USERs.Where(zz => zz.USER_ID == oFFER.USER_ID).First().EMAIL;
                        var receiverEmail = new MailAddress(email, "Receiver");
                        var password = "Ivtinocana";
                        var sub = "Rejected Offer";
                        var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                       "\n We hope you are doing well, you have make the following offer on one of our cars." +
                       "\n Offer ID: " + oFFER.OFFER_ID + " " +
                       "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                       "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                       "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME +
                       "\n Price: R " + oFFER.AMOUNT + "  " +
                       "\n Unfortunately we have rejected this offer. " +
                       "\n Please feel free to contact us for more information." +
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
                }
                TempData["AlertMessage"] = "You have just rejected this offer";
                return RedirectToAction("AdminIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
            }
        }

        public ActionResult AcceptC(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 1;
                db.SaveChanges();
                if (oFFER.USER_ID != 1)
                {

                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    var receiverEmail = new MailAddress("vehlutionReceive@gmail.com", "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Accepted Offer";
                    var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                         "\n We hope you are doing well, you have make the following offer on my car." +
                         "\n Offer ID: " + oFFER.OFFER_ID + " " +
                         "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                         "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                         "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME +
                         "\n Price: R " + oFFER.AMOUNT + "  " +
                         "\n I would like to accept this offer" +
                         "\n Please contact me via email to finalize this sale: " + oFFER.CAR.USER.EMAIL +
                         "\n Yours Faithfully, " +
                         "\n" + oFFER.USER.FIRSTNAME + oFFER.USER.LASTNAME;
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
                TempData["AlertMessage"] = "You have just accepted this offer";
                return RedirectToAction("AdminIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
            }
        

    }
        public ActionResult RejectC(int id)
        {
            try
            {
                OFFER oFFER = db.OFFERS.Find(id);
                oFFER.STATUS_ID = 2;
                db.SaveChanges();
                if (oFFER.USER_ID != null)
                {

                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    var receiverEmail = new MailAddress("vehlutionReceive@gmail.com", "Receiver");
                    var password = "Ivtinocana";
                    var sub = "Rejected Offer";
                    var body = "Good day " + oFFER.CAR.USER.FIRSTNAME +
                      "\n We hope you are doing well, you have make the following offer on one of our cars." +
                      "\n Offer ID: " + oFFER.OFFER_ID + " " +
                      "\n Car Registration : " + oFFER.CAR.CAR_REG + " " +
                      "\n Car Details: " + oFFER.CAR.MODEL.MAKE.MAKE_NAME + " " + oFFER.CAR.MODEL.MODEL_NAME +
                      "\n Colour: " + oFFER.CAR.COLOUR.COLOUR_NAME +
                      "\n Price: R " + oFFER.AMOUNT + "  " +
                      "\n Unfortunately I have rejected this offer. " +
                      "\n Please feel free to contact me for more information." + oFFER.CAR.USER.EMAIL +
                      "\n Yours Faithfully, " +
                      "\n" + oFFER.USER.FIRSTNAME + oFFER.USER.LASTNAME;
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


                TempData["AlertMessage"] = "You have just accepted this offer";
                return RedirectToAction("AdminIndex");
            }
            catch (Exception err)
            {
                TempData["AlertMessage"] = "Sorry something went wrong please try again later. " + err;
                return RedirectToAction("AdminIndex");
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
