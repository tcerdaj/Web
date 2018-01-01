﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Core.EntitiesDto
{
	public class DonationImageDto : EntityBaseDto<int>
	{
		public virtual DonationRefDto Donation { get; set; }
		public virtual string ImageUrl { get; set; }
	}
}
