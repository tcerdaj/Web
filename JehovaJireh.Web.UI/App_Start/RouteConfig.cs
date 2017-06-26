using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JehovaJireh.Web.UI.Helpers;
using JehovaJireh.Web.UI2.Helpers;

namespace JehovaJireh.Web.UI
{
	public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapRoute(
			//	name: "DefaultCulture",
			//	url: "{culture}/{controller}/{action}/{id}",
			//	constraints: new { culture = new CultureConstraint(defaultCulture: "nl", pattern: "[a-z]{2}") },
			//	defaults: new { culture = CultureHelper.GetDefaultCulture(), controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			);
		}
    }
}
