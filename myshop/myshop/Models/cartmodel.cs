using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myshop.Models
{
    public class cartmodel
    {
        public static List<cartitem> c = new List<cartitem>();
    }

    public class cartitem
    {
        public int iid;
        public int iqty;
    }
}