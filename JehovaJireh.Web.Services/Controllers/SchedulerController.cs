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
using NHibernate;
using NHibernate.Linq;

namespace JehovaJireh.Web.Services.Controllers
{
    public class SchedulerController : BaseController<Scheduler, SchedulerDto, int>
    {
        ISchedulerRepository repository;
        ILogger log;
        ISession session;

        public SchedulerController(ISchedulerRepository repository, ILogger log, ISession session)
			:base(repository, log)
        {
            this.repository = repository;
            this.log = log;
            this.session = session;
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

        public override IHttpActionResult Post(SchedulerDto dto)
        {
            try
            {
                Donation donation = null;
                Scheduler data = new Scheduler();
                data.InjectFrom<DeepCloneInjection>(dto);

                if (dto != null && dto.Donation != null)
                {
                    donation = session.Query<Donation>().FirstOrDefault(x => x.Id == dto.Donation.Id);
                    if (donation != null)
                    {
                        var itemsCount = donation.DonationDetails.Count();
                        var scheduledCount = donation.DonationDetails.Where(x => x.DonationStatus == DonationStatus.Scheduled).Count() + 1;

                        if (itemsCount > 0 && scheduledCount > 0 & itemsCount == scheduledCount)
                        {
                            data.Donation.DonationStatus = DonationStatus.Scheduled;
                        }
                        else
                            data.Donation.DonationStatus = DonationStatus.PartialScheduled;

                        if (dto.Item != null)
                        {
                            data.Donation.DonationDetails.Where(x => x.Id == dto.Item.Id).Select(y => y.DonationStatus == DonationStatus.Scheduled);
                        }
                    }
                }

                repository.Create(data);
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
