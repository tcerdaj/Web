using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Web.UI.Models
{
	public class DonationViewModels
	{
		public int Id { get; set; }

		[Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
		[Required(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "TitleRequired")]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "C50Long")]
		public string Title { get; set; }

		[Display(Name = "Description", ResourceType = typeof(Resources.Resources))]
		[StringLength(180, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "C180Long")]
		public string Description { get; set; }

		[Display(Name = "Amount", ResourceType = typeof(Resources.Resources))]
		[DataType(DataType.Currency,ErrorMessage =null, ErrorMessageResourceName ="AmountInvalid", ErrorMessageResourceType = typeof(Resources.Resources))]
		public string Amount { get; set; }

		[Display(Name = "IsMoney", ResourceType = typeof(Resources.Resources))]
		public bool IsMoney { get; set; }

		[Display(Name = "ExpireOn", ResourceType = typeof(Resources.Resources))]
		[DataType(DataType.Date, ErrorMessage = null, ErrorMessageResourceName = "DateInvalid", ErrorMessageResourceType = typeof(Resources.Resources))]
		public DateTime ExpireOn { get; set; }

		[Display(Name = "DonationStatus", ResourceType = typeof(Resources.Resources))]
		public DonationStatus DonationStatus { get; set; }

		[Display(Name = "DonationDetails", ResourceType = typeof(Resources.Resources))]
		public ICollection<DonationDetailsViewModels> DonationDetails { get; set; }

	}

	public class DonationDetailsViewModels
	{
		public Guid Id { get; set; }
		public  int Index { get; set; }
		public  Donation Donation { get; set; }

		[Display(Name = "ItemType", ResourceType = typeof(Resources.Resources))]
		public  DonationType ItemType { get; set; }

		[Display(Name = "ItemName", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "C50Long")]
		public  string ItemName { get; set; }

		[Display(Name = "ImageUrl", ResourceType = typeof(Resources.Resources))]
		public  string ImageUrl { get; set; }

		[Display(Name = "DonationStatus", ResourceType = typeof(Resources.Resources))]
		public  DonationStatus DonationStatus { get; set; }
	}

	public class RequestViewModels
	{
		public int Id { get; set; }

		[Display(Name = "Title", ResourceType = typeof(Resources.Resources))]
		[Required(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "TitleRequired")]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "C50Long")]
		public string Title { get; set; }

		[Display(Name = "Description", ResourceType = typeof(Resources.Resources))]
		[StringLength(180, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "C180Long")]
		public string Description { get; set; }

		[Display(Name = "Amount", ResourceType = typeof(Resources.Resources))]
		[DataType(DataType.Currency, ErrorMessage = null, ErrorMessageResourceName = "AmountInvalid", ErrorMessageResourceType = typeof(Resources.Resources))]
		public string Amount { get; set; }

		[Display(Name = "ExpireOn", ResourceType = typeof(Resources.Resources))]
		[DataType(DataType.Date, ErrorMessage = null, ErrorMessageResourceName = "DateInvalid", ErrorMessageResourceType = typeof(Resources.Resources))]
		public DateTime ExpireOn { get; set; }

		[Display(Name = "DonationStatus", ResourceType = typeof(Resources.Resources))]
		public DonationStatus DonationStatus { get; set; }

		[Display(Name = "DonationDetails", ResourceType = typeof(Resources.Resources))]
		public ICollection<DonationDetailsViewModels> DonationDetails { get; set; }
	}
}