﻿using JehovaJireh.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.EntitiesDto
{
    public class DonationDetailsImageDto: EntityBaseDto<int>
    {
        public virtual DonationDetailsRefDto Item { get; set; }
        public virtual string ImageUrl { get; set; }
    }
}
