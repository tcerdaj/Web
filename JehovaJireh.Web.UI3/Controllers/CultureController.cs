using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JehovaJireh.Web.UI.Helpers;

namespace JehovaJireh.Web.UI.Controllers
{
    public class CultureController : Controller
    {
		// GET: Culture
		public ActionResult Index()
		{
			return RedirectToAction("Index", "Home");
		}

		
	}
}