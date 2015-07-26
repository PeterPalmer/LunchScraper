using System;
using System.Collections.Generic;
using System.Configuration;
using CsQuery.ExtensionMethods;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace LunchScraper.Core.Storage
{
	public class MenuRepository : IMenuRepository
	{
		private readonly CloudTable _dishTable;

		public MenuRepository()
		{
			var connectionString = ConfigurationManager.ConnectionStrings["LunchScraper"];
			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString.ConnectionString);

			// Create the table client.
			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

			// Create the table if it doesn't exist.
			_dishTable = tableClient.GetTableReference("Dishes");
			_dishTable.CreateIfNotExists();
		}

		public void StoreDish(Dish dish)
		{
			_dishTable.Execute(TableOperation.InsertOrReplace(dish));
		}

		public void StoreDishes(IEnumerable<Dish> dishes)
		{
			TableBatchOperation batchOperation = new TableBatchOperation();

			dishes.ForEach(dish => batchOperation.Insert(dish));

			// Execute the insert operation.
			_dishTable.ExecuteBatch(batchOperation);
		}

		public void DeleteThisWeek()
		{
			var mondayPartitionKey = DateHelper.MondayThisWeek().ToString("yyyyMMdd");
			DeleteAllEntitiesInBatches(_dishTable, TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, mondayPartitionKey));
		}

		public IEnumerable<Dish> GetAllDishes()
		{
			var query = new TableQuery<Dish>();
			return _dishTable.ExecuteQuery(query);
		}

		public IEnumerable<Dish> GetThisWeekDishes()
		{
			var mondayPartitionKey = DateHelper.MondayThisWeek().ToString("yyyyMMdd");
			var query = new TableQuery<Dish>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.GreaterThanOrEqual, mondayPartitionKey));

			return _dishTable.ExecuteQuery(query);
		}

		private static void DeleteAllEntitiesInBatches(CloudTable table, string filter)
		{
			Action<IEnumerable<DynamicTableEntity>> processor = entities =>
			{
				var batches = new Dictionary<string, TableBatchOperation>();

				foreach (var entity in entities)
				{
					TableBatchOperation batch = null;

					if (batches.TryGetValue(entity.PartitionKey, out batch) == false)
					{
						batches[entity.PartitionKey] = batch = new TableBatchOperation();
					}

					batch.Add(TableOperation.Delete(entity));

					if (batch.Count == 100)
					{
						table.ExecuteBatch(batch);
						batches[entity.PartitionKey] = new TableBatchOperation();
					}
				}

				foreach (var batch in batches.Values)
				{
					if (batch.Count > 0)
					{
						table.ExecuteBatch(batch);
					}
				}
			};

			ProcessEntities(table, processor, filter);
		}

		private static void ProcessEntities(CloudTable table, Action<IEnumerable<DynamicTableEntity>> processor, string filter)
		{
			var query = new TableQuery<DynamicTableEntity>().Where(filter);
			processor( table.ExecuteQuery(query));

			//while (segment == null || segment.ContinuationToken != null)
			//{
			//	if (filter == null)
			//	{
			//		segment = table.ExecuteQuerySegmented(new TableQuery().Take(100), segment == null ? null : segment.ContinuationToken);
			//	}
			//	else
			//	{
			//		var query = table.CreateQuery<DynamicTableEntity>().Where(filter).Take(100).AsTableQuery();
			//		segment = query.ExecuteSegmented(segment == null ? null : segment.ContinuationToken);
			//	}

			//	processor(segment.Results);
			//}
		}
	}
}
