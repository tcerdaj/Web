using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.Entities
{
	public class DonationDetails: EntityBase<Guid>
	{
		public DonationDetails()
		{
			this.Images = new List<DonationImage>();
		}
		public virtual int Line { get; set; }
		public virtual Donation Donation { get; set; }
		public virtual DonationType ItemType { get; set; }
		public virtual User RequestedBy { get; set; }
		public virtual string ItemName { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
		public virtual ICollection<DonationImage> Images { get; set; }
	}
}
