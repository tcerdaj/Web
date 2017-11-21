using JehovaJireh.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.EntitiesDto
{
    public class DonationDetailsImageDto
    {
        public virtual Guid ItemId { get; set; }
        public virtual int Line { get; set; }
        //public virtual RequestedByDto RequestedBy { get; set; }
        public virtual DonationType ItemType { get; set; }
        public virtual string ItemName { get; set; }
        public virtual DonationStatus DonationStatus { get; set; }
    }
}
