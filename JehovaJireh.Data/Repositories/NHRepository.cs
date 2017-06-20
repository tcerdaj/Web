using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JehovaJireh.Core.IRepositories;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using NHibernate;
using NHibernate.Linq;

namespace JehovaJireh.Data.Repositories
{
	public class NHRepository<T, IdT> : IRepository<T, IdT>
	{
		private readonly ISession session;
		private readonly ExceptionManager exManager;

		public ISession Session
		{
			get { return session; }
		}

		public NHRepository(ISession session, ExceptionManager exManager)
		{
			this.session = session;
			this.exManager = exManager;
		}

		public NHRepository(ISession session)
		{
			this.session = session;
		}

		public NHRepository()
		{

		}

		public IQueryable<T> Query()
		{
			IQueryable<T> data = null;

			try
			{
				data = Session.Query<T>();
			}
			catch (Exception ex)
			{
				throw ex;
				//Exception exceptionToThrow;
				//if (exManager.HandleException(ex, Policies.DataAccess.WebServicePolicy, out exceptionToThrow))
				//{
				//    if (exceptionToThrow == null)
				//        throw;
				//    else
				//        throw exceptionToThrow;
				//}
			}

			return data;
		}

		public T GetById(IdT Id)
		{
			object item = new object();

			try
			{
				item = Session.Get<T>(Id);
			}
			catch (Exception ex)
			{
				Exception exceptionToThrow;
				//if (exManager.HandleException(ex, Policies.DataAccess.WebServicePolicy, out exceptionToThrow))
				//{
				//	if (exceptionToThrow == null)
				//		throw;
				//	else
				//		throw exceptionToThrow;
				//}
				throw ex;
			}

			return (T)item;
		}

		public void Create(T entity)
		{

			try
			{
				using (var tx = Session.BeginTransaction())
				{

					Session.Save(entity);
					tx.Commit();

				}
			}
			catch (Exception ex)
			{
				//log.Error(ex);
				throw ex;
			}

		}

		public void Delete(T entity)
		{
			try
			{
				using (var tx = Session.BeginTransaction())
				{
					Session.Delete(entity);
					tx.Commit();
				}
			}
			catch (Exception ex)
			{
				//Exception exceptionToThrow;
				//if (exManager.HandleException(ex, Policies.DataAccess.WebServicePolicy, out exceptionToThrow))
				//{
				//    if (exceptionToThrow == null)
				//        throw;
				//    else
				//        throw exceptionToThrow;
				//}
				throw ex;
			}

		}

		public void DeleteById(IdT Id)
		{
			//log.DeleteStarted(Session.Load<T>(Id));
			//Stopwatch timespan = Stopwatch.StartNew();
			try
			{
				using (var tx = Session.BeginTransaction())
				{
					Session.Delete(Session.Load<T>(Id));
					tx.Commit();
				}
			}
			catch (Exception ex)
			{
				//Exception exceptionToThrow;
				//if (exManager.HandleException(ex, Policies.DataAccess.WebServicePolicy, out exceptionToThrow))
				//{
				//    if (exceptionToThrow == null)
				//        throw;
				//    else
				//        throw exceptionToThrow;
				//}
				throw ex;
			}
			//timespan.Stop();
			// log.DeleteFinished(Session.Load<T>(Id), timespan.Elapsed);

		}

		public void Dispose()
		{
		}


		public void Update(T entity)
		{
			//log.SaveStarted(entity);
			//Stopwatch timespan = Stopwatch.StartNew();
			try
			{
				using (var tx = Session.BeginTransaction())
				{
					Session.SaveOrUpdate(entity);
					tx.Commit();
				}
			}
			catch (Exception ex)
			{
				Exception exceptionToThrow;
				//if (exManager.HandleException(ex, Policies.DataAccess.WebServicePolicy, out exceptionToThrow))
				//{
				//	if (exceptionToThrow == null)
				//		throw;
				//	else
				//		throw exceptionToThrow;
				//}
				throw ex;
			}

		}


		public T GetReferenceById(IdT Id)
		{
			object item = new object();

			try
			{
				item = Session.Load<T>(Id);
			}
			catch (Exception ex)
			{
				//Exception exceptionToThrow;
				//if (exManager.HandleException(ex, Policies.DataAccess.WebServicePolicy, out exceptionToThrow))
				//{
				//    if (exceptionToThrow == null)
				//        throw;
				//    else
				//        throw exceptionToThrow;
				//}
				throw ex;
			}

			return (T)item;
		}
	}
}
