﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using JehovaJireh.Web.UI.Helpers;

namespace JehovaJireh.Web.UI.CustomAttributes
{
	public class CultureAwareControllerActivator : IControllerActivator
	{
		public IController Create(RequestContext requestContext, Type controllerType)
		{
			//Get the {language} parameter in the RouteData
			string language = requestContext.RouteData.Values["culture"] == null ?
			CultureHelper.GetDefaultCulture() : requestContext.RouteData.Values["culture"].ToString();

			//Get the culture info of the language code
			CultureInfo culture = CultureInfo.GetCultureInfo(language);
			Thread.CurrentThread.CurrentCulture = culture;
			Thread.CurrentThread.CurrentUICulture = culture;

			return DependencyResolver.Current.GetService(controllerType) as IController;
		}
	}
}