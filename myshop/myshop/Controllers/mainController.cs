using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myshop;
using myshop.Models;

namespace myshop.Controllers
{
    public class mainController : Controller
    {
        myshopDBEntities db = new myshopDBEntities();

        // GET: main
        public ActionResult faqs()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            myshopDBEntities db = new myshopDBEntities();
            var p = db.faqs.ToList();
            ViewBag.p = p;
            return View();
        }

        
        public ActionResult feeds()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            myshopDBEntities db = new myshopDBEntities();

           

            var p = db.feeds.ToList();
            ViewBag.p = p;
            return View();
        }

        [HttpPost]
        public ActionResult feeddone(string feedq, string feeda)
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            feed obj = new feed();
            obj.feedq = feedq;
            obj.feeda = feeda;
            db.feeds.Add(obj);
            db.SaveChanges();
            return RedirectToAction("feeds");
        }

        // GET: admin
        public ActionResult Index()
        {
            if (Session["custid"] != null)
            {
                return RedirectToAction("home");
            }

            return View();
        }

      

        [HttpPost]
        public ActionResult Index(tblcust usr)
        {
            myshopDBEntities obj = new myshopDBEntities();
            var a = obj.tblcusts.Where(l => l.customermail.Equals(usr.customermail) && l.customerpass.Equals(usr.customerpass)).SingleOrDefault();
            if (a != null)
            {
                Session["customerid"] = a.customerid;
                Session["custid"] = usr.customermail.ToString();
                return RedirectToAction("home");
            }
            else
            {
                ViewBag.msg = "Invalid Email or Password";
            }
            return View();
        }


        public ActionResult logout()
        {
            cartmodel.c.Clear();
            Session.Abandon();
            Session.Remove("custid");
            Session.RemoveAll();

            return RedirectToAction("index");
        }


        public ActionResult home()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            myshopDBEntities db = new myshopDBEntities();
            var p = db.tblproes.ToList();
            ViewBag.p = p;

            var imgs = db.tblimages.ToList();
            ViewBag.imgs = imgs;

            return View();
        }

        public ActionResult allproducts()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            myshopDBEntities db = new myshopDBEntities();
            var p = db.tblproes.ToList();
            ViewBag.p = p;

            var imgs = db.tblimages.ToList();
            ViewBag.imgs = imgs;

            return View();
        }

        public ActionResult product(int id)
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            myshopDBEntities db = new myshopDBEntities();
            var p = db.tblproes.Where(U => U.pid == id).ToList();
            ViewBag.p = p;
            var imgs = db.tblimages.Where(U => U.pid == id).ToList();
            ViewBag.imgs = imgs;

            var allp = db.tblproes.ToList();
            ViewBag.allp = allp;

            var alli = db.tblimages.ToList();
            ViewBag.alli = alli;
            return View();
        }

        public ActionResult cartdata(int pid, int pqty)
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            foreach (var item in cartmodel.c)
            {
                if (item.iid == pid)
                {
                    item.iqty += pqty;
                    ViewBag.c = cartmodel.c;
                    return RedirectToAction("cart");
                }
            }
            cartitem i = new cartitem() { iid = pid, iqty = pqty };
            cartmodel.c.Add(i);
            ViewBag.c = cartmodel.c;
            return RedirectToAction("cart");
        }

        public ActionResult cart()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.c = cartmodel.c;
            return View();
        }

        [HttpPost]  
        public ActionResult checkout(string userid, string money)
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.t = money;
            return View();
        }


        [HttpPost]
        public ActionResult doneorder(tblorder tb, string userid, string money)
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            
            tblorder obj = new tblorder();
            obj.odate = DateTime.Now;
            obj.opname = tb.opname;
            obj.opphone = tb.opphone;
            obj.opaddress = tb.opaddress;
            obj.ostatus = (int)decimal.Parse(userid);
            obj.oamount = decimal.Parse(money);
            
            ViewBag.i = obj;
            db.tblorders.Add(obj);
            db.SaveChanges();

            int moid = db.tblorders.Select(a => a.oid).DefaultIfEmpty(0).Max();
            var pro = from prod in cartmodel.c
                      join od in db.tblproes
                      on prod.iid equals od.pid
                      select new { PID = od.pid, PPRICE = od.pprice, PQTY = prod.iqty };

            var ode = ("In Processing");
            tblorderdetail orderdata = new tblorderdetail();
            foreach (var item in pro)
            {
                orderdata.oid = moid;
                orderdata.pid = item.PID;
                orderdata.custid = (int)decimal.Parse(userid);
                orderdata.pprice = item.PPRICE;
                orderdata.pqyt = item.PQTY;
                orderdata.pamount = item.PQTY * item.PPRICE;
                orderdata.odelivery = ode;
                db.tblorderdetails.Add(orderdata);
                db.SaveChanges();
                
            }

            return RedirectToAction("bugsolve");


        }

        public ActionResult cartempty()
        {
            //cartmodel.c = null;

            cartmodel.c.Clear();



            return RedirectToAction("cart");
        }

        public ActionResult bugsolve()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }

            cartmodel.c.Clear();



            return RedirectToAction("donemsg");
        }
        public ActionResult donemsg()
        {



            return View();
        }

        public ActionResult myorders()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
            var i = Session["customerid"];
            
            int id = Convert.ToInt32(i);
            myshopDBEntities db = new myshopDBEntities();
            var p = db.tblorderdetails.Where(U => U.custid == id).ToList();
            ViewBag.p = p;
            return View();
        }
        public ActionResult deleteorder(int id)
        {
            tblorderdetail tblorderdetail = db.tblorderdetails.Find(id);
            db.tblorderdetails.Remove(tblorderdetail);
            db.SaveChanges();
            return RedirectToAction("myorders");
        }

        public ActionResult about()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }
           
            return View();
        }
        public ActionResult contact()
        {
            if (Session["custid"] == null)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}