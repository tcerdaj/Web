using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Core.EntitiesDto
{
	public class DonationImageDto : EntityBase<int>
	{
		public virtual DonationDetailsDto Item { get; set; }
		public virtual string ImageUrl { get; set; }
	}
}
