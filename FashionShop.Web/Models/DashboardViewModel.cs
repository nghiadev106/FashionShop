using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FashionShop.Web.Models
{
    public class DashboardViewModel
    {
        public int ProductCount { set; get; }
        public int PostCount { get; set; }
        public int OrderCount { get; set; }
        public int UserCount { get; set; }
    }
}