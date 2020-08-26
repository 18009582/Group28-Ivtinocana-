using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using User.Models;

namespace User.Controllers
{
    public class CLIENTsController : Controller
    {
        private VehlutionEntities db = new VehlutionEntities();

        // GET: CLIENTs
        public ActionResult Index()
        {
            var cLIENTs = db.CLIENTs.Include(c => c.USER);
            return View(cLIENTs.ToList());
        }

        // GET: CLIENTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENTs.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cLIENT);
        }

        // GET: CLIENTs/Create
        public ActionResult Create()
        {
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "FULL_NAME");
            return View();
        }

        // POST: CLIENTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CLIENT_ID,USER_ID,USER_NAME,PASSWORD,FULL_NAME_,EMAIL_,ADDRESS")] CLIENT cLIENT)
        {
            if (ModelState.IsValid)
            {
                db.CLIENTs.Add(cLIENT);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "FULL_NAME", cLIENT.USER_ID);
            return View(cLIENT);
        }

        // GET: CLIENTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENTs.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "FULL_NAME", cLIENT.USER_ID);
            return View(cLIENT);
        }

        // POST: CLIENTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CLIENT_ID,USER_ID,USER_NAME,PASSWORD,FULL_NAME_,EMAIL_,ADDRESS")] CLIENT cLIENT)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cLIENT).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.USER_ID = new SelectList(db.USERS, "USER_ID", "FULL_NAME", cLIENT.USER_ID);
            return View(cLIENT);
        }

        // GET: CLIENTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CLIENT cLIENT = db.CLIENTs.Find(id);
            if (cLIENT == null)
            {
                return HttpNotFound();
            }
            return View(cLIENT);
        }

        // POST: CLIENTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CLIENT cLIENT = db.CLIENTs.Find(id);
            db.CLIENTs.Remove(cLIENT);
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
