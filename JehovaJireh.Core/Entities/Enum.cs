using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.Entities
{
	public enum Gender
	{
		Male,
		Female
	}

	public enum DonationStatus
	{
		Created,
		ReadyToPickUp,
		Canceled,
		Approved, //for money
		Completed
	}

	public enum DonationType
	{
		Money,
		Clothes,
		Furniture,
		Gardening,
		Other
	}
}
