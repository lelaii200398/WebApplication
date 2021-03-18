using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteTuDien
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
               name: "HomePost",
               url: "tin-tuc",
               defaults: new { controller = "Site", action = "HomePost", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "TatCaTinTuc",
               url: "tat-ca-tin-tuc",
               defaults: new { controller = "Site", action = "Post", id = UrlParameter.Optional }
           );
            routes.MapRoute(
               name: "LienHe",
               url: "lien-he",
              defaults: new { Controller = "Contacts", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "Search Product",
               url: "search",
               defaults: new { controller = "Site", action = "Search", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "TatCaSP",
                url: "tat-ca-san-pham",
                defaults: new { controller = "Site", action = "Product", id = UrlParameter.Optional }
            );



            routes.MapRoute(
                name: "SiteSlug",
                url: "{slug}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
