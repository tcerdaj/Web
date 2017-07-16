using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Castle.Windsor;
using JehovaJireh.Data.Repositories;
using JehovaJireh.Logging;
using JehovaJireh.Web.UI.Models;
using Newtonsoft.Json;
using JehovaJireh.Web.UI.Extentions;
using JehovaJireh.Web.UI.App_GlobalResources;
using JehovaJireh.Core.IRepositories;
using JehovaJireh.Core.Entities;
using Omu.ValueInjecter;
using JehovaJireh.Data.Mappings;

namespace JehovaJireh.Web.UI.Controllers
{
    public class DonationsController : Controller
    {
		// GET: Donation list
		private ILogger log;
		private static IWindsorContainer container;
		private IDonationRepository donationRepository;
		private IUserRepository userRepository;

		public DonationsController()
		{
			container = MvcApplication.BootstrapContainer();
			log = container.Resolve<ILogger>();
			donationRepository = container.Resolve<IDonationRepository>();
			userRepository = container.Resolve<IUserRepository>();
		}
		public ActionResult Index(DonationMessageId? message)
        {
			ViewBag.StatusMessage =
					message == DonationMessageId.CrateDonationSuccess ? Resources.YourDonationHasBenCreated
					: message == DonationMessageId.Error ? Resources.Error
					: "";

			//TODO: 
			//Create kendo parent child grid.
			//Get webapi call
			//if message is null show all donation 
			//else if message is not null show the current user donations.
			
			return View();
        }

		[Authorize]
		[HttpGet]
		public ActionResult MakeADonation()
		{
			//TODO: Maybe show the last 5 donations list
			var model = new DonationViewModels();
			model.DonationDetails.Add(new DonationDetailsViewModels());
			return View(model);
		}

		[HttpPost]
		public async System.Threading.Tasks.Task<ActionResult> MakeADonation(DonationViewModels model)
		{
			//Setting donation details from formData
			//TODO:
			//Save to database
			try
			{
				var donationDetailsJson = Request.Params["details"];
			var donationDetails = JsonConvert.DeserializeObject<List<DonationDetailsViewModels>>(donationDetailsJson);
			model.DonationDetails = donationDetails;
			var imageService = new ImageService(log);
			ViewBag.StatusMessage = "";

				foreach (var line in model.DonationDetails)
				{
					List<string> filesAdded = new List<string>();
					foreach (var file in line.MultiFileData)
					{
						//check if the image already exist.
						var fileExist = filesAdded.Any(x => x == file.Name);
						if (!fileExist)
						{
							//upload image to azure
							var fileToUpload = file.GetFile(Request.Files);
							if (fileToUpload != null)
							{
								var imageUrl = await imageService.CreateUploadedImageAsync(fileToUpload, Guid.NewGuid().ToString(), true, 200, 200);
								line.Images.Add(new ImageViewModel() { Item = line, ImageUrl = imageUrl });
								filesAdded.Add(file.Name);
							}
						}
					}
				}

				//Class Injection
				var user = userRepository.GetByUserName(User.Identity.Name);
				var donation = new Donation {
					Amount = model.Amount,
					CreatedBy = user,
					CreatedOn = DateTime.Now,
					DonatedOn = DateTime.Now,
					Description = model.Description,
					DonationStatus = model.DonationStatus,
					ExpireOn= model.ExpireOn,
					Title = model.Title,
					ModifiedOn = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue
				};

				donation.DonationDetails = new List<DonationDetails>();

				foreach (var item in model.DonationDetails)
				{


					var itemLine = new DonationDetails
					{
						CreatedBy = user,
						Donation = donation,
						CreatedOn = DateTime.Now,
						ItemName = item.ItemName,
						ItemType = item.ItemType,
						Line = item.Index,
						DonationStatus = item.DonationStatus,
						ModifiedOn = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue
					};

					foreach (var image in item.Images)
					{
						itemLine.Images.Add(
							new DonationImage
							{
								CreatedBy = user,
								Item = itemLine,
								ImageUrl = image.ImageUrl,
								CreatedOn = DateTime.Now,
								ModifiedOn = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue
					});
					}

					donation.DonationDetails.Add(itemLine);
				}

				//save to database
				donationRepository.Create(donation);
				//Success
				return Json(new { result = "Redirect", url = Url.Action("Index", "Donations", new { Message = DonationMessageId.CrateDonationSuccess })});

			}
			catch (System.Exception ex)
			{
				ViewBag.StatusMessage = ex.Message;
			}


			return View(model);
		}

		[Authorize]
		public ActionResult MakeNewRequest()
		{
			return View(new RequestViewModels());
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

		#region Helpers	
		public enum DonationMessageId
		{
			CrateDonationSuccess,
			Error,
		}
		#endregion
	}
}