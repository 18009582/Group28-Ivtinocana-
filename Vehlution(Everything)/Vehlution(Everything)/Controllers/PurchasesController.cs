using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using Vehlution_Everything_.ViewModel;
namespace Vehlution_Everything_.Controllers
{
    public class PurchasesController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        // GET: Purchases
        public static List<OFFER> oFFERs = new List<OFFER>();
        public ActionResult Index()
        {

            ViewBag.Regs = new SelectList(db.CARS, "CAR_ID", "CAR_REG");
            return View(oFFERs);
        }
        public ActionResult Search(int Regs)
        {

            oFFERs = db.OFFERS.Where(zz => zz.CAR_ID == Regs && zz.OFFERTYPE_ID == 2 && zz.STATUS_ID == 1).ToList();
            return RedirectToAction("Index");
        }
        public ActionResult Create(int id)
        {
            try
            {
                OFFER of = db.OFFERS.Where(zz => zz.OFFER_ID == id).FirstOrDefault();
                Purchase p = new Purchase();
                db.OFFERS.Find(of.OFFER_ID).STATUS_ID = 4;
                p.CAR_ID = of.CAR_ID;
                p.USER_ID = of.USER_ID;
                p.PURCHASEDATE_ = DateTime.Now;
                p.COST_ = of.AMOUNT;
                db.PURCHASES.Add(p);
                db.CARS.Find(of.CAR_ID).STATUS_ID = 2;
                db.SaveChanges();
                oFFERs.Clear();
                TempData["AlertMessage"] = "You have just made a purchase!";
                return RedirectToAction("Index");
            }
            catch(Exception err)
            {
                TempData["AlertMessage"] = "Sorry somthing went wrong" + err;
                return RedirectToAction("Index");
            }
            
        }
    }
}