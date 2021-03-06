﻿using Main.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Main.DBContext;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace Main.Controllers
{
    public class RegisterController : Controller
    {
        #region entity connection
        Vehlution4Entities1 objcon = new Vehlution4Entities1();
        #endregion


        // GET: Register


        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        #region Registration post method for data save  
        [HttpPost]
        public ActionResult Index(USER objUsr)
        {
            // email not verified on registration time  
            objUsr.EMAILVERIFICATION = false;

            //email exists or not
            var IsExists = IsEmailExists(objUsr.EMAIL);
            if (IsExists)
            {
                ModelState.AddModelError("EmailExists", "Email Already Exists");
                return View();
            }

            //it generate unique code     
            objUsr.ACTIVATIONCODE = Guid.NewGuid();
            //password convert  
            objUsr.PASSWORD = Main.Models.encryptPassword.textToEncrypt(objUsr.PASSWORD);
            objcon.USERS.Add(objUsr);
            objcon.SaveChanges();

            #region Send email verification link
            SendEmailToUser(objUsr.EMAIL, objUsr.ACTIVATIONCODE.ToString());
            var Message = "Registration complete. Please check your email: " + objUsr.EMAIL;
            ViewBag.Message = Message;
            #endregion

            return View("Registration");
        }
        #endregion


        #region Check Email Exists or not in DB  
        public bool IsEmailExists(string eMail)
        {
            var IsCheck = objcon.USERS.Where(email => email.EMAIL == eMail).FirstOrDefault();
            return IsCheck != null;
        }
        #endregion

        public void SendEmailToUser(string emailId, string activationCode)
        {
            var GenarateUserVerificationLink = "/Register/UserVerification/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("vehlution@gmail.com", "Vehlution"); // use our systems email, to be able to have sender
            var fromEmailpassword = "Ivtinocana"; // Set your password 
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Registration Completed-Demo";
            Message.Body = "<br/> Your registration completed succesfully." +
                           "<br/> please click on the below link for account verification" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>";
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }

        #region Verification from Email Account.  
        public ActionResult UserVerification(string id)


        {
            bool Status = false;

            objcon.Configuration.ValidateOnSaveEnabled = false; // Ignore to password confirmation   
            var IsVerify = objcon.USERS.Where(u => u.ACTIVATIONCODE == new Guid(id)).FirstOrDefault();

            if (IsVerify != null)
            {
                IsVerify.EMAILVERIFICATION = true;
                objcon.SaveChanges();
                ViewBag.Message = "Email Verification completed";
                Status = true;
            }
            else
            {
                ViewBag.Message = "Invalid Request...Email not verify";
                ViewBag.Status = false;
            }

            return View();
        }
        #endregion

        #region User login
        //generate login view

        public ActionResult Login()
        {
            return View();
        }
        #endregion
        [HttpPost]
        public ActionResult Login(UserLogin LgnUsr)
        {
            var _passWord = Main.Models.encryptPassword.textToEncrypt(LgnUsr.Password);
            bool Isvalid = objcon.USERS.Any(x => x.EMAIL == LgnUsr.EmailId && x.EMAILVERIFICATION == true &&
            x.PASSWORD == _passWord);
            if (Isvalid)
            {
                int timeout = LgnUsr.Rememberme ? 60 : 5; // Timeout in minutes, 60 = 1 hour.  
                var ticket = new FormsAuthenticationTicket(LgnUsr.EmailId, false, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = System.DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "UserDash");
            }
            else
            {
                ModelState.AddModelError("", "Invalid Information... Please try again!");
            }
            return View();
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        public string GeneratePassword()
        {
            string OTPLength = "4";
            string OTP = string.Empty;

            string Chars = string.Empty;
            Chars = "1,2,3,4,5,6,7,8,9,0";

            char[] seplitChar = { ',' };
            string[] arr = Chars.Split(seplitChar);
            string NewOTP = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < Convert.ToInt32(OTPLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                NewOTP += temp;
                OTP = NewOTP;
            }
            return OTP;
        }
        public void ForgetPasswordEmailToUser(string emailId, string activationCode, string OTP)
        {
            var GenarateUserVerificationLink = "/Register/ChangePassword/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, GenarateUserVerificationLink);

            var fromMail = new MailAddress("vehlution@gmail.com", "Vehlution"); // use our systems email, to be able to have sender
            var fromEmailpassword = "Ivtinocana"; // Set your password 
            var toEmail = new MailAddress(emailId);

            var smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(fromMail.Address, fromEmailpassword);

            var Message = new MailMessage(fromMail, toEmail);
            Message.Subject = "Password Reset-Demo";
            Message.Body = "<br/> Please click on the link below to change your password" +
                           "<br/><br/><a href=" + link + ">" + link + "</a>" +
                           "<br/> OTP for password change: " + OTP;
            Message.IsBodyHtml = true;
            smtp.Send(Message);
        }
        [HttpPost]
        public ActionResult ForgetPassword(ForgetPassword pass)
        {
            var IsExists = IsEmailExists(pass.EmailId);
            if (!IsExists)
            {
                ModelState.AddModelError("EmailNotExists", "This email is not exists");
                return View();
            }
            var objUsr = objcon.USERS.Where(x => x.EMAIL == pass.EmailId).FirstOrDefault();

            // Genrate OTP   
            string OTP = GeneratePassword();

            objUsr.ACTIVATIONCODE = Guid.NewGuid();
            objUsr.OTP = OTP;
            objcon.Entry(objUsr).State = System.Data.Entity.EntityState.Modified;
            objcon.SaveChanges();

            ForgetPasswordEmailToUser(objUsr.EMAIL, objUsr.ACTIVATIONCODE.ToString(), objUsr.OTP);
            return View();
        }




        public ActionResult ChangePassword()
        {
           
            return View();
        }
       

    }

}