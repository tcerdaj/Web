using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Core.EntitiesDto
{
	public class DonationDto: EntityBase<int>
	{
		public DonationDto()
		{
			this.DonationDetails = (ICollection<DonationDetails>)new List<DonationDetails>();
		}

		public virtual User RequestedBy { get; set; }
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual DateTime ExpireOn { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }

		public virtual ICollection<DonationDetails> DonationDetails { get; set; }
	}
}
