using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using JehovaJireh.Core.Entities;

namespace JehovaJireh.Web.UI.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

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
			  ErrorMessageResourceName = "EmailRequired"))]
        [EmailAddress]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resources))]
        public string Email { get; set; }

		[Display(Name = "User Name")]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string UserName { get; set; }

		[Display(Name = "Please enter your photo")]
		public string FileData{ get; set; }

		[Required]
		[Display(Name = "First Name")]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string FirstName { get; set; }

		[Required]
		[Display(Name = "Last Name")]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string LastName { get; set; }

		[Display(Name = "Gender")]
		[StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public Gender Gender { get; set; }

		[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordHash { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("PasswordHash", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

		[Display(Name = "Address")]
		[StringLength(80, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string Address { get; set; }

		[Display(Name = "City")]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string City { get; set; }

		[Display(Name = "State")]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string State { get; set; }

		[Display(Name = "Zip")]
		[StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string Zip { get; set; }

		[Display(Name = "Phone")]
		[Phone]
		[StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Is Church Member?")]
		public bool IsChurchMember { get; set; }

		[Display(Name = "Church Name")]
		[StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string ChurchName { get; set; }

		[Display(Name = "Church Address")]
		[StringLength(80, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string ChurchAddress { get; set; }

		[Phone]
		[Display(Name = "Church Phone")]
		[StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string ChurchPhone { get; set; }

		[Display(Name = "Church Pastor")]
		[StringLength(20, ErrorMessage = "The {0} must be at least {2} characters long.")]
		public string ChurchPastor { get; set; }

		[Display(Name = "Do you want us to visit you or call you?")]
		public bool NeedToBeVisited { get; set; }

		[Display(Name = "Your opinion is very important to us, please leave us your comments.")]
		public string Comments { get; set; }
	}

	public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
