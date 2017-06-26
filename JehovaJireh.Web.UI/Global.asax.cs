using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace JehovaJireh.Web.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
		private static IWindsorContainer container;

		protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

		public static IWindsorContainer BootstrapContainer()
		{
			if (container != null)
			{
				return container;
			}

			return container = new WindsorContainer()
				.Install(FromAssembly.This(), FromAssembly.Containing<Infrastructure.Installers.RepositoryInstaller>());
		}

		protected void Application_End()
		{
			container.Dispose();
		}
	}
}
