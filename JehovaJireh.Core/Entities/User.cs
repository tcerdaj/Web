using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JehovaJireh.Core.Entities
{
	public class User: EntityBase<int>
	{
		#region Properties
		public virtual string UserName { get; set; }
		public virtual string PhoneNumber { get; set; }
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
		public virtual bool Active { get; set; }
		public virtual string ConfirmationToken { get; set; }
		public virtual bool IsConfirmed { get; set; }
		public virtual ICollection<Role> Roles { get; set; }
		#endregion
	}
}
