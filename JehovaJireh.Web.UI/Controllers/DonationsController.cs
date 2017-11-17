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
using MvcSiteMapProvider;
using JehovaJireh.Core.EntitiesDto;
using System.Threading.Tasks;
using System.Net;

namespace JehovaJireh.Web.UI.Controllers
{
    public class DonationsController : BaseController
    {
		// GET: Donation list
		private ILogger log;
		private static IWindsorContainer container;
		private IDonationRepository donationRepository;
		private IUserRepository userRepository;
        private IRequestRepository requestRepository;
        User _user;

        private User CurrentUser
        {
            get
            {
               return _user;
            }

            set { _user = value; }
        }

        public DonationsController()
		{
			container = MvcApplication.BootstrapContainer();
			log = container.Resolve<ILogger>();
			donationRepository = container.Resolve<IDonationRepository>();
			userRepository = container.Resolve<IUserRepository>();
            requestRepository = container.Resolve<IRequestRepository>();
        }

		public ActionResult Index(DonationMessageId? message)
        {
			ViewBag.StatusMessage =
					message == DonationMessageId.CrateDonationSuccess ? Resources.YourDonationHasBenCreated
					: message == DonationMessageId.Error ? Resources.Error
					: "";
           
            CurrentUser = CurrentUser ?? userRepository.GetByUserName(User.Identity.Name);
            ViewBag.UserId = CurrentUser?.Id;

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
            if (!ModelState.IsValid) return View(model);

			try
			{
				var donationDetailsJson = Request.Params["details"];
				var donationDetails = JsonConvert.DeserializeObject<List<DonationDetailsViewModels>>(donationDetailsJson);
				model.DonationDetails = donationDetails;
				var imageService = new ImageService(log);
				ViewBag.StatusMessage = "";

                //Amount validation if money is on
                if (model.IsMoney && model.Amount <= 0)
                {
                    ViewBag.StatusMessage = Resources.InvalidAmount;
                    return View(model);
                }

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
								var imageUrl = await imageService.CreateUploadedImageAsync(fileToUpload, Guid.NewGuid().ToString(), true, 500, 500);
								line.Images.Add(new ImageViewModel() { Item = line, ImageUrl = imageUrl });
								filesAdded.Add(file.Name);
							}
						}
					}
				}

                //Class Injection
                CurrentUser = userRepository.GetByUserName(User.Identity.Name);
				var donation = new Donation {
					Amount = model.Amount,
					CreatedBy = CurrentUser,
					CreatedOn = DateTime.Now,
					DonatedOn = DateTime.Now,
					Description = model.Description,
					DonationStatus = (Core.Entities.DonationStatus)model.DonationStatus,
					ExpireOn= model.ExpireOn,
					Title = model.Title,
					ModifiedOn = null
				};

				donation.DonationDetails = new List<DonationDetails>();
                if (!model.IsMoney)
                {
                    foreach (var item in model.DonationDetails)
                    {


                        var itemLine = new DonationDetails
                        {
                            CreatedBy = CurrentUser,
                            Donation = donation,
                            CreatedOn = DateTime.Now,
                            ItemName = item.ItemName,
                            ItemType = (Core.Entities.DonationType)item.ItemType,
                            Line = item.Index,
                            DonationStatus = (Core.Entities.DonationStatus)item.DonationStatus,
                            ModifiedOn = null
                        };

                        foreach (var image in item.Images)
                        {
                            itemLine.Images.Add(
                                new DonationImage
                                {
                                    CreatedBy = CurrentUser,
                                    Item = itemLine,
                                    ImageUrl = image.ImageUrl,
                                    CreatedOn = DateTime.Now,
                                    ModifiedOn = null
                                });
                        }

                        donation.DonationDetails.Add(itemLine);
                    }
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

        [HttpPut]
        public ActionResult Update(int id, DonationDto model)
        {
            try
            {
                Console.WriteLine("Inside Update");
                if (model != null )
                {
                    var dta = donationRepository.Query().FirstOrDefault(x => x.Id == id);
                    dta.InjectFrom<DeepCloneInjection>(model);
                    var selected = dta.DonationDetails.Where(x => x.DonationStatus == Core.Entities.DonationStatus.Requested).Count();
                    var isRequested = model.DonationDetails != null || (selected + 1) == dta.DonationDetails.Count() ;
                    dta.DonationStatus = isRequested ? Core.Entities.DonationStatus.Requested : Core.Entities.DonationStatus.PartialRequested;

                    if (dta.DonationStatus == Core.Entities.DonationStatus.PartialRequested)
                    {
                        var itemId = model.DonationDetails.FirstOrDefault().Id;
                        var line = dta.DonationDetails.FirstOrDefault(x => x.Id == itemId);
                        line.RequestedBy = CurrentUser;
                        line.ModifiedBy = CurrentUser;
                        line.ModifiedOn = DateTime.Now;
                        line.DonationStatus = Core.Entities.DonationStatus.Requested;
                        dta.DonationDetails.Where(x => x.Id == itemId).Select(u => u = line);
                    }
                    donationRepository.Update(dta);
                    Response.StatusCode = (int)HttpStatusCode.OK;
                }
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { status = "error", data = ex.Message });
            }

            return Json(new { status = "ok", data = model });
        }

        public ActionResult RequestList()
        {
            ViewBag.UserId = CurrentUser?.Id;
            return View();
        }

		[Authorize]
		public ActionResult MakeNewRequest()
		{
			return View(new RequestViewModels());
		}

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async System.Threading.Tasks.Task<ActionResult> MakeNewRequest(RequestViewModels model)
		{
            if (!ModelState.IsValid) return View(model);

            try
            {
                
                //upload image to azure
                var imageService = new ImageService(log);
                var fileToUpload = model.ImageUpload;
                if (fileToUpload != null)
                {
                    var imageUrl = await imageService.CreateUploadedImageAsync(fileToUpload, Guid.NewGuid().ToString(), true, 500, 500);
                    model.ImageUrl = imageUrl;
                }

                Request data = new Request();
                data.InjectFrom<DeepCloneInjection>(model);
                data.CreatedBy = CurrentUser;
                data.CreatedOn = DateTime.Now;
                requestRepository.Create(data);
                return RedirectToAction("RequestList");
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError("error", ex.Message);
            }

            return View(model);
		}

		[Authorize]
		public ActionResult SchedulerDonation()
		{
			return View();
		}

        [Authorize]
        [HttpPost]
        public Task<DonationDto> RequestedBy(int donationId)
        {
            var dta = donationRepository.GetById(donationId);
            DonationDto dto = null;

            try
            {
                if (dta != null)
                {
                    dta.RequestedBy = CurrentUser;
                    dta.ModifiedBy = CurrentUser;
                    dta.ModifiedOn = DateTime.Now;
                    donationRepository.Update(dta);
                    dto = (DonationDto)new DonationDto().InjectFrom<DeepCloneInjection>(dta);
                }
            }
            catch (System.Exception)
            {
                throw;
            }

            return Task.FromResult<DonationDto>(dto);
        }

        #region Helpers	
        public enum DonationMessageId
		{
			CrateDonationSuccess,
			Error,
		}

        public IEnumerable<DonationDto> GetDonations()
        {
            List<DonationDto> dto = new List<DonationDto>();
            try
            {
                var dta = donationRepository.Query().ToList();

                foreach (var item in dta)
                {
                    var donation = new DonationDto
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Amount = item.Amount,
                        Description = item.Description,
                        RequestedBy = item.RequestedBy != null ? new RequestedByDto
                        {
                            Id = item.RequestedBy.Id,
                            UserName = item.RequestedBy.UserName,
                            FirstName = item.RequestedBy.FirstName,
                            LastName = item.RequestedBy.LastName,
                            Email = item.RequestedBy.Email,
                            PhoneNumber = item.RequestedBy.PhoneNumber,
                            Address = item.RequestedBy.Address,
                            ImageUrl = item.RequestedBy.ImageUrl
                        } : null,
                        DonatedOn = item.DonatedOn,
                        DonationStatus = item.DonationStatus,
                        ExpireOn = item.ExpireOn,
                        CreatedBy = item.CreatedBy != null ? new UserDto
                        {
                            Id = item.CreatedBy.Id,
                            UserName = item.CreatedBy.UserName,
                            FirstName = item.CreatedBy.FirstName,
                            LastName = item.CreatedBy.LastName,
                            Email = item.CreatedBy.Email,
                            PhoneNumber = item.CreatedBy.PhoneNumber,
                            Address = item.CreatedBy.Address,
                            ImageUrl = item.CreatedBy.ImageUrl
                        } : null,
                        CreatedOn = item.CreatedOn,
                        ModifiedBy = item.ModifiedBy != null ? new UserDto
                        {
                            Id = item.ModifiedBy.Id,
                            UserName = item.ModifiedBy.UserName,
                            FirstName = item.ModifiedBy.FirstName,
                            LastName = item.ModifiedBy.LastName,
                            Email = item.ModifiedBy.Email,
                            PhoneNumber = item.ModifiedBy.PhoneNumber,
                            Address = item.ModifiedBy.Address,
                            ImageUrl = item.ModifiedBy.ImageUrl
                        } : null,
                        ModifiedOn = item.ModifiedOn
                    };

                    foreach (var line in item.DonationDetails)
                    {

                        var donationItem = new DonationDetailsDto
                        {
                            Id = line.Id,
                            ItemType = line.ItemType,
                            ItemName = line.ItemName,
                            Line = line.Line,
                            DonationStatus = line.DonationStatus,
                            Donation = new DonationRefDto
                            {
                                Id = line.Donation.Id,
                                Description = line.Donation.Description,
                                Title = line.Donation.Title,
                                Amount = line.Donation.Amount,
                                ExpireOn = line.Donation.ExpireOn,
                                RequestedBy = line.Donation.RequestedBy != null ? new RequestedByDto
                                {
                                    Id = line.Donation.RequestedBy.Id,
                                    UserName = line.Donation.RequestedBy.UserName,
                                    FirstName = line.Donation.RequestedBy.FirstName,
                                    LastName = line.Donation.RequestedBy.LastName,
                                    Email = line.Donation.RequestedBy.Email,
                                    PhoneNumber = line.Donation.RequestedBy.PhoneNumber,
                                    Address = line.Donation.RequestedBy.Address,
                                    ImageUrl = line.Donation.RequestedBy.ImageUrl,
                                } : null,
                                CreatedBy = line.Donation.CreatedBy != null ? new UserDto
                                {
                                    Id = line.CreatedBy.Id,
                                    UserName = line.Donation.CreatedBy.UserName,
                                    FirstName = line.Donation.CreatedBy.FirstName,
                                    LastName = line.Donation.CreatedBy.LastName,
                                    Email = line.Donation.CreatedBy.Email,
                                    PhoneNumber = line.Donation.CreatedBy.PhoneNumber,
                                    Address = line.Donation.CreatedBy.Address,
                                    ImageUrl = line.Donation.CreatedBy.ImageUrl,
                                } : null,
                                CreatedOn = line.Donation.CreatedOn,
                                DonatedOn = line.Donation.DonatedOn,
                                ModifiedBy = line.Donation != null && line.Donation.ModifiedBy != null ? new UserDto
                                {
                                    Id = line.Donation.ModifiedBy.Id,
                                    UserName = line.Donation.ModifiedBy.UserName,
                                    FirstName = line.Donation.ModifiedBy.FirstName,
                                    LastName = line.Donation.ModifiedBy.LastName,
                                    Email = line.Donation.ModifiedBy.Email,
                                    PhoneNumber = line.Donation.ModifiedBy.PhoneNumber,
                                    Address = line.Donation.ModifiedBy.Address,
                                    ImageUrl = line.Donation.ModifiedBy.ImageUrl
                                } : null,
                                DonationStatus = line.Donation.DonationStatus,
                                ModifiedOn = line.Donation.ModifiedOn
                            },
                            RequestedBy = line.RequestedBy != null ? new RequestedByDto
                            {
                                Id = line.RequestedBy.Id,
                                UserName = line.RequestedBy.UserName,
                                FirstName = line.RequestedBy.FirstName,
                                LastName = line.RequestedBy.LastName,
                                Email = line.RequestedBy.Email,
                                PhoneNumber = line.RequestedBy.PhoneNumber,
                                Address = line.RequestedBy.Address,
                                ImageUrl = line.RequestedBy.ImageUrl,
                            } : null,
                            CreatedBy = line.CreatedBy != null ? new UserDto
                            {
                                Id = line.CreatedBy.Id,
                                UserName = line.CreatedBy.UserName,
                                FirstName = line.CreatedBy.FirstName,
                                LastName = line.CreatedBy.LastName,
                                Email = line.CreatedBy.Email,
                                PhoneNumber = line.CreatedBy.PhoneNumber,
                                Address = line.CreatedBy.Address,
                                ImageUrl = line.CreatedBy.ImageUrl,
                            } : null,
                            CreatedOn = line.CreatedOn,
                            ModifiedBy = line.ModifiedBy != null ? new UserDto
                            {
                                Id = line.ModifiedBy.Id,
                                UserName = line.ModifiedBy.UserName,
                                FirstName = line.ModifiedBy.FirstName,
                                LastName = line.ModifiedBy.LastName,
                                Email = line.ModifiedBy.Email,
                                PhoneNumber = line.ModifiedBy.PhoneNumber,
                                Address = line.ModifiedBy.Address,
                                ImageUrl = line.ModifiedBy.ImageUrl
                            } : null,
                            ModifiedOn = line.ModifiedOn
                        };

                        foreach (var image in line.Images)
                        {
                            donationItem.Images.Add(new DonationImageDto
                            {
                                Id = image.Id,
                                Item = new DonationDetailsDto
                                {
                                    Id = image.Item.Id,
                                    Line = image.Item.Line,
                                    ItemName = image.Item.ItemName,
                                    DonationStatus = image.Item.DonationStatus,
                                    CreatedOn = image.Item.CreatedOn,
                                    CreatedBy = image.Item.CreatedBy != null ? new UserDto
                                    {
                                        Id = image.Item.CreatedBy.Id,
                                        UserName = image.Item.CreatedBy.UserName,
                                        FirstName = image.Item.CreatedBy.FirstName,
                                        LastName = image.Item.CreatedBy.LastName,
                                        Email = image.Item.CreatedBy.Email,
                                        PhoneNumber = image.Item.CreatedBy.PhoneNumber,
                                        Address = image.Item.CreatedBy.Address,
                                        ImageUrl = image.Item.CreatedBy.ImageUrl
                                    } : null,
                                    Donation = new DonationRefDto { Id = image.Item.Donation.Id },
                                    ItemType = image.Item.ItemType,
                                    ModifiedBy = image.Item != null && image.Item.ModifiedBy != null ? new UserDto
                                    {
                                        Id = image.Item.ModifiedBy.Id,
                                        UserName = image.Item.ModifiedBy.UserName,
                                        FirstName = image.Item.ModifiedBy.FirstName,
                                        LastName = image.Item.ModifiedBy.LastName,
                                        Email = image.Item.ModifiedBy.Email,
                                        PhoneNumber = image.Item.ModifiedBy.PhoneNumber,
                                        Address = image.Item.ModifiedBy.Address,
                                        ImageUrl = image.Item.ModifiedBy.ImageUrl
                                    } : null,
                                    ModifiedOn = image.Item.ModifiedOn,
                                    RequestedBy = image.Item.RequestedBy != null ? new RequestedByDto
                                    {
                                        Id = image.Item.RequestedBy.Id,
                                        UserName = image.Item.RequestedBy.UserName,
                                        FirstName = image.Item.RequestedBy.FirstName,
                                        LastName = image.Item.RequestedBy.LastName,
                                        Email = image.Item.RequestedBy.Email,
                                        PhoneNumber = image.Item.RequestedBy.PhoneNumber,
                                        Address = image.Item.RequestedBy.Address,
                                        ImageUrl = image.Item.RequestedBy.ImageUrl
                                    } : null
                                },
                                ImageUrl = image.ImageUrl,
                                CreatedBy = image.CreatedBy != null ? new UserDto
                                {
                                    Id = image.CreatedBy.Id,
                                    UserName = image.CreatedBy.UserName,
                                    FirstName = image.CreatedBy.FirstName,
                                    LastName = image.CreatedBy.LastName,
                                    Email = image.CreatedBy.Email,
                                    PhoneNumber = image.CreatedBy.PhoneNumber,
                                    Address = image.CreatedBy.Address,
                                    ImageUrl = image.CreatedBy.ImageUrl
                                } : null,
                                CreatedOn = image.CreatedOn,
                                ModifiedOn = image.ModifiedOn,
                                ModifiedBy = image.ModifiedBy != null ? new UserDto
                                {
                                    Id = image.ModifiedBy.Id,
                                    UserName = image.ModifiedBy.UserName,
                                    FirstName = image.ModifiedBy.FirstName,
                                    LastName = image.ModifiedBy.LastName,
                                    Email = image.ModifiedBy.Email,
                                    PhoneNumber = image.ModifiedBy.PhoneNumber,
                                    Address = image.ModifiedBy.Address,
                                    ImageUrl = image.ModifiedBy.ImageUrl
                                } : null
                            });
                        }

                        donation.DonationDetails.Add(donationItem);
                    }

                    donation.DonationDetails.OrderBy(x => x.Line);
                    dto.Add(donation);
                }
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return dto;
        }
		#endregion
	}
}