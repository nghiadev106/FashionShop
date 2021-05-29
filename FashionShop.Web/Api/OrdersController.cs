using AutoMapper;
using FashionShop.Common;
using FashionShop.Model.Models;
using FashionShop.Service;
using FashionShop.Web.Infrastructure.Core;
using FashionShop.Web.Infrastructure.Extensions;
using FashionShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace FashionShop.Web.Api
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiControllerBase
    {
        IOrderService _orderService;

        public OrdersController(IErrorService errorService, IOrderService orderService) : base(errorService)
        {
            _orderService = orderService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 15)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _orderService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(query);

                var paginationSet = new PaginationSet<OrderViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _orderService.GetDetail(id);

                var response = request.CreateResponse(HttpStatusCode.OK, model);

                return response;
            });
        }

        [Route("create-order")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, OrderViewModel order)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var orderNew = new Order();
                orderNew.UpdateOrder(order);
                var sessionData = HttpContext.Current.Session[CommonConstants.SessionCart];
                var cart = (List<ShoppingCartViewModel>)sessionData;
                List<OrderDetail> orderDetails = new List<OrderDetail>();
                foreach (var item in cart)
                {
                    var detail = new OrderDetail();
                    detail.ProductID = item.ProductId;
                    detail.Quantitty = item.Quantity;
                    orderDetails.Add(detail);
                }
                _orderService.Create(orderNew, orderDetails);
                response = request.CreateResponse(HttpStatusCode.OK, order);
                return response;
            });
        }
    }
}
