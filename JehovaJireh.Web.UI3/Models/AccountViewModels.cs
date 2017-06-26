using System.ComponentModel.DataAnnotations;

namespace JehovaJireh.Web.UI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

	public class RegisterViewModel
	{
		[Required(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "EmailRequired")]
		[EmailAddress(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "EmailInvalid")]
		[Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
		public string Email { get; set; }

		[Display(Name = "UserName", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "UserNameLong")]
		public string UserName { get; set; }

		[Display(Name = "FileData", ResourceType = typeof(Resources.Resources))]
		public string FileData { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "FirstNameRequired")]
		[Display(Name = "FirstName", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "FirstNameLong")]
		public string FirstName { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "LastNameRequired")]
		[Display(Name = "LastName", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "LastNameLong")]
		public string LastName { get; set; }

		[Display(Name = "Gender", ResourceType = typeof(Resources.Resources))]
		[StringLength(15, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "GenderLong")]
		public string Gender { get; set; }

		[Required(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PasswordRequired")]
		[StringLength(100, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PasswordLong", MinimumLength = 6)]
		[DataType(DataType.Password, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PasswordInvalid")]
		[Display(Name = "PasswordHash", ResourceType = typeof(Resources.Resources))]
		public string PasswordHash { get; set; }

		[DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(Resources.Resources))]
		[Compare("PasswordHash", ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PasswordCompare")]
		public string ConfirmPassword { get; set; }

		[Display(Name = "Address", ResourceType = typeof(Resources.Resources))]
		[StringLength(80, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "AddressLong")]
		public string Address { get; set; }

		[Display(Name = "City", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "CityLong")]
		public string City { get; set; }

		[Display(Name = "State", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "StateLong")]
		public string State { get; set; }

		[Display(Name = "Zip", ResourceType = typeof(Resources.Resources))]
		[StringLength(15, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "ZipLong")]
		public string Zip { get; set; }

		[Display(Name = "Phone", ResourceType = typeof(Resources.Resources))]
		[Phone(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PhoneInvalid")]
		[StringLength(15, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PhoneLong")]
		public string PhoneNumber { get; set; }

		[Display(Name = "IsChurchMember", ResourceType = typeof(Resources.Resources))]
		public bool IsChurchMember { get; set; }

		[Display(Name = "ChurchName", ResourceType = typeof(Resources.Resources))]
		[StringLength(50, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "ChurchNameLong")]
		public string ChurchName { get; set; }

		[Display(Name = "ChurchAddress", ResourceType = typeof(Resources.Resources))]
		[StringLength(80, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "ChurchAddressLong")]
		public string ChurchAddress { get; set; }

		[Phone(ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PhoneInvalid")]
		[Display(Name = "ChurchPhone", ResourceType = typeof(Resources.Resources))]
		[StringLength(15, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "PhoneLong")]
		public string ChurchPhone { get; set; }

		[Display(Name = "ChurchPastor", ResourceType = typeof(Resources.Resources))]
		[StringLength(20, ErrorMessageResourceType = typeof(Resources.Resources),
			  ErrorMessageResourceName = "ChurchPastorLong")]
		public string ChurchPastor { get; set; }

		[Display(Name = "NeedToBeVisited", ResourceType = typeof(Resources.Resources))]
		public bool NeedToBeVisited { get; set; }

		[Display(Name = "Comments", ResourceType = typeof(Resources.Resources))]
		public string Comments { get; set; }
	}
}
