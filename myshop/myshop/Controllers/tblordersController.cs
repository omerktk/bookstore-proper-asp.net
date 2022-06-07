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
    public class tblordersController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: tblorders
        public ActionResult Index()
        {
            var tblorders = db.tblorders.Include(t => t.tblcust);
            return View(tblorders.ToList());
        }

        // GET: tblorders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblorder tblorder = db.tblorders.Find(id);
            if (tblorder == null)
            {
                return HttpNotFound();
            }
            return View(tblorder);
        }

        // GET: tblorders/Create
        public ActionResult Create()
        {
            ViewBag.ostatus = new SelectList(db.tblcusts, "customerid", "customername");
            return View();
        }

        // POST: tblorders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "oid,odate,opname,opphone,opaddress,oamount,ostatus")] tblorder tblorder)
        {
            if (ModelState.IsValid)
            {
                db.tblorders.Add(tblorder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ostatus = new SelectList(db.tblcusts, "customerid", "customername", tblorder.ostatus);
            return View(tblorder);
        }

        // GET: tblorders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblorder tblorder = db.tblorders.Find(id);
            if (tblorder == null)
            {
                return HttpNotFound();
            }
            ViewBag.ostatus = new SelectList(db.tblcusts, "customerid", "customername", tblorder.ostatus);
            return View(tblorder);
        }

        // POST: tblorders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "oid,odate,opname,opphone,opaddress,oamount,ostatus")] tblorder tblorder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblorder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ostatus = new SelectList(db.tblcusts, "customerid", "customername", tblorder.ostatus);
            return View(tblorder);
        }

        // GET: tblorders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblorder tblorder = db.tblorders.Find(id);
            if (tblorder == null)
            {
                return HttpNotFound();
            }
            return View(tblorder);
        }

        // POST: tblorders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblorder tblorder = db.tblorders.Find(id);
            db.tblorders.Remove(tblorder);
            db.SaveChanges();
            return RedirectToAction("deletedone");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        public ActionResult deletedone()
        {

            return View();
        }
    }
}
