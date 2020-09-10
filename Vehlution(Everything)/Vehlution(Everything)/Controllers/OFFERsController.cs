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

namespace Vehlution_Everything_.Controllers
{
    public class OFFERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: OFFERs
        public ActionResult AdminIndex()
        {
            //where pending //where type = 1 client 2=admin
            var oFFERS = db.OFFERS.Where(zz => zz.STATUS_ID == 3 && zz.OFFERTYPE_ID == 1).Include(o => o.CAR).Include(o => o.USER).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE);
            return View(oFFERS.ToList());
        }
        //need to take in client id as a parameter
        public ActionResult ClientIndex(int clientid)
        {
            //where pending //where type = 1 client 2=admin
            var oFFERS = db.OFFERS.Where(zz => zz.STATUS_ID == 3 && zz.USER_ID == clientid).Include(o => o.CAR).Include(o => o.USER).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE);
            return View(oFFERS.ToList());
        }

        public ActionResult Accept(int id)
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
                var body = "Your Offer, \n Offer ID: " + oFFER.OFFER_ID + " \n car registration : " + oFFER.CAR.CAR_REG + " \n Price: R " + oFFER.AMOUNT + "  \n Has been accepted \n Please contact your salesperson to finalize the sale.  \n Yours Faithfully , \n Vehlution ";
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
            return RedirectToAction("AdminIndex");
        }
        public ActionResult Reject(int id)
        {
            OFFER oFFER = db.OFFERS.Find(id);
            oFFER.STATUS_ID = 2;
            var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
            if (oFFER.USER_ID != null)
            {
                string email = db.USERs.Where(zz => zz.USER_ID == oFFER.USER_ID).First().EMAIL;
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "Ivtinocana";
                var sub = "Rejected Offer";
                var body = " regret to infrom you that your offer has been rejected Your Offer details :  \n Offer ID: " + oFFER.OFFER_ID + " \n car registration : " + oFFER.CAR.CAR_REG + " \n Price: R " + oFFER.AMOUNT + "  \n if you would like to make another offer please do so on the Vehlution website. \n Yours Faithfully , \n Vehlution";
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
            return RedirectToAction("ClientIndex");
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
