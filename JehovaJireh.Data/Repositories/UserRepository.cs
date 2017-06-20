using System.Linq;
using JehovaJireh.Core.Entities;
using JehovaJireh.Core.IRepositories;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using NHibernate;

namespace JehovaJireh.Data.Repositories
{
	public class UserRepository:NHRepository<User, int>, IUserRepository
	{
		ISession session;

		public UserRepository(ISession session, ExceptionManager exManager)
			: base(session, exManager)
		{

		}

		public UserRepository(ISession session)
			: base(session)
		{
			this.session = session;
		}

		public UserRepository()
		{

		}

		public User GetByUserName(string userName)
		{
			return (from u in this.Query() where u.UserName == userName select u).SingleOrDefault();
		}

		public User GetByEmail(string email)
		{
			return (from u in this.Query() where u.Email == email select u).SingleOrDefault();
		}

		public User GetByConfirmationToken(string token)
		{
			return (from u in this.Query() where u.ConfirmationToken == token select u).SingleOrDefault();
		}
	}
}
