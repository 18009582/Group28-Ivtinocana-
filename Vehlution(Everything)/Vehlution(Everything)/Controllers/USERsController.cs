﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;

namespace Vehlution_Everything_.Controllers
{
    public class USERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: USERs
        public ActionResult Index()
        {
            var uSERS = db.USERs.Include(u => u.USER_ROLE);
            return View(uSERS.ToList());
        }

        // GET: USERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // GET: USERs/Create
        public ActionResult Create()
        {
            ViewBag.USERROLE_ID = new SelectList(db.USER_ROLE, "USERROLE_ID", "ROLE");
            return View();
        }

        // POST: USERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "USER_ID,USERROLE_ID,FIRSTNAME,LASTNAME,EMAIL,PASSWORD,EMAILVERIFICATION,ACTIVATIONCODE,OTP")] USER uSER)
        {
            if (ModelState.IsValid)
            {
                db.USERs.Add(uSER);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USERROLE_ID = new SelectList(db.USER_ROLE, "USERROLE_ID", "ROLE", uSER.USERROLE_ID);
            return View(uSER);
        }

        // GET: USERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            ViewBag.USERROLE_ID = new SelectList(db.USER_ROLE, "USERROLE_ID", "ROLE", uSER.USERROLE_ID);
            return View(uSER);
        }

        // POST: USERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "USER_ID,FIRSTNAME,LASTNAME,EMAIL")] USER uSER)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    USER usr = db.USERs.Find(uSER.USER_ID);
                    usr.FIRSTNAME = uSER.FIRSTNAME;
                    usr.LASTNAME = uSER.LASTNAME;
                    usr.EMAIL = uSER.EMAIL;
                    db.SaveChanges();
                    TempData["AlertMessage"] = "Your profile has successfully been updated";
                    return RedirectToAction("MyProfile");
                }
                ViewBag.USERROLE_ID = new SelectList(db.USER_ROLE, "USERROLE_ID", "ROLE", uSER.USERROLE_ID);
                return View(uSER);
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("MyProfile");
            }
            
        }

        // GET: USERs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER uSER = db.USERs.Find(id);
            if (uSER == null)
            {
                return HttpNotFound();
            }
            return View(uSER);
        }

        // POST: USERs/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OFFER oFFER = new OFFER();
            USER uSER = db.USERs.Find(id);
            oFFER = db.OFFERS.Where(zz => zz.USER_ID == uSER.USER_ID).FirstOrDefault();
            Purchase purchase = new Purchase();
            purchase = db.PURCHASES.Where(zz => zz.USER_ID == uSER.USER_ID).FirstOrDefault();
            CAR_BOOKING cAR_BOOKING = new CAR_BOOKING();
            cAR_BOOKING = db.CAR_BOOKING.Where(zz => zz.USER_ID == uSER.USER_ID).FirstOrDefault();
            CAR cAR = new CAR();
            cAR = db.CARS.Where(zz => zz.USER_ID == uSER.USER_ID).FirstOrDefault();
            BOOKING_FOR_POSSIBLE_PURCHASE bOOKING_FOR_POSSIBLE_PURCHASE = new BOOKING_FOR_POSSIBLE_PURCHASE();
            bOOKING_FOR_POSSIBLE_PURCHASE = db.BOOKING_FOR_POSSIBLE_PURCHASE.Where(zz => zz.USER_ID == uSER.USER_ID).FirstOrDefault();
            if((oFFER != null) | (purchase != null) | (cAR_BOOKING != null) | (bOOKING_FOR_POSSIBLE_PURCHASE != null) | (cAR != null))
            {
                uSER.BLOCKED = Convert.ToBoolean(1);
                db.SaveChanges();
                return RedirectToAction("LoginNav", "Nav");
            }

            db.USERs.Remove(uSER);
            db.SaveChanges();
            return RedirectToAction("LoginNav", "Nav");
        }
        public ActionResult Blocked(int id)
        {

            USER u = db.USERs.Find(id);
            u.BLOCKED = Convert.ToBoolean(1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MyProfile()
        {
            int clientid = Convert.ToInt32(HttpContext.Request.Cookies.Get("User").Value);
            USER usr = db.USERs.Find(clientid);
            return View(usr);
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
