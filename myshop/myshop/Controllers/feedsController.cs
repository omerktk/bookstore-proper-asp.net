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
    public class feedsController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: feeds
        public ActionResult Index()
        {
            return View(db.feeds.ToList());
        }

        // GET: feeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feed feed = db.feeds.Find(id);
            if (feed == null)
            {
                return HttpNotFound();
            }
            return View(feed);
        }

        // GET: feeds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: feeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "feedid,feedq,feeda")] feed feed)
        {
            if (ModelState.IsValid)
            {
                db.feeds.Add(feed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feed);
        }

        // GET: feeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feed feed = db.feeds.Find(id);
            if (feed == null)
            {
                return HttpNotFound();
            }
            return View(feed);
        }

        // POST: feeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "feedid,feedq,feeda")] feed feed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feed);
        }

        // GET: feeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            feed feed = db.feeds.Find(id);
            if (feed == null)
            {
                return HttpNotFound();
            }
            return View(feed);
        }

        // POST: feeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            feed feed = db.feeds.Find(id);
            db.feeds.Remove(feed);
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
