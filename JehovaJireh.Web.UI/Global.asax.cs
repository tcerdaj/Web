using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JehovaJireh.Web.UI.CustomIAttribute;
using JehovaJireh.Web.UI.Helpers;

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
			ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(new CultureAwareControllerActivator()));
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

		protected void Application_AcquireRequestState(object sender, EventArgs e)
		{
			if (Request.Cookies["Culture"] != null && !string.IsNullOrEmpty(Request.Cookies["Culture"].Value))
			{
				string culture = Request.Cookies["culture"].Value;
				CultureInfo ci = new CultureInfo(culture);
				Thread.CurrentThread.CurrentUICulture = ci;
				Thread.CurrentThread.CurrentCulture = ci;
				
			}
		}
	}
}
