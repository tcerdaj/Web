using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JehovaJireh.Configuration;
using JehovaJireh.Core.Queues;
using JehovaJireh.Data;
using JehovaJireh.Data.Listeners;
using JehovaJireh.Data.Queues;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using NHibernate;
using NHibernate.Event;

namespace JehovaJireh.Infrastructure.Plumbing
{
	public class PersistenceFacility : AbstractFacility
	{
		protected override void Init()
		{
			var configDB = BuildDatabaseConfiguration();
			var configAzureStorage = BuildCloudStorageAccountConfiguration();

			Kernel.Register(
				Component.For<ISessionFactory>()
					.UsingFactoryMethod(_ => configDB.BuildSessionFactory()),
				Component.For<ISession>()
					.UsingFactoryMethod(k => k.Resolve<ISessionFactory>().OpenSession())
					.LifestyleTransient(),
				Component.For<CloudQueueClient>()
					.UsingFactoryMethod(_ => configAzureStorage.CreateCloudQueueClient()),
				Component.For<CloudTableClient>()
					.UsingFactoryMethod(_ => configAzureStorage.CreateCloudTableClient()),
				Component.For<CloudBlobClient>()
					.UsingFactoryMethod(_ => configAzureStorage.CreateCloudBlobClient()),
				Component.For<IQueue>()
					.ImplementedBy<AzureQueue>()
				);
		}

		protected virtual IPersistenceConfigurer SetupDatabase()
		{
			var connectinString = JehovaJireh.Configuration.CloudConfiguration.GetConnectionString("SQLConnectionString");
			return MsSqlConfiguration.MsSql2008
				.UseOuterJoin()
				.ConnectionString(x => x.Is(connectinString))
				.ShowSql();
		}
		private NHibernate.Cfg.Configuration BuildDatabaseConfiguration()
		{
			NHibernate.Cfg.Configuration cfg = Fluently.Configure()
				.Database(SetupDatabase)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHUnitOfWork>())
				.ExposeConfiguration(c =>
				{
					c.SetProperty(@"nhibernate-logger", @"JehovaJireh.Infrastructure.Logging.NLogNHibernateFactory, JehovaJireh.Infrastructure"); //TODO: WHY DOESNT THIS WORK?
				})
				.BuildConfiguration();
			cfg.EventListeners.PreUpdateEventListeners =
				new IPreUpdateEventListener[] { new AuditEventListener() };
			cfg.EventListeners.PreInsertEventListeners =
				new IPreInsertEventListener[] { new AuditEventListener() };
			return cfg;
		}

		private CloudStorageAccount BuildCloudStorageAccountConfiguration()
		{
			var storageAccount = CloudConfiguration.GetStorageAccount("StorageConnectionString");
			var queueContext = new AzureQueue(storageAccount.CreateCloudQueueClient());

			return storageAccount;
		}
	}
}
