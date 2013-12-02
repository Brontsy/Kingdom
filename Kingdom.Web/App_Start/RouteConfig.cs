using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Kingdom.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(name: "ChangeTile", url: "tile/change/{tileType}", defaults: new { controller = "Tile", action = "Change" });
            routes.MapRoute(name: "GetRegion", url: "region/get/{x}-{y}", defaults: new { controller = "Region", action = "Get" });
            routes.MapRoute(name: "GetRegionRange", url: "region/get/range", defaults: new { controller = "Region", action = "GetRange" });
            routes.MapRoute(name: "GetVisibleRegions", url: "region/get/visible", defaults: new { controller = "Region", action = "GetVisibleRegions" });
            routes.MapRoute(name: "GetVisibleRegionIds", url: "region/get/visible/ids", defaults: new { controller = "Region", action = "GetVisibleRegionIds" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Tile", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}