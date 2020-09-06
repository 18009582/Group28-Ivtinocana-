using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using D5.Models;

namespace D5.Controllers
{
    public class ORDERsController : Controller
    {
        private VehlutionEntities1 db = new VehlutionEntities1();
        static public List<NewOrderItem> NewPartsOrder = new List<NewOrderItem>();


        // GET: ORDERs
        public ActionResult Index()
        {
            dynamic data = new ExpandoObject();
            List<ORDER> oRDERs = db.ORDERs.ToList();
            List<CAR_PARTS_ORDERED> PartsOrdered = db.CAR_PARTS_ORDERED.ToList();
            List<CAR_PARTS> Partslist = new List<CAR_PARTS>();
            Partslist = db.CAR_PARTS.ToList();


            List<dynamic> DynamicOrders = new List<dynamic>();

            foreach (ORDER order in oRDERs)
            {
                dynamic dorder = new ExpandoObject();
                dorder.ORDER_ID = order.ORDER_ID;
                dorder.ORDER_DATE_ = order.ORDER_DATE_;
                dorder.ORDER_PRICE_ = order.ORDER_PRICE_;
                dorder.ORDER_STATUS_ = order.ORDER_STATUS_;
                dorder.SUPPLIER = order.SUPPLIER;
                List<dynamic> dparts = new List<dynamic>();
                List<CAR_PARTS_ORDERED> cpo = PartsOrdered.Where(zz => zz.ORD_ID == order.ORDER_ID).ToList();
                foreach (CAR_PARTS_ORDERED o in cpo)
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
            data.id = 0;
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
            ViewBag.CarParts = new SelectList(db.CAR_PARTS, "CARPARTS_ID", "PARTNAME");
            ViewBag.total = NewPartsOrder.Sum(z => z.Total);
            ViewBag.PartsOrdered = NewPartsOrder;
            return View();
        }

        // POST: ORDERs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Create(int SUPPLIER_ID, System.DateTime date)
        {

            
            ORDER o = new ORDER();
            
            o.ORDER_DATE_ = date;
            o.ORDER_STATUS_ = "Placed";
            o.SUPPLIER_ID = SUPPLIER_ID;
            o.ORDER_PRICE_ = NewPartsOrder.Sum(z => z.Total);
            db.ORDERs.Add(o);
            db.SaveChanges();
            int id = db.ORDERs.ToList().Last().ORDER_ID;
            foreach (NewOrderItem x in NewPartsOrder)
            {
                CAR_PARTS_ORDERED c = new CAR_PARTS_ORDERED();
                c.CARPARTS_ID = x.PartID;
                c.ORD_ID = id;
                c.QUANTITY = x.Qty;
                db.CAR_PARTS_ORDERED.Add(c);
                db.SaveChanges();
            }
            NewPartsOrder.Clear();
            return RedirectToAction("Index");
        }

      

        

        // POST: ORDERs/Delete/5
        [HttpPost, ActionName("Delete")]
        //  [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int DelId)
        {
            ORDER oRDER = db.ORDERs.Find(DelId);
            db.ORDERs.Remove(oRDER);

            foreach (CAR_PARTS_ORDERED x in db.CAR_PARTS_ORDERED)
            {
                if (x.ORD_ID == DelId)
                {
                    db.CAR_PARTS_ORDERED.Remove(x);

                }
            }
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
        public ActionResult AddToList(int CarParts, int Qty, double Price)
        {
            NewOrderItem newOrder = new NewOrderItem();
            foreach (NewOrderItem x in NewPartsOrder)
            {
                if (x.PartID == CarParts)
                {
                    x.Qty += Qty;
                    x.Price = Price;
                    x.Total = x.Qty * Price;
                    return RedirectToAction("Create");
                }
            }
            newOrder.PartID = CarParts;
            newOrder.PartName = db.CAR_PARTS.Where(z => z.CARPARTS_ID == CarParts).FirstOrDefault().PARTNAME;
            newOrder.Qty = Qty;
            newOrder.Price = Price;
            newOrder.Total = Qty * Price;
            NewPartsOrder.Add(newOrder);
            ViewBag.PartsOrdered = NewPartsOrder;
            return RedirectToAction("Create");

        }

        [HttpPost]
        public ActionResult RemoveFromList(int id)
        {
            foreach (NewOrderItem x in NewPartsOrder)
            {
                if (x.PartID == id)
                {
                    NewPartsOrder.Remove(x);
                    continue;

                }
            }
            return RedirectToAction("Create");
        }
        [HttpPost]
        public ActionResult RecieveOrder(int id)
        {
            db.ORDERs.Find(id).ORDER_STATUS_ = "Recieved";
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
