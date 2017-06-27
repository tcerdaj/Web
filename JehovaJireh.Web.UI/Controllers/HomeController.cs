﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JehovaJireh.Web.UI.Controllers
{
	public class HomeController : BaseController
	{
		public ActionResult Index()
		{
			var r = Request.Cookies.Get("culture");
			if (r !=null && !string.IsNullOrEmpty(r.Value))
				SetCulture(r.Value);
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}