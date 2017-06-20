using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.IRepositories;
using NHibernate;

namespace JehovaJireh.Data
{
	public class NHUnitOfWork: IUnitOfWork
	{
		private readonly ISession _session;
		private ITransaction _transaction;
		public NHUnitOfWork(ISession session)
		{
			_session = session;
		}
		public void Dispose()
		{
			if (_transaction != null)
				_transaction.Rollback();
		}

		public void Commit()
		{
			if (_transaction == null)
				throw new InvalidOperationException("UnitOfWork has already been saved.");

			if (!_transaction.IsActive)
			{
				throw new InvalidOperationException("Oops! We don't have an active transaction");
			}

			_transaction.Commit();
		}

		public void Rollback()
		{
			if (_transaction.IsActive)
			{
				_transaction.Rollback();
			}
		}

		public void Begin()
		{
			_transaction = _session.BeginTransaction();
		}
	}
}
