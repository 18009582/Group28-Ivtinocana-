using System;
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
    public class OFFERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: OFFERs
        public ActionResult AdminIndex()
        {
            //where pending //where type = 1 client 2=admin
            var oFFERS = db.OFFERS.Where(zz => zz.STATUS_ID == 3 && zz.OFFERTYPE_ID == 1).Include(o => o.CAR).Include(o => o.CLIENT).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE);
            return View(oFFERS.ToList());
        }
        //need to take in client id as a parameter
        public ActionResult ClientIndex(int clientid)
        {
            //where pending //where type = 1 client 2=admin
            var oFFERS = db.OFFERS.Where(zz => zz.STATUS_ID == 3 && zz.CLIENT_ID == clientid).Include(o => o.CAR).Include(o => o.CLIENT).Include(o => o.OFFER_STATUS).Include(o => o.OFFER_TYPE);
            return View(oFFERS.ToList());
        }

        public ActionResult Accept(int id)
        {
            OFFER oFFER = db.OFFERS.Find(id);
            oFFER.STATUS_ID = 1;
            return RedirectToAction("Index");
        }
        public ActionResult Reject(int id)
        {
            OFFER oFFER = db.OFFERS.Find(id);
            oFFER.STATUS_ID = 2;
            return RedirectToAction("Index");
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
