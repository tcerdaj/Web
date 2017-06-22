using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace JehovaJireh.Core.Entities
{
	public class User : EntityBase<int>, IUser
	{
		#region Properties
		public virtual string UserName { get; set; }
		public virtual string FirstName { get; set; }
		public virtual string LastName { get; set; }
		public virtual Gender Gender { get; set; }
		public virtual string PasswordHash { get; set; }
		public virtual string SecurityStamp { get; set; }
		public virtual string Email { get; set; }
		public virtual string Address { get; set; }
		public virtual string City { get; set; }
		public virtual string State { get; set; }
		public virtual string Zip { get; set; }
		public virtual string PhoneNumber { get; set; }
		public virtual bool Active { get; set; }
		public virtual string ConfirmationToken { get; set; }
		public virtual bool IsConfirmed { get; set; }
		public virtual bool IsChurchMember { get; set; }
		public virtual string ChurchName { get; set; }
		public virtual string ChurchAddress { get; set; }
		public virtual string ChurchPhone { get; set; }
		public virtual string ChurchPastor { get; set; }
		public virtual bool NeedToBeVisited { get; set; }
		public virtual string Comments { get; set; }
		public virtual ICollection<Role> Roles { get; set; }

		string IUser<string>.Id
		{
			get { return this.Id.ToString(); }
		}
		#endregion

		#region Methods
		public virtual async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
		{
			// Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
			var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
			// Add custom user claims here
			return userIdentity;
		}

		public virtual void SetStatus(Boolean active)
		{
			this.Active = active;
		}
		#endregion
	}
}
