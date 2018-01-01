using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Core.EntitiesDto
{
	public class DonationDetailsRefDto : EntityBaseDto<Guid>
	{
		public DonationDetailsRefDto()
		{
			this.Images = new List<DonationDetailsImageRefDto>();
		}

		public virtual int Line { get; set; }
		public virtual DonationRefDto Donation { get; set; }
		public virtual RequestedByDto RequestedBy { get; set; }
		public virtual DonationType ItemType { get; set; }
		public virtual string ItemName { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
		public virtual ICollection<DonationDetailsImageRefDto> Images { get; set; }
	}
}
