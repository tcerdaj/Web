﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;

namespace JehovaJireh.Web.UI.Helpers
{
	public class CultureConstraint : IRouteConstraint
	{
		private readonly string defaultCulture;
		private readonly string pattern;

		public CultureConstraint(string defaultCulture, string pattern)
		{
			this.defaultCulture = defaultCulture;
			this.pattern = pattern;
		}

		public bool Match(
			HttpContextBase httpContext,
			Route route,
			string parameterName,
			RouteValueDictionary values,
			RouteDirection routeDirection)
		{
			if (routeDirection == RouteDirection.UrlGeneration &&
				this.defaultCulture.Equals(values[parameterName]))
			{
				return false;
			}
			else
			{
				return Regex.IsMatch((string)values[parameterName], "^" + pattern + "$");
			}
		}
	}
}