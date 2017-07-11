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

namespace JehovaJireh.Web.Services.Controllers
{
    public class DonationsController : BaseController<Donation, DonationDto,int>
    {
		public DonationsController(IDonationRepository repository, ILogger log)
			:base(repository, log)
		{

		}
    }
}
