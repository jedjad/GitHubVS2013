﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShoppingStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "",              //no parameters
                new
                {
                    controller = "Product",
                    action = "List",
                    category = (string) null,
                    page = 1
                });

            routes.MapRoute(null, "Page{page}",     //only with page number
                new
                {
                    controller = "Product",
                    action = "List",
                    category =
                        (string)null
                },     
                new { page = @"\d+"});              //means any number of decimal

            routes.MapRoute(null,                   //only with category
                "{category}",
                new { controller = "Product", action = "List", page=1},
                new {page = @"\d+"}
                );

            routes.MapRoute(null,                   //category with page
                "{category}/Page{page}",            //e.q. /Women/Page1
                new { controller = "Product", action = "List" },
                new {page = @"\d+"}
                );

            routes.MapRoute(null, "{controller}/{action}");

        }
    }
}
