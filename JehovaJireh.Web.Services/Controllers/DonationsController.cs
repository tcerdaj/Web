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
        

        [HttpGet]
        [Route("Donations/RequestedBy/{id}")]
        public IEnumerable<DonationDto> RequestedBy(int id)
        {
            IEnumerable<DonationDto> dto;
            try
            {
                var data = session.Query<DonationRequested>().ToList();
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
