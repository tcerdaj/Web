using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.Entities
{
	public class DonationDetails: EntityBase<int>
	{
		public virtual int Index { get; set; }
		public virtual Donation Donation { get; set; }
		public virtual DonationType ItemType { get; set; }
		public virtual string ItemName { get; set; }
		public virtual string ImageUrl { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
	}
}
