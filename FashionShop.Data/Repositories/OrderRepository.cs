using FashionShop.Data.Infrastructure;
using FashionShop.Model.Models;
using FashionShop.Model.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FashionShop.Data.Repositories
{
    public interface IOrderRepository  : IRepository<Order>
    {
        int GetCountOrder();
        IEnumerable<OrderVm> GetDetail(int orderId);
    }

    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public int GetCountOrder()
        {
            return DbContext.Orders.ToList().Count();
        }

        public IEnumerable<OrderVm> GetDetail(int orderId)
        {
            var query = (from o in DbContext.Orders
                        where o.ID == orderId
                        select new OrderVm
                        {
                            ID = o.ID,
                            CustomerName = o.CustomerName,
                            CustomerAddress = o.CustomerAddress,
                            CustomerEmail = o.CustomerEmail,
                            CustomerMessage=o.CustomerMessage,
                            CustomerMobile=o.CustomerMobile,
                            CreatedDate=o.CreatedDate,
                            CustomerId=o.CustomerId,
                            PaymentMethod=o.PaymentMethod,
                            Status=o.Status,
                            OrderDetails= (from od in DbContext.OrderDetails
                                           join p in DbContext.Products on od.ProductID equals p.ID
                                           where od.OrderID == o.ID
                                           select new OrderDetailVm
                                           {
                                               ProductName = p.Name,
                                               Price=p.Price,
                                               PromotionPrice=p.PromotionPrice,
                                               Image=p.Image,
                                               OrderID = od.OrderID,
                                              Quantitty=od.Quantitty
                                           })

                        });

            return query.ToList();
        }
    }
}