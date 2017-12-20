using JehovaJireh.Core.Entities;
using System;

namespace JehovaJireh.Core.EntitiesDto
{
	public class ItemDto: EntityBaseDto<Guid>
	{
		public virtual int Line { get; set; }
		public virtual DonationType ItemType { get; set; }
		public virtual UserDto RequestedBy { get; set; }
		public virtual string ItemName { get; set; }
		public virtual DonationStatus DonationStatus { get; set; }
    }
}
