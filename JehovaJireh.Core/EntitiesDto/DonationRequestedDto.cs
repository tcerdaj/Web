using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.EntitiesDto
{
    public class DonationRequestedDto
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual DonationDto Donation { get; set; }
        public virtual DonationDetailsDto Item { get; set; }
        public virtual UserDto RequestedBy { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual bool IsAnItem { get { return Item != null; } }
    }
}
