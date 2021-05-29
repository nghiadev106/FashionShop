using FashionShop.Service;
using FashionShop.Web.Infrastructure.Core;
using System.Net.Http;
using System.Web.Http;
using FashionShop.Web.Models;
using System.Net;
using FashionShop.Web.App_Start;
using System.Linq;

namespace FashionShop.Web.Api
{
    [RoutePrefix("api/home")]
    //[Authorize]
    public class HomeController : ApiControllerBase
    {
        IErrorService _errorService;
        private IProductService _productService;
        private IOrderService _orderService;
        private IPostService _postService;
        private readonly ApplicationUserManager _userManager;

        public HomeController(IErrorService errorService,
            IProductService productService, IOrderService orderService,
            IPostService postService, ApplicationUserManager userManager) : base(errorService)
        {
            _errorService = errorService;
            _productService = productService;
            _orderService = orderService;
            _postService = postService;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("test")]
        public string TestMethod()
        {
            return "Hello";
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                DashboardViewModel model = new DashboardViewModel();
                model.PostCount = _productService.GetCount();
                model.OrderCount = _orderService.GetCountOrder();
                model.PostCount = _postService.GetCountPost();
                model.UserCount = _userManager.Users.Count();
                var response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}
