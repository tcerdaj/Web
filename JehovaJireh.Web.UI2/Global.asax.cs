using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JehovaJireh.Web.UI.Plumbing;

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

			container = BootstrapContainer();
			//initialize controller situation
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);

		}

		public static IWindsorContainer BootstrapContainer()
		{
			if (container != null)
			{
				return container;
			}

			return container = new WindsorContainer()
				.Install(FromAssembly.This(), FromAssembly.Containing<JehovaJireh.Infrastructure.Installers.RepositoryInstaller>());
		}

		protected void Application_End()
		{
			container.Dispose();
		}
	}
}
