using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Vehlution_Everything_.Controllers
{
    public class ContactAboutController : Controller
    {
        // GET: ContactAbout
        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Email(string name, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                var receiverEmail = new MailAddress("vehlutionReceive@gmail.com", "Receiver");
                var password = "Ivtinocana";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new System.Net.Mail.MailMessage(senderEmail, receiverEmail)
                {
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                TempData["AlertMessage"] = "We will get back to you as soon as possible";
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = err;
            }
             
            return RedirectToAction("ContactUs");
        }
    }
}