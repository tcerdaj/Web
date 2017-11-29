using JehovaJireh.Core.Entities;
using JehovaJireh.Core.EntitiesDto;
using JehovaJireh.Core.IRepositories;
using JehovaJireh.Data.Mappings;
using JehovaJireh.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Omu.ValueInjecter;

namespace JehovaJireh.Web.Services.Controllers
{
    public class SchedulerController : BaseController<Scheduler, SchedulerDto, int>
    {
        ISchedulerRepository repository;
        ILogger log;

        public SchedulerController(ISchedulerRepository repository, ILogger log)
			:base(repository, log)
        {
            this.repository = repository;
            this.log = log;
        }

        [ActionName("CreatedBy")]
        [HttpGet]
        public IEnumerable<SchedulerDto> CreatedBy(int id)
        {
            IEnumerable<SchedulerDto> dto;
            try
            {
                var dta = repository.Query().Where(x => x.CreatedBy.Id == id && 
                                     (x.Donation.DonationStatus == DonationStatus.Requested || x.Donation.DonationStatus == DonationStatus.PartialRequested
                                     || x.Item.DonationStatus == DonationStatus.Requested))
                                     .ToList();
                dto = dta.Select(x => new SchedulerDto()
                                .InjectFrom<DeepCloneInjection>(x))
                                .Cast<SchedulerDto>()
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
