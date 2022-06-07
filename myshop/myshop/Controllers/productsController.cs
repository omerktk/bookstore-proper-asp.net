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
    public class productsController : Controller
    {
        private myshopDBEntities db = new myshopDBEntities();

        // GET: products
        public ActionResult Index()
        {
            var tblproes = db.tblproes.Include(t => t.tblcat);
            return View(tblproes.ToList());
        }

        // GET: products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpro tblpro = db.tblproes.Find(id);
            if (tblpro == null)
            {
                return HttpNotFound();
            }
            return View(tblpro);
        }

        // GET: products/Create
        public ActionResult Create()
        {
            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname");
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pid,pname,pprice,pdetails,cid")] tblpro tblpro)
        {
            if (ModelState.IsValid)
            {
                db.tblproes.Add(tblpro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname", tblpro.cid);
            return View(tblpro);
        }

        // GET: products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpro tblpro = db.tblproes.Find(id);
            if (tblpro == null)
            {
                return HttpNotFound();
            }
            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname", tblpro.cid);
            return View(tblpro);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pid,pname,pprice,pdetails,cid")] tblpro tblpro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblpro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.cid = new SelectList(db.tblcats, "cid", "cname", tblpro.cid);
            return View(tblpro);
        }

        // GET: products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblpro tblpro = db.tblproes.Find(id);
            if (tblpro == null)
            {
                return HttpNotFound();
            }
            return View(tblpro);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblpro tblpro = db.tblproes.Find(id);
            db.tblproes.Remove(tblpro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Images(int id) {

            Session["pid"] = id;
            var pro = db.tblproes.Where(l => l.pid == id).ToList();

            int d = Convert.ToInt32(Session["pid"]);
            var imgs = db.tblimages.Where(l => l.pid==d).ToList();
            ViewBag.imgs = imgs;
            ViewBag.pro = pro;

            return View();
        }

        [HttpPost]
        public ActionResult Images(int id,HttpPostedFileBase file){
            string path = System.IO.Path.Combine("~/Images/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            tblimage img = new tblimage();
            img.img_name = file.FileName;
            img.pid = id;
            db.tblimages.Add(img);
                db.SaveChanges();
            //return View(db.tblimages.ToList());
            return RedirectToAction("Images");
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
