﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.EntitiesDto
{
    public class DonationDetailsImageRefDto: EntityBaseDto<int>
    {
        public virtual string ImageUrl { get; set; }
    }
}
