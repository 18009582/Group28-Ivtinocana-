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
    public class USER_ROLEController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();
        //static public List<CAR> carlist = new List<CAR>();
       // public static List<USER_ROLE> rolelist = new List<USER_ROLE>();

        // GET: USER_ROLE
        public ActionResult Index()
        {
            return View(db.USER_ROLE.ToList());
        }

        // GET: USER_ROLE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_ROLE uSER_ROLE = db.USER_ROLE.Find(id);
            if (uSER_ROLE == null)
            {
                return HttpNotFound();
            }
            return View(uSER_ROLE);
        }

        // GET: USER_ROLE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: USER_ROLE/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
      
        public ActionResult Create([Bind(Include = "USERROLE_ID,ROLE,BLOCKED")] USER_ROLE uSER_ROLE)
        {
            if (ModelState.IsValid)
            {
                db.USER_ROLE.Add(uSER_ROLE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(uSER_ROLE);
        }

        // GET: USER_ROLE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_ROLE uSER_ROLE = db.USER_ROLE.Find(id);
            if (uSER_ROLE == null)
            {
                return HttpNotFound();
            }
            return View(uSER_ROLE);
        }

        // POST: USER_ROLE/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
     
        public ActionResult Edit([Bind(Include = "USERROLE_ID,ROLE,BLOCKED")] USER_ROLE uSER_ROLE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uSER_ROLE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(uSER_ROLE);
        }

        // GET: USER_ROLE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            USER_ROLE uSER_ROLE = db.USER_ROLE.Find(id);
            if (uSER_ROLE == null)
            {
                return HttpNotFound();
            }
            return View(uSER_ROLE);
        }

        // POST: USER_ROLE/Delete/5
        [HttpPost, ActionName("Delete")]
    
        public ActionResult DeleteConfirmed(int id)
        {
            USER_ROLE uSER_ROLE = db.USER_ROLE.Find(id);
            db.USER_ROLE.Remove(uSER_ROLE);
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
    }
}
