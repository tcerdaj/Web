using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Core.EntitiesDto
{
	public class DonationDetailsDto : EntityBase<Guid>
	{
		public virtual int Index { get; set; }
		public virtual DonationDto Donation { get; set; }
		public virtual DonationType ItemType { get; set; }
		public virtual string ItemName { get; set; }
		public virtual string ImageUrl { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
	}
}
