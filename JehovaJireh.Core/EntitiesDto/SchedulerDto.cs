using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.EntitiesDto
{
    public class SchedulerDto:EntityBaseDto<int>
    {
        public virtual DonationDto Donation { get; set; }
        public virtual Guid ItemId { get; set; }
        public virtual string Title { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string Description { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string RecurrenceId { get; set; }
        public virtual string RecurrenceException { get; set; }
        public virtual string RecurrenceRule { get; set; }
        public virtual bool IsAllDay { get; set; }
    }
}
