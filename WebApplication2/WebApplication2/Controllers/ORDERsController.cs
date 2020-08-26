using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ORDERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        public List<NewOrderItem> NewPartsOrder = new List<NewOrderItem>();
        // GET: ORDERs
        public ActionResult Index()
        {
            dynamic data = new ExpandoObject();
            List<ORDER> oRDERs = db.ORDERs.ToList();
            List<CAR_PARTS_ORDERED> PartsOrdered = db.CAR_PARTS_ORDERED.ToList();
            List<CAR_PARTS> Partslist = new List<CAR_PARTS>();
            Partslist = db.CAR_PARTS.ToList();


            List<dynamic> DynamicOrders = new List<dynamic>();

            foreach(ORDER order in oRDERs)
            {
                dynamic dorder=new ExpandoObject();
                dorder.ORDER_ID = order.ORDER_ID;
                dorder.ORDER_DATE_ = order.ORDER_DATE_;
                dorder.ORDER_PRICE_ = order.ORDER_PRICE_;
                dorder.ORDER_STATUS_ = order.ORDER_STATUS_;
                dorder.SUPPLIER= order.SUPPLIER;
                List<dynamic> dparts = new List<dynamic>();
                List<CAR_PARTS_ORDERED> cpo = PartsOrdered.Where(zz => zz.ORD_ID == order.ORDER_ID).ToList();
                foreach(CAR_PARTS_ORDERED o in cpo)
                {
                    dynamic x = new ExpandoObject();
                    x.CARPARTS_ID = o.CARPARTS_ID;
                    x.QUANTITY = o.QUANTITY;
                    x.PARTNAME = o.CAR_PARTS.PARTNAME;
                    dparts.Add(x);
                }
                dorder.Parts = dparts;
                DynamicOrders.Add(dorder);
            }

            data.Orders = DynamicOrders;
            data.CarParts = Partslist;
            return View(data);
        }

        // GET: ORDERs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERs.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            return View(oRDER);
        }

        // GET: ORDERs/Create
        public ActionResult Create()
        {
            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERs, "SUPPLIER_ID", "NAME_");
            ViewBag.PartsOrdered = NewPartsOrder;
            ViewBag.total = NewPartsOrder.Sum(z => z.Total);
            return View();
        }

        // POST: ORDERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ORDER_ID,SUPPLIER_ID,ORDER_DATE_,ORDER_PRICE_,ORDER_STATUS_")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.ORDERs.Add(oRDER);
                db.SaveChanges();
                foreach(NewOrderItem o in NewPartsOrder)
                {
                    CAR_PARTS_ORDERED x = new CAR_PARTS_ORDERED();
                    x.CARPARTS_ID = o.PartID;
                    x.ORD_ID = db.ORDERs.Last().ORDER_ID;
                    x.QUANTITY = o.Qty;
                    db.CAR_PARTS_ORDERED.Add(x);
                }
                return RedirectToAction("Index");
            }

            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERs, "SUPPLIER_ID", "NAME_", oRDER.SUPPLIER_ID);
            return View(oRDER);
        }

        // GET: ORDERs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDER oRDER = db.ORDERs.Find(id);
            if (oRDER == null)
            {
                return HttpNotFound();
            }
            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERs, "SUPPLIER_ID", "NAME_", oRDER.SUPPLIER_ID);
            return View(oRDER);
        }

        // POST: ORDERs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ORDER_ID,SUPPLIER_ID,ORDER_DATE_,ORDER_PRICE_,ORDER_STATUS_")] ORDER oRDER)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDER).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SUPPLIER_ID = new SelectList(db.SUPPLIERs, "SUPPLIER_ID", "NAME_", oRDER.SUPPLIER_ID);
            return View(oRDER);
        }

        // GET: ORDERs/Delete/5
        public ActionResult Delete(int? id)
        {
          
            ORDER oRDER = db.ORDERs.Find(id);
            db.ORDERs.Remove(oRDER);
            db.SaveChanges();
            return RedirectToAction("Index");
            return View(oRDER);
        }

        // POST: ORDERs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ORDER oRDER = db.ORDERs.Find(id);
            db.ORDERs.Remove(oRDER);
            db.SaveChanges();
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

        [HttpPost]
        public void addToList(int Id,int Qty,double Price)
        {
            NewOrderItem newOrder = new NewOrderItem();
            newOrder.PartID = Id;
            newOrder.PartName = db.CAR_PARTS.Where(z=>z.CARPARTS_ID==Id).FirstOrDefault().PARTNAME;
            newOrder.Qty = Qty;
            newOrder.Price = Price;
            newOrder.Total = Qty * Price;
            NewPartsOrder.Add(newOrder);
            


        }
    
    }
}
