using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JehovaJireh.Web.UI.Models;

namespace JehovaJireh.Web.UI.Controllers
{
    public class DonationController : Controller
    {
		// GET: Donation list
		

		public ActionResult Index()
        {
			//TODO: 
			//Query donations
			//Inject in donations list model
			//return list to the view.
			var model = new List<DonationViewModels>();
			return View(model);
        }

		[Authorize]
		public ActionResult MakeADonation()
		{
			//TODO: Maybe show the last 5 donations list
			return View();
		}

		[HttpPost]
		public ActionResult MakeADonation(DonationViewModels model)
		{
			return View();
		}

		[Authorize]
		public ActionResult MakeNewRequest()
		{
			return View();
		}

		[HttpPost]
		public ActionResult MakeNewRequest(RequestViewModels model)
		{
			return View();
		}

		[Authorize]
		public ActionResult SchedulerDonation()
		{
			return View();
		}
	}
}