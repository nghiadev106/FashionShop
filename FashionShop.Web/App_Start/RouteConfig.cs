using System.Web.Mvc;
using System.Web.Routing;

namespace FashionShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
              name: "Search",
              url: "tim-kiem",
              defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional }
           );

            routes.MapRoute(
             name: "Contact",
             url: "lien-he",
             defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "Cart",
             url: "gio-hang",
             defaults: new { controller = "ShoppingCart", action = "Index", id = UrlParameter.Optional }
          );

            routes.MapRoute(
             name: "DetailProduct",
             url: "san-pham/{alias}/{id}",
             defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional }
         );

            routes.MapRoute(
             name: "DetailPost",
             url: "tin-tuc/{alias}/{id}",
             defaults: new { controller = "Post", action = "Detail", id = UrlParameter.Optional }
         );

            routes.MapRoute(
              name: "ProductMale",
              url: "san-pham-nam",
              defaults: new { controller = "Product", action = "ProductMale", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "ProducFemale",
                url: "san-pham-nu",
                defaults: new { controller = "Product", action = "ProductFemale", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "Register",
               url: "dang-ky",
               defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
         );
            routes.MapRoute(
               name: "Login",
               url: "dang-nhap",
               defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
         );

            routes.MapRoute(
                name: "Product Category Male",
                url: "danh-muc/Nam/{alias}/{id}",
                defaults: new { controller = "Product", action = "CategoryMale", id = UrlParameter.Optional },
                namespaces: new string[] { "Fashion.Web.Controllers" }
         );

            routes.MapRoute(
                name: "Product Category Female",
                url: "danh-muc/Nu/{alias}/{id}",
                defaults: new { controller = "Product", action = "CategoryFemale", id = UrlParameter.Optional },
                namespaces: new string[] { "Fashion.Web.Controllers" }
         );

            routes.MapRoute(
               name: "Post",
               url: "tin-tuc",
               defaults: new { controller = "Post", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Home",
                url: "trang-chu",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
