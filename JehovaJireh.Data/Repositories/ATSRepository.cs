using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace JehovaJireh.Data.Repositories
{
	public class ATSRepository<T> where T : class, ITableEntity, new()
	{
		private string partitionKey;
		private string tableName;

		internal CloudTableClient tableClient;
		internal CloudTable table;

		public ATSRepository(CloudTableClient tableClient, string tableName)
		{
			this.partitionKey = typeof(T).Name;
			this.tableName = tableName;
			this.tableClient = tableClient;

			//pluralise the partition key (because basically it is the 'table' name).
			if (partitionKey.Substring(partitionKey.Length - 1, 1).ToLower() == "y")
				partitionKey = partitionKey.Substring(0, partitionKey.Length - 1) + "ies";

			if (partitionKey.Substring(partitionKey.Length - 1, 1).ToLower() != "s")
				partitionKey = partitionKey + "s";

			table = tableClient.GetTableReference(tableName);
			table.CreateIfNotExists();
		}

		public virtual T GetByID(object id)
		{
			var query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id.ToString()));
			var result = table.ExecuteQuery(query).First();

			return (T)result;
		}

		public virtual List<T> GetAll()
		{
			var query = new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey)); //get all customers - because Customer is our partition key
			var result = table.ExecuteQuery(query).ToList();
			return result;
		}

		public virtual void Insert(T entity)
		{
			TableOperation insertOperation = TableOperation.Insert(entity);
			table.Execute(insertOperation);
		}
	}
}
