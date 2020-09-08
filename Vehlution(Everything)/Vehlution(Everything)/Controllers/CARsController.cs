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
    public class CARsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: CARs for sale
        public ActionResult ViewCarsForSaleIndex()
        {
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                              .Include(c => c.MODEL)
                              .Include(c => c.COLOUR)
                              .Include(c => c.FUEL_TYPE)
                              .Include(c => c.NUMBER_OF_DOORS)
                              .Include(c => c.NUMBER_OF_SEATS)
                              .Include(c => c.TRANSMISSION)
                              .Include(c => db.MAKEs.Where(p => p.MAKE_ID == c.MODEL.MAKE_ID))
                              .Include(c => c.IMAGE)
                              .Where(cc => cc.STATUS_ID == 5);
            return View(cARS.ToList());
        }

        //GET: CARs that are pending
        public ActionResult PendingCarIndex()
        {
            var cARS = db.CARS.Include(c => c.CAR_STATUS)
                              .Include(c => c.MODEL)
                              .Include(c => c.COLOUR)
                              .Include(c => c.FUEL_TYPE)
                              .Include(c => c.NUMBER_OF_DOORS)
                              .Include(c => c.NUMBER_OF_SEATS)
                              .Include(c => c.TRANSMISSION)
                              .Include(c => db.MAKEs.Where(p => p.MAKE_ID == c.MODEL.MAKE_ID))
                              .Include(c => c.IMAGE)
                              .Where(cc => cc.STATUS_ID == 1);
            return View(cARS.ToList());
        }

        // GET: CARs/Details/5
        public ActionResult SaleDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CARS.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
        }

        public ActionResult PendingDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAR cAR = db.CARS.Find(id);
            if (cAR == null)
            {
                return HttpNotFound();
            }
            return View(cAR);
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