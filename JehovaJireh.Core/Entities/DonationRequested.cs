using JehovaJireh.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.EntitiesDto
{
    public class DonationRequested
    {
        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Donation Donation { get; set; }
        public virtual Guid ItemId { get; set; }
        public virtual User RequestedBy { get; set; }
        public virtual string Description { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual bool IsAnItem { get { return ItemId != Guid.Empty; } }
    }
}
