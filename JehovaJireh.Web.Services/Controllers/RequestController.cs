using JehovaJireh.Core.EntitiesDto;
using JehovaJireh.Core.IRepositories;
using JehovaJireh.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Omu.ValueInjecter;
using JehovaJireh.Data.Mappings;
using System.Web.Http;

namespace JehovaJireh.Web.Services.Controllers
{
    
    public class RequestController: BaseController<JehovaJireh.Core.Entities.Request, RequestDto, int>
    {
        IRequestRepository repository;
        ILogger log;
        public RequestController(IRequestRepository repository, ILogger log)
           :base(repository, log)
        {
            this.repository = repository;
            this.log = log;
        }

        [Route("Request/users/{id}")]
        public IEnumerable<RequestDto> GetByUser(int id)
        {
            IEnumerable<RequestDto> dto;
            try
            {
                var dta = repository.Query().Where(x => x.CreatedBy.Id == id).ToList();
                dto = dta.Select(x => new RequestDto()
                                .InjectFrom<DeepCloneInjection>(x))
                                .Cast<RequestDto>()
                                .ToList();
            }
            catch (System.Exception ex)
            {
                log.Error(ex);
                throw ex;
            }

            return dto;
        }

        [Route("Request/users")]
        public IEnumerable<RequestDto> GetByUser()
        {
            IEnumerable<RequestDto> dto;
            int thisYear = DateTime.Now.Year;
            try
            {
                var dta = repository.Query().Where(x => x.CreatedOn.Year == thisYear && x.DonationStatus == Core.Entities.DonationStatus.Created).ToList();
                dto = dta.Select(x => new RequestDto()
                                .InjectFrom<DeepCloneInjection>(x))
                                .Cast<RequestDto>()
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