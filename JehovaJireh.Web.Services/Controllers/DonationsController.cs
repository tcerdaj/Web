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
using System.Web.Http.Cors;
using NHibernate;
using NHibernate.Linq;

namespace JehovaJireh.Web.Services.Controllers
{
    public class DonationsController : BaseController<Donation, DonationDto,int>
    {
		IDonationRepository repository;
		ILogger log;
        ISession session;

		public DonationsController(IDonationRepository repository, ILogger log, ISession session)
			:base(repository, log)
		{
			this.repository = repository;
			this.log = log;
            this.session = session;
		}

        [ActionName("RequestedBy")]
        [HttpGet]
        public IEnumerable<DonationRequestedDto> RequestedBy(int id)
        {
            IEnumerable<DonationRequestedDto> dto;
            try
            {
                var dta = session.Query<DonationRequested>().Where(x => x.RequestedBy.Id == id).ToList();
                dto = dta.Select(x => new DonationRequestedDto()
                                .InjectFrom<DeepCloneInjection>(x))
                                .Cast<DonationRequestedDto>()
                                .ToList();
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return dto;
        }

        [ActionName("UpdateStatusRequested")]
        [HttpPut]
        public IHttpActionResult UpdateStatusRequested(string id, DonationDto dto)
        {
            try
            {
                var data = repository.GetById(dto.Id);
                //data.InjectFrom<DeepCloneInjection>(dto);
                Guid itemId;
                Guid.TryParse(id, out itemId);
                var modifiedOn = DateTime.Now;

                if (data != null)
                {
                    var itemsCount = data.DonationDetails.Count();
                    var requestedCount = data.DonationDetails.Where(x => x.DonationStatus == DonationStatus.Requested).Count() + 1;

                    if (itemsCount > 0 && requestedCount > 0 & itemsCount == requestedCount)
                    {
                        data.DonationStatus = DonationStatus.Requested;
                        data.RequestedBy = new User { Id = dto.RequestedBy.Id };
                        data.ModifiedBy = new User { Id = dto.RequestedBy.Id };
                        data.ModifiedOn = modifiedOn;
                    }
                    else
                    {
                        data.DonationStatus = DonationStatus.PartialRequested;
                        data.ModifiedBy = new User { Id = dto.RequestedBy.Id };
                    }

                    if (data.DonationDetails != null && itemId != Guid.Empty)
                    {
                        var item = data.DonationDetails.FirstOrDefault(x => x.Id == itemId);

                        if (item != null)
                        {
                            item.DonationStatus = DonationStatus.Requested;
                            item.RequestedBy = new User { Id = dto.RequestedBy.Id };
                            item.ModifiedBy = new User { Id = dto.RequestedBy.Id };
                            item.ModifiedOn = modifiedOn;

                            //data.DonationDetails.Where(x => x.Id == itemId).Select(p => p == item);
                        }
                    }
                }
                
                repository.Update(data);
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return Ok(dto);
        }
    }
}
