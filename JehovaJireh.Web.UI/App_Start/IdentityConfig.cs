using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using JehovaJireh.Core.Entities;
using JehovaJireh.Data.Repositories;
using NHibernate;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using JehovaJireh.Logging;
using CSharpVitamins;

namespace JehovaJireh.Web.UI
{
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

	// Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.
	public class ApplicationUserManager : UserManager<User>
	{
		UserRepository userRepository;
		private static ISession _session;
		private static ILogger _log;

		public ApplicationUserManager(UserRepository store, ISession session, ILogger log)
			: base(store)
		{
			userRepository = store;
			_session = session;
			_log = log;
		}


		public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
		{
			var container = MvcApplication.BootstrapContainer();
			var session = container.Resolve<ISession>();
			var errorHandler = container.Resolve<ExceptionManager>();
			var manager = new ApplicationUserManager(new UserRepository(_session, errorHandler,_log), _session, _log);

			// Configure validation logic for usernames
			manager.UserValidator = new UserValidator<User>(manager)
			{
				AllowOnlyAlphanumericUserNames = false,
				RequireUniqueEmail = true
			};

			// Configure validation logic for passwords
			manager.PasswordValidator = new PasswordValidator
			{
				RequiredLength = 6,
				RequireNonLetterOrDigit = false,
				RequireDigit = false,
				RequireLowercase = true,
				RequireUppercase = true,
			};

			// Configure user lockout defaults
			manager.UserLockoutEnabledByDefault = true;
			manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
			manager.MaxFailedAccessAttemptsBeforeLockout = 5;

			// Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
			// You can write your own provider and plug it in here.
			manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<User>
			{
				MessageFormat = "Your security code is {0}"
			});
			manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<User>
			{
				Subject = "Security Code",
				BodyFormat = "Your security code is {0}"
			});
			manager.EmailService = new EmailService();
			manager.SmsService = new SmsService();
			var dataProtectionProvider = options.DataProtectionProvider;
			if (dataProtectionProvider != null)
			{
				manager.UserTokenProvider =
					new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
			}
			return manager;
		}

		public Task<User> UpdatetUserSettingsAsync(User user)
		{
			if (user == null)
				throw new ArgumentNullException("user");

			this.Update(user);
			return Task.FromResult(user);
		}

		public Task<string> CreateConfirmationTokenAsync()
		{
			return Task.FromResult<string>(ShortGuid.NewGuid().ToString());
		}

		public Task<User> GetByConfirmationTokennAsync(string token)
		{
			var user = (from u in userRepository.Query() where u.ConfirmationToken == token select u).SingleOrDefault();
			return Task.FromResult(user);
		}


		public override Task SendEmailAsync(string userId, string subject, string body)
		{
			EmailService email = new EmailService();
			var destination = this.FindById(userId).Email;
			IdentityMessage message = new IdentityMessage()
			{
				Body = body,
				Subject = subject,
				Destination = destination
			};

			var result = email.SendAsync(message);
			return Task.FromResult(result);
		}
	}

	// Configure the application sign-in manager which is used in this application.
	public class ApplicationSignInManager : SignInManager<User, string>
	{
		public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
			: base(userManager, authenticationManager)
		{

		}

		public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user)
		{
			return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
		}

		public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
		{
			return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
		}

		public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
		{
			User user = this.UserManager.FindByName(userName);
			if (null != user)
			{
				if (false == user.Active)
				{
					return (SignInStatus.Failure);
				}
			}

			var result = await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

			return (result);
		}
	}
}
