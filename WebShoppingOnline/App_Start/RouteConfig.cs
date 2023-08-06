using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebShoppingOnline
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
          name: "NewsDetails",
          url: "{alias}-new{id}",
          defaults: new { controller = "News", action = "Details", alias = UrlParameter.Optional, id = UrlParameter.Optional },
          namespaces: new[] { "WebShoppingOnline.Controllers" }
     );

            routes.MapRoute(
           name: "News",
           url: "tin-tuc",
           defaults: new { controller = "News", action = "Index" },
           namespaces: new[] { "WebShoppingOnline.Controllers" }
      );
            routes.MapRoute(
        name: "Map",
        url: "lien-he",
        defaults: new { controller = "Contact", action = "Index", alias = UrlParameter.Optional },
        namespaces: new[] { "WebShoppingOnline.Controllers" }
   );
            routes.MapRoute(
           name: "BaiViet",
           url: "post/{alias}",
           defaults: new { controller = "Artical", action = "Index", alias = UrlParameter.Optional },
           namespaces: new[] { "WebShoppingOnline.Controllers" }
      );
            routes.MapRoute(
            name: "CategoryProduct",
            url: "san-pham",
            defaults: new { controller = "Product", action = "Index" },
            namespaces: new[] { "WebShoppingOnline.Controllers" }
       );
            routes.MapRoute(
          name: "Checkout",
          url: "thanh-toan",
          defaults: new { controller = "ShoppingCart", action = "CheckOut" },
          namespaces: new[] { "WebShoppingOnline.Controllers" }
     );
            routes.MapRoute(
           name: "ShoppingCart",
           url: "gio-hang",
           defaults: new { controller = "ShoppingCart", action = "Index" },
           namespaces: new[] { "WebShoppingOnline.Controllers" }
      );
            routes.MapRoute(
            name: "Trang chủ",
            url: "trang-chu",
            defaults: new { controller = "Home", action = "Index", alias = UrlParameter.Optional },
            namespaces: new[] { "WebShoppingOnline.Controllers" }
        );

            routes.MapRoute(
         name: "DetailProduct",
         url: "detail/{alias}-p{id}",
         defaults: new { controller = "Product", action = "Detail", alias = UrlParameter.Optional, id = UrlParameter.Optional },
         namespaces: new[] { "WebShoppingOnline.Controllers" }
  );

            routes.MapRoute(
           name: "ProductCategory",
           url: "danh-muc-san-pham/{alias}/{id}",
           defaults: new { controller = "Product", action = "ProductOfCategory", alias = UrlParameter.Optional, id = UrlParameter.Optional },
           namespaces: new[] { "WebShoppingOnline.Controllers" }
    );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "WebShoppingOnline.Controllers" }
            );









        }
    }
}
