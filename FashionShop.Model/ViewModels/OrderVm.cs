using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashionShop.Model.ViewModels
{
    public class OrderVm
    {
        public int ID { set; get; }

        public string CustomerName { set; get; }

        public string CustomerAddress { set; get; }

        public string CustomerEmail { set; get; }
 
        public string CustomerMobile { set; get; }

        public string CustomerMessage { set; get; }

        public string PaymentMethod { set; get; }

        public DateTime? CreatedDate { set; get; }
        public string CreatedBy { set; get; }
        public string PaymentStatus { set; get; }
        public bool Status { set; get; }

        public string CustomerId { set; get; }

        public string BankCode { set; get; }

        public IEnumerable<OrderDetailVm> OrderDetails { set; get; }
    }
}
