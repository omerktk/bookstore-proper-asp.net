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
    public class orderstatusController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: orderstatus
        public ActionResult Index()
        {
            var tblorderdetails = db.tblorderdetails.Include(t => t.tblcust).Include(t => t.tblorder).Include(t => t.tblpro);
            return View(tblorderdetails.ToList());
        }

        // GET: orderstatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblorderdetail tblorderdetail = db.tblorderdetails.Find(id);
            if (tblorderdetail == null)
            {
                return HttpNotFound();
            }
            return View(tblorderdetail);
        }

        // GET: orderstatus/Create
        public ActionResult Create()
        {
            ViewBag.custid = new SelectList(db.tblcusts, "customerid", "customername");
            ViewBag.oid = new SelectList(db.tblorders, "oid", "opname");
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname");
            return View();
        }

        // POST: orderstatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "did,oid,pid,pprice,pqyt,pamount,custid,odelivery")] tblorderdetail tblorderdetail)
        {
            if (ModelState.IsValid)
            {
                db.tblorderdetails.Add(tblorderdetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.custid = new SelectList(db.tblcusts, "customerid", "customername", tblorderdetail.custid);
            ViewBag.oid = new SelectList(db.tblorders, "oid", "opname", tblorderdetail.oid);
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname", tblorderdetail.pid);
            return View(tblorderdetail);
        }

        // GET: orderstatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblorderdetail tblorderdetail = db.tblorderdetails.Find(id);
            if (tblorderdetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.custid = new SelectList(db.tblcusts, "customerid", "customername", tblorderdetail.custid);
            ViewBag.oid = new SelectList(db.tblorders, "oid", "opname", tblorderdetail.oid);
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname", tblorderdetail.pid);
            return View(tblorderdetail);
        }

        // POST: orderstatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "did,oid,pid,pprice,pqyt,pamount,custid,odelivery")] tblorderdetail tblorderdetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblorderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("done");
            }
            ViewBag.custid = new SelectList(db.tblcusts, "customerid", "customername", tblorderdetail.custid);
            ViewBag.oid = new SelectList(db.tblorders, "oid", "opname", tblorderdetail.oid);
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname", tblorderdetail.pid);
            return View(tblorderdetail);
        }



        // GET: orderstatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblorderdetail tblorderdetail = db.tblorderdetails.Find(id);
            if (tblorderdetail == null)
            {
                return HttpNotFound();
            }
            return View(tblorderdetail);
        }

        // POST: orderstatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblorderdetail tblorderdetail = db.tblorderdetails.Find(id);
            db.tblorderdetails.Remove(tblorderdetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult done()
        {
            return View();
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
