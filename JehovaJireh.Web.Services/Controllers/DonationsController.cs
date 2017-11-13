using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JehovaJireh.Core.Entities;
using JehovaJireh.Core.EntitiesDto;
using JehovaJireh.Core.IRepositories;
using JehovaJireh.Logging;
using JehovaJireh.Data.Mappings;
using Omu.ValueInjecter;

namespace JehovaJireh.Web.Services.Controllers
{
    public class DonationsController : BaseController<Donation, DonationDto,int>
    {
		IDonationRepository repository;
		ILogger log;

		public DonationsController(IDonationRepository repository, ILogger log)
			:base(repository, log)
		{
			this.repository = repository;
			this.log = log;
		}

		public override IEnumerable<DonationDto> Get()
		{
            List<DonationDto> dto = new List<DonationDto>();
            try
            {
                var dta = repository.Query().ToList();

                foreach (var item in dta)
                {
                    var donation = new DonationDto
                    {
                        Id = item.Id,
                        Title = item.Title,
                        Amount = item.Amount,
                        Description = item.Description,
                        RequestedBy = item.RequestedBy,
                        DonatedOn = item.DonatedOn,
                        DonationStatus = item.DonationStatus,
                        ExpireOn = item.ExpireOn,
                        CreatedBy = new UserDto
                        {
                            Id = item.CreatedBy.Id,
                            UserName = item.CreatedBy.UserName,
                            FirstName = item.CreatedBy.FirstName,
                            LastName = item.CreatedBy.LastName,
                            Email = item.CreatedBy.Email,
                            PhoneNumber = item.CreatedBy.PhoneNumber,
                            Address = item.CreatedBy.Address,
                            ImageUrl = item.CreatedBy.ImageUrl
                        },
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
                            Donation = new DonationDto
                            {
                                Id = line.Donation.Id,
                                Description = line.Donation.Description,
                                Title = line.Donation.Title,
                                Amount = line.Donation.Amount,
                                ExpireOn = line.Donation.ExpireOn,
                                RequestedBy =new UserDto
                                {
                                    Id = line.Donation.RequestedBy.Id,
                                    UserName = line.Donation.RequestedBy.UserName,
                                    FirstName = line.Donation.RequestedBy.FirstName,
                                    LastName = line.Donation.RequestedBy.LastName,
                                    Email = line.Donation.RequestedBy.Email,
                                    PhoneNumber = line.Donation.RequestedBy.PhoneNumber,
                                    Address = line.Donation.RequestedBy.Address,
                                    ImageUrl = line.Donation.RequestedBy.ImageUrl,
                                },
                                CreatedBy = new UserDto
                                {
                                    Id = line.CreatedBy.Id,
                                    UserName = line.Donation.CreatedBy.UserName,
                                    FirstName = line.Donation.CreatedBy.FirstName,
                                    LastName = line.Donation.CreatedBy.LastName,
                                    Email = line.Donation.CreatedBy.Email,
                                    PhoneNumber = line.Donation.CreatedBy.PhoneNumber,
                                    Address = line.Donation.CreatedBy.Address,
                                    ImageUrl = line.Donation.CreatedBy.ImageUrl,
                                },
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
                            RequestedBy = new UserDto { 
                                Id = line.RequestedBy.Id,
                                UserName = line.RequestedBy.UserName,
                                FirstName =line.RequestedBy.FirstName,
                                LastName = line.RequestedBy.LastName,
                                Email = line.RequestedBy.Email,
                                PhoneNumber = line.RequestedBy.PhoneNumber,
                                Address = line.RequestedBy.Address,
                                ImageUrl = line.RequestedBy.ImageUrl,
                            },
                            CreatedBy = new UserDto
                            {
                                Id = line.CreatedBy.Id,
                                UserName = line.CreatedBy.UserName,
                                FirstName = line.CreatedBy.FirstName,
                                LastName = line.CreatedBy.LastName,
                                Email = line.CreatedBy.Email,
                                PhoneNumber = line.CreatedBy.PhoneNumber,
                                Address = line.CreatedBy.Address,
                                ImageUrl = line.CreatedBy.ImageUrl,
                            },
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
                                    CreatedBy = new UserDto
                                    {
                                        Id = image.Item.CreatedBy.Id,
                                        UserName = image.Item.CreatedBy.UserName,
                                        FirstName = image.Item.CreatedBy.FirstName,
                                        LastName = image.Item.CreatedBy.LastName,
                                        Email = image.Item.CreatedBy.Email,
                                        PhoneNumber = image.Item.CreatedBy.PhoneNumber,
                                        Address = image.Item.CreatedBy.Address,
                                        ImageUrl = image.Item.CreatedBy.ImageUrl
                                    },
                                    Donation = new DonationDto { Id = image.Item.Donation.Id },
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
                                    RequestedBy = new UserDto {
                                            Id = image.Item.RequestedBy.Id,
                                            UserName = image.Item.RequestedBy.UserName,
                                            FirstName = image.Item.RequestedBy.FirstName,
                                            LastName = image.Item.RequestedBy.LastName,
                                            Email = image.Item.RequestedBy.Email,
                                            PhoneNumber = image.Item.RequestedBy.PhoneNumber,
                                            Address = image.Item.RequestedBy.Address,
                                            ImageUrl = image.Item.RequestedBy.ImageUrl
                                    }
                                },
                                ImageUrl = image.ImageUrl,
                                CreatedBy = new UserDto
                                {
                                    Id = image.CreatedBy.Id,
                                    UserName = image.CreatedBy.UserName,
                                    FirstName = image.CreatedBy.FirstName,
                                    LastName = image.CreatedBy.LastName,
                                    Email = image.CreatedBy.Email,
                                    PhoneNumber = image.CreatedBy.PhoneNumber,
                                    Address = image.CreatedBy.Address,
                                    ImageUrl = image.CreatedBy.ImageUrl
                                },
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

        [HttpGet]
        [Route("Donations/RequestedBy/{id}")]
        public IEnumerable<DonationDto> RequestedBy(int id)
        {
            IEnumerable<DonationDto> dto;
            try
            {
                var dta = repository.Query().Where(x => x.RequestedBy.Id == id).ToList();
                dto = dta.Select(x => new DonationDto()
                                .InjectFrom<DeepCloneInjection>(x))
                                .Cast<DonationDto>()
                                .ToList();
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return dto;
        }
    }
}
