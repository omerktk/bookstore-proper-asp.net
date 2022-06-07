using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using myshop;

namespace myshop.Controllers
{
    public class custsidController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: custsid
        public ActionResult Index()
        {
            return View(db.tblcusts.ToList());
        }

        // GET: custsid/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcust tblcust = db.tblcusts.Find(id);
            if (tblcust == null)
            {
                return HttpNotFound();
            }
            return View(tblcust);
        }

        // GET: custsid/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: custsid/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "customerid,customername,customermail,customerpass")] tblcust tblcust)
        {
            if (ModelState.IsValid)
            {
                db.tblcusts.Add(tblcust);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblcust);
        }

        // GET: custsid/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcust tblcust = db.tblcusts.Find(id);
            if (tblcust == null)
            {
                return HttpNotFound();
            }
            return View(tblcust);
        }

        // POST: custsid/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "customerid,customername,customermail,customerpass")] tblcust tblcust)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblcust).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblcust);
        }

        // GET: custsid/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblcust tblcust = db.tblcusts.Find(id);
            if (tblcust == null)
            {
                return HttpNotFound();
            }
            return View(tblcust);
        }

        // POST: custsid/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblcust tblcust = db.tblcusts.Find(id);
            db.tblcusts.Remove(tblcust);
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
