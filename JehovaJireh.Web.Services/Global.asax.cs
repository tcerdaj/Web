using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;
using JehovaJireh.Infrastructure.Installers;

namespace JehovaJireh.Web.Services
{
	public class WebApiApplication : System.Web.HttpApplication
	{

		private static IWindsorContainer container;

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
			GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

			//set the IoC container
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
				.Install(FromAssembly.This(),
				FromAssembly.Containing<RepositoryInstaller>());
		}


		protected void Application_End()
		{
			container.Dispose();
		}
	}
}
