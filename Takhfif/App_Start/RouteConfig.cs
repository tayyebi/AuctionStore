using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Takhfif
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
                "Admin",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Download",
                "Download/{id}",
                new { controller = "Download", action = "Main", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "Weblog",
                "Weblog/{Username}/{action}/{id}",
                new { controller = "Weblog", Username = UrlParameter.Optional, action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                "ABNT",
                "ABNT/{Username}/{Action}",
                new { controller = "ABNT", Action = "Index", Username = "[anonymous]" }
            );

            routes.MapRoute(
                "Information",
                "Info/{id}",
                new { controller = "Information", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}/{option}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional, option = UrlParameter.Optional }
            );

        }
    }
}