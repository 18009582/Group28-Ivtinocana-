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
    public class BOOKINGsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        
        public ActionResult AdminbookingIndex(int id)
        {
            //where pending //where type = 1 client 2=admin
            var bookings = db.BOOKING_FOR_POSSIBLE_PURCHASE.Where(zz => zz.BOOKING_STATUS.Trim() == "Pending"&& zz.USER_ID==id).ToList();
            return View(bookings.ToList());
        }
        //need to take in client id as a parameter


        public ActionResult Accept(int id)
        {
            BOOKING_FOR_POSSIBLE_PURCHASE bOOKING_FOR_POSSIBLE_PURCHASE = db.BOOKING_FOR_POSSIBLE_PURCHASE.Find(id);
            bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_STATUS = "Accepted";
           
          
            db.SaveChanges();
            
                var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                string email = db.USERs.Where(zz => zz.USER_ID == bOOKING_FOR_POSSIBLE_PURCHASE.USER_ID).First().EMAIL;
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "Ivtinocana";
                var sub = "Accepted booking";
                var body = "Your Booking , \n booking ID: " + bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_ID+ " \n car registration : " + bOOKING_FOR_POSSIBLE_PURCHASE.CAR.CAR_REG + " \n date & time : "+bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_DATE+" "+bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_TIME+ "  \n Has been accepted   \n Yours Faithfully , \n Vehlution ";
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
            
            return RedirectToAction("AdminIndex");
        }
        public ActionResult Reject(int id)
        {
            BOOKING_FOR_POSSIBLE_PURCHASE bOOKING_FOR_POSSIBLE_PURCHASE = db.BOOKING_FOR_POSSIBLE_PURCHASE.Find(id);
            bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_STATUS = "rejected";


            db.SaveChanges();

            var receiverEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
            string email = db.USERs.Where(zz => zz.USER_ID == bOOKING_FOR_POSSIBLE_PURCHASE.USER_ID).First().EMAIL;
            var senderEmail = new MailAddress(email, "Sender");
            var password = "Ivtinocana";
            var sub = "rejected Booking";
            var body = "Your Booking , \n booking ID: " + bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_ID + " \n car registration : " + bOOKING_FOR_POSSIBLE_PURCHASE.CAR.CAR_REG + " \n date & time : " + bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_DATE + " " + bOOKING_FOR_POSSIBLE_PURCHASE.BOOKING_TIME + "  \n Has been rejected   \n Yours Faithfully , \n Vehlution ";
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

            return RedirectToAction("AdminIndex");
            return RedirectToAction("ClientIndex");
        }


    }
}