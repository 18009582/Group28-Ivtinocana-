﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Main.Controllers
{
    public class UserDashController : Controller
    {
       
        // GET: UserDash
       [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Register");
        }
    }
}