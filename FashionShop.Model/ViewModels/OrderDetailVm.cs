using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Model.ViewModels
{
    public class OrderDetailVm
    {
        public int OrderID { set; get; }
        public string ProductName { set; get; }
        public string Image { set; get; }
        public decimal Price { set; get; }
        public decimal? PromotionPrice { set; get; }
        public int Quantitty { set; get; }
    }
}
