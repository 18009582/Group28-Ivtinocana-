using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Vehlution_Everything_.Models;
using System.Net.Mail;
using System;

namespace Vehlution_Everything_.Controllers
{
    public class ORDERsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
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
            try
            {
                ORDER o = new ORDER();
                string body;

                o.ORDER_DATE_ = date;
                o.ORDER_STATUS_ = "Placed";
                o.SUPPLIER_ID = SUPPLIER_ID;
                o.ORDER_PRICE_ = NewPartsOrder.Sum(z => z.Total);
                db.ORDERs.Add(o);
                db.SaveChanges();
                body = "Good day " + db.SUPPLIERs.Where(zz => zz.SUPPLIER_ID == SUPPLIER_ID).First().NAME_.Trim() + ", I trust you are well. \n  We'd like to place the following order: \n Order ID: " + db.ORDERs.ToList().Last().ORDER_ID + "\t date: " + date.ToShortDateString() + "\n" + "Order details";
                int id = db.ORDERs.ToList().Last().ORDER_ID;
                foreach (NewOrderItem x in NewPartsOrder)
                {
                    CAR_PARTS_ORDERED c = new CAR_PARTS_ORDERED();
                    c.CARPARTS_ID = x.PartID;
                    c.ORD_ID = id;
                    c.QUANTITY = x.Qty;
                    db.CAR_PARTS_ORDERED.Add(c);
                    body += "\n" + x.PartName + " X " + x.Qty + "\t R" + x.Price;
                    db.SaveChanges();
                }
                body += "\n Total: R " + NewPartsOrder.Sum(z => z.Total) + " \n Thank you. \n + Yours Faithfully, \n Vehlution  ";


                var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");
                string email = db.SUPPLIERs.Where(zz => zz.SUPPLIER_ID == SUPPLIER_ID).First().EMAIL_;
                var receiverEmail = new MailAddress(email, "Receiver");
                var password = "Ivtinocana";
                var sub = "New Order";

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
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                NewPartsOrder.Clear();
                TempData["AlertMessage"] = "Your order has been placed!";
                return RedirectToAction("Index");
            
            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }


        }





        // POST: ORDERs/Delete/5
        [HttpPost, ActionName("Delete")]
        //  [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int DelId)
        {
            try
            {
                ORDER oRDER = db.ORDERs.Find(DelId);
                var sup = oRDER.SUPPLIER.EMAIL_;
                var supname = oRDER.SUPPLIER.NAME_;
                var dateplaced = Convert.ToDateTime(oRDER.ORDER_DATE_).ToShortDateString();

                db.ORDERs.Remove(oRDER);

                foreach (CAR_PARTS_ORDERED x in db.CAR_PARTS_ORDERED)
                {
                    if (x.ORD_ID == DelId)
                    {
                        db.CAR_PARTS_ORDERED.Remove(x);

                    }
                }

                db.SaveChanges();

                var body = "Good day" + supname + ", I trust you are well. +\n We regret to inform you that we will be canceling our order , Order Id : " + DelId + " , Placed of the " + dateplaced + " \n Thank you. \n + Yours Faithfully, \n Vehlution  ";
                var senderEmail = new MailAddress("vehlution@gmail.com", "Vehlution");

                var receiverEmail = new MailAddress(sup, "Receiver");
                var password = "Ivtinocana";
                var sub = "New Order";

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
                    Subject = sub,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                TempData["AlertMessage"] = "Your order has been cancelled!";
                return RedirectToAction("Index");

            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Index");

            }

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
            try
            {
                foreach (NewOrderItem x in NewPartsOrder)
                {
                    if (x.PartID == id)
                    {
                        NewPartsOrder.Remove(x);
                        return RedirectToAction("Create");

                    }
                }
                TempData["AlertMessage"] = "Your item has been removed!";
                return RedirectToAction("Create");

            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Create");

            }

        }
        [HttpPost]
        public ActionResult RecieveOrder(int id)
        {
            try
            {
                db.ORDERs.Find(id).ORDER_STATUS_ = "Recieved";
                List<CAR_PARTS_ORDERED> ord = db.CAR_PARTS_ORDERED.Where(zz => zz.ORD_ID == id).ToList();
                foreach (CAR_PARTS_ORDERED x in ord)
                {
                    db.CAR_PARTS.Find(x.CARPARTS_ID).STOCKONHAND += x.QUANTITY;
                }
                db.SaveChanges();
                TempData["AlertMessage"] = "Your order has been recieved!";
                return RedirectToAction("Create");

            }
            catch
            {
                TempData["AlertMessage"] = "Sorry something went wrong, please try again late";
                return RedirectToAction("Create");

            }

        }

    }
}
