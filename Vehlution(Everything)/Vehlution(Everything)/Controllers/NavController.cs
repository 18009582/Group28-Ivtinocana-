using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;

namespace Vehlution_Everything_.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
        public ActionResult AdminNav()
        {
            

            return View();
        }

        public ActionResult ClientNav()
        {
            return View();
        }

        public ActionResult LoginNav()
        {
            return View();
        }

        
    }
}