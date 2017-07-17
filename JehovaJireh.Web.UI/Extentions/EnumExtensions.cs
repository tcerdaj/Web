﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;

namespace JehovaJireh.Web.UI.Extentions
{
	public static class EnumExtensions
	{
		public static string GetDescription(this Enum enumValue)
		{
			FieldInfo fi = enumValue.GetType().GetField(enumValue.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[])fi.GetCustomAttributes(
				typeof(DescriptionAttribute),
				false);

			if (attributes != null &&
				attributes.Length > 0)
				return attributes[0].Description;
			else
				return enumValue.ToString();
		}
	}
}