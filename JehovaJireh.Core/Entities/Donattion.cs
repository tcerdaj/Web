using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.Entities
{
	public class Donattion: EntityBase<int>
	{
		public virtual string Title { get; set; }
		public virtual string Description { get; set; }
		public virtual decimal Amount { get; set; }
		public virtual  
		public virtual DateTime DonationDateUTC { get; set; }
	    public virtual bool IsDonationForMembers { get; set; }
		public virtual DateTime DonationExpiredDate { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
		public virtual DonationType DonationType { get; set; }

	}
}
