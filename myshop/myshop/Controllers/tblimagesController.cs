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
    public class tblimagesController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: tblimages
        public ActionResult Index()
        {
            var tblimages = db.tblimages.Include(t => t.tblpro);
            return View(tblimages.ToList());
        }

        // GET: tblimages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblimage tblimage = db.tblimages.Find(id);
            if (tblimage == null)
            {
                return HttpNotFound();
            }
            return View(tblimage);
        }

        // GET: tblimages/Create
        public ActionResult Create()
        {
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname");
            return View();
        }

        // POST: tblimages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "img_id,img_name,pid")] tblimage tblimage)
        {
            if (ModelState.IsValid)
            {
                db.tblimages.Add(tblimage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname", tblimage.pid);
            return View(tblimage);
        }

        // GET: tblimages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblimage tblimage = db.tblimages.Find(id);
            if (tblimage == null)
            {
                return HttpNotFound();
            }
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname", tblimage.pid);
            return View(tblimage);
        }

        // POST: tblimages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "img_id,img_name,pid")] tblimage tblimage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblimage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pid = new SelectList(db.tblproes, "pid", "pname", tblimage.pid);
            return View(tblimage);
        }

        // GET: tblimages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblimage tblimage = db.tblimages.Find(id);
            if (tblimage == null)
            {
                return HttpNotFound();
            }
            return View(tblimage);
        }

        // POST: tblimages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblimage tblimage = db.tblimages.Find(id);
            db.tblimages.Remove(tblimage);
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
