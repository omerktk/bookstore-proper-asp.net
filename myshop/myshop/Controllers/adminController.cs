using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using myshop;

namespace myshop.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
      
        

        [HttpPost]
        public ActionResult Index(tbluser usr)
        {
            myshopDBEntities obj = new myshopDBEntities();
            var a = obj.tblusers.Where(l => l.uname.Equals(usr.uname) && l.upass.Equals(usr.upass)).SingleOrDefault();
            if (a != null)
            {
                Session["uname"] = usr.uname.ToString();
                return RedirectToAction("Dashboard");
            }
            else {
                ViewBag.msg = "Invalid User Name or Password";
            }
            return View();
        }
        public ActionResult logout()
        {

            Session.Abandon();
            Session.Remove("uname");
            Session.RemoveAll();

            return RedirectToAction("index");
        }

        // after login and log out

        public ActionResult Index()
        {
            if (Session["uname"] != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            if (Session["uname"] == null) {
                return RedirectToAction("Index");
            }
            myshopDBEntities db = new myshopDBEntities();
            var p = db.tblorders.ToList();
            ViewBag.p = p;
            return View();
        }

        public ActionResult orderdata(int oid, string price)
        {
            ViewBag.oid = oid;
            ViewBag.price = price;
            var orderid = oid;
            myshopDBEntities db = new myshopDBEntities();
            var p = db.tblorderdetails.Where(x => x.oid == orderid).ToList();
            ViewBag.p = p;
            var o = db.tblorders.Where(x => x.oid == orderid).ToList();
            ViewBag.data = o;
            return View();
        }

    }
}