using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JehovaJireh.Core.Entities;
using JehovaJireh.Web.UI.App_GlobalResources;


namespace JehovaJireh.Web.UI.Models
{
	public class DonationViewModels
	{

		public DonationViewModels()
		{
			this.DonationDetails = new List<DonationDetailsViewModels>();
		}
		public int Id { get; set; }

		[Display(Name = "Title", ResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "TitleRequired")]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "C50Long")]
		public string Title { get; set; }


		[Required(ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "DescriptionRequired")]
		[Display(Name = "Description", ResourceType = typeof(Resources))]
		[StringLength(180, ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "C180Long")]
		public string Description { get; set; }

		[Display(Name = "Amount", ResourceType = typeof(Resources))]
		[DataType(DataType.Currency, ErrorMessage = null, ErrorMessageResourceName = "AmountInvalid", ErrorMessageResourceType = typeof(Resources))]
		public decimal Amount { get; set; }

		[Display(Name = "IsMoney", ResourceType = typeof(Resources))]
		public bool IsMoney { get; set; }


		[Display(Name = "ExpireOn", ResourceType = typeof(Resources))]
		[DataType(DataType.Date, ErrorMessage = null, ErrorMessageResourceName = "DateInvalid", ErrorMessageResourceType = typeof(Resources))]
		public DateTime ExpireOn { get; set; }


		[Display(Name = "CreatedOn", ResourceType = typeof(Resources))]
		public DateTime CreatedOn { get; set; }


		[Display(Name = "DonationStatus", ResourceType = typeof(Resources))]
		public DonationStatus DonationStatus { get; set; }


		[Display(Name = "DonationDetails", ResourceType = typeof(Resources))]
		public ICollection<DonationDetailsViewModels> DonationDetails { get; set; }
		public IEnumerable<SelectListItem> ItemTypes
		{
			get
			{
				return Enum.GetValues(typeof(DonationType))
					.Cast<DonationType>()
					.Select(
						 enu => new SelectListItem() { Text = enu.ToString(), Value = ((int)enu).ToString() }
					 )
					.ToList();
			}
		}
	}



	public class DonationDetailsViewModels
	{
		public DonationDetailsViewModels()
		{
			this.Images = new List<ImageViewModel>();
		}
		public Guid Id { get; set; }
		public int Index { get; set; }
		public Donation Donation { get; set; }

		[Display(Name = "ItemType", ResourceType = typeof(Resources))]
		public DonationType ItemType { get; set; }

		[Display(Name = "ItemName", ResourceType = typeof(Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "C50Long")]
		public string ItemName { get; set; }

		[Display(Name = "ImageUrl", ResourceType = typeof(Resources))]
		public string ImageUrl { get; set; }

		public IEnumerable<FileViewModel> MultiFileData { get; set; }

		public List<ImageViewModel> Images { get; set; }

		[Display(Name = "DonationStatus", ResourceType = typeof(Resources))]
		public DonationStatus DonationStatus { get; set; }

		[Display(Name = "WantThis", ResourceType = typeof(Resources))]
		public bool WantThis { get; set; }


	}


	public class FileViewModel
	{
		public string Name { get; set; }
        public long LastModified { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string WebkitRelativePath { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
	}
	public class ImageViewModel
	{
		public DonationDetailsViewModels Item { get; set; }
		public virtual string ImageUrl { get; set; }
	}

	public class RequestViewModels
	{
		public int Id { get; set; }

		[Display(Name = "Title", ResourceType = typeof(Resources))]
		[Required(ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "TitleRequired")]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "C50Long")]
		public string Title { get; set; }

		[Display(Name = "Description", ResourceType = typeof(Resources))]
		[StringLength(180, ErrorMessageResourceType = typeof(Resources),
			  ErrorMessageResourceName = "C180Long")]
		public string Description { get; set; }

		[Display(Name = "Amount", ResourceType = typeof(Resources))]
		[DataType(DataType.Currency, ErrorMessage = null, ErrorMessageResourceName = "AmountInvalid", ErrorMessageResourceType = typeof(Resources))]
		public string Amount { get; set; }

		[Display(Name = "ExpireOn", ResourceType = typeof(Resources))]
		[DataType(DataType.Date, ErrorMessage = null, ErrorMessageResourceName = "DateInvalid", ErrorMessageResourceType = typeof(Resources))]
		public DateTime ExpireOn { get; set; }

		[Display(Name = "DonationStatus", ResourceType = typeof(Resources))]
		public DonationStatus DonationStatus { get; set; }

		[Display(Name = "DonationDetails", ResourceType = typeof(Resources))]
		public ICollection<DonationDetailsViewModels> DonationDetails { get; set; }
	}
}