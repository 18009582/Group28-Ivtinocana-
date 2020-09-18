﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Vehlution_Everything_.Controllers
{
    public class EmailController : Controller
    {// GET: Home
        public ActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Email(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                    var receiverEmail = new MailAddress(receiver, "Receiver");
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
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Error";
            }
            return View();
        }

        public ActionResult ReceiveEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ReceiveEmail(string receiver, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
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
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return View();
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Error";
            }
            return View();
        }
    }
}