using AutoMapper;
using FashionShop.Common;
using FashionShop.Model.Models;
using FashionShop.Service;
using FashionShop.Web.App_Start;
using FashionShop.Web.Infrastructure.Core;
using FashionShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace FashionShop.Web.Api
{
    [RoutePrefix("api/carts")]
    public class CartsController : ApiControllerBase
    {
        private readonly IProductService _productService;
        public CartsController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            _productService = productService;            
        }

        [Route("add-to-cart/{productId}")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage AddToCart(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {
               
                var cart = (List<ShoppingCartViewModel>)HttpContext.Current.Session[CommonConstants.SessionCart];
                if (cart == null)
                {
                    cart = new List<ShoppingCartViewModel>();
                }
                if (cart.Any(x => x.ProductId == productId))
                {
                    foreach (var item in cart)
                    {
                        if (item.ProductId == productId)
                        {
                            item.Quantity += 1;
                        }
                    }
                }
                else
                {
                    ShoppingCartViewModel newItem = new ShoppingCartViewModel();
                    newItem.ProductId = productId;
                    var product = _productService.GetById(productId);
                    newItem.Product = Mapper.Map<Product, ProductViewModel>(product);
                    newItem.Quantity = 1;
                    cart.Add(newItem);
                }

                HttpContext.Current.Session[CommonConstants.SessionCart] = cart;

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, cart);

                return response;
            });
        }

        [Route("update-cart")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage UpdateCart(HttpRequestMessage request, UpdateCartViewModel cartData)
        {
            return CreateHttpResponse(request, () =>
            {

                var cartSession = (List<ShoppingCartViewModel>)HttpContext.Current.Session[CommonConstants.SessionCart];
                foreach (var item in cartSession)
                {
                    if (item.ProductId == cartData.ProductId)
                    {
                        item.Quantity = cartData.Quantity;
                    }                   
                }

                HttpContext.Current.Session[CommonConstants.SessionCart] = cartSession;
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, cartSession);
                return response;
            });
        }

        [Route("delete-item/{productId}")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteItem(HttpRequestMessage request, int productId)
        {
            return CreateHttpResponse(request, () =>
            {

                var cartSession = (List<ShoppingCartViewModel>)HttpContext.Current.Session[CommonConstants.SessionCart];
                if (cartSession != null)
                {
                    cartSession.RemoveAll(x => x.ProductId == productId);
                    HttpContext.Current.Session[CommonConstants.SessionCart] = cartSession;
                    return request.CreateResponse(HttpStatusCode.OK,productId);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest);
            });
        }

        [Route("delete-all")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpContext.Current.Session[CommonConstants.SessionCart] = new List<ShoppingCartViewModel>();
                bool stastus = true;
                return request.CreateResponse(HttpStatusCode.OK, stastus);
            });
        }

    }
}
