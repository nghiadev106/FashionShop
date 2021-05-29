using FashionShop.Data.Infrastructure;
using FashionShop.Data.Repositories;
using FashionShop.Model.Models;
using FashionShop.Model.ViewModels;
using System;
using System.Collections.Generic;

namespace FashionShop.Service
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        bool Create(Order order, List<OrderDetail> orderDetails);

        Order GetOrderByCustomerEmail(string email);
        int GetCountOrder();

        IEnumerable<Order> GetAll(string keyword);
        IEnumerable<OrderVm> GetDetail(int orderId);
    }
    public class OrderService : IOrderService
    {
        IOrderRepository _orderRepository;
        IOrderDetailRepository _orderDetailRepository;
        IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IUnitOfWork unitOfWork)
        {
            this._orderRepository = orderRepository;
            this._orderDetailRepository = orderDetailRepository;
            this._unitOfWork = unitOfWork;
        }
        public bool Create(Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();

                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                _unitOfWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Order> GetAll()
        {
            return _orderRepository.GetAll();
        }

        public IEnumerable<Order> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _orderRepository.GetMulti(x => x.CustomerName.Contains(keyword) || x.CustomerEmail.Contains(keyword) || x.CustomerMobile.Contains(keyword));
            else
                return _orderRepository.GetAll();
        }

        public int GetCountOrder()
        {
            return _orderRepository.GetCountOrder();
        }

        public IEnumerable<OrderVm> GetDetail(int orderId)
        {
            return _orderRepository.GetDetail(orderId);
        }

        public Order GetOrderByCustomerEmail(string email)
        {
            return _orderRepository.GetSingleByCondition(x => x.CustomerEmail==email);
        }
    }
}
