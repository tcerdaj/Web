using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Core.EntitiesDto
{
	public class DonationDto: EntityBaseDto<int>
	{
		public DonationDto()
		{
			this.DonationDetails = new List<DonationDetailsDto>();
		}

		public virtual User RequestedBy { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime ExpireOn { get; set; }
		public virtual DateTime DonatedOn { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
        public virtual int RequestId { get; set; }

        public virtual ICollection<DonationDetailsDto> DonationDetails { get; set; }
	}
}
