﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using LunchScraper.Core.Domain;
using LunchScraper.Core.MenuReaders;
using LunchScraper.Core.Storage;
using Ninject;

namespace LunchScraper.MenuReaderJob
{
	/// <summary>
	/// Schedule: 1 1 * * * *
	/// </summary>
	public class Program
	{
		static void Main()
		{
			var dayOfWeek = DateTime.Now.DayOfWeek;
			if (dayOfWeek == DayOfWeek.Saturday || dayOfWeek == DayOfWeek.Sunday)
			{
				return;
			}

			var stopWatch = System.Diagnostics.Stopwatch.StartNew();

			var dishes = GetDishes();
			dishes.ForEach(d => d.Date = DateTime.SpecifyKind(d.Date, DateTimeKind.Utc));

			StoreDishes(dishes);

			stopWatch.Stop();
			Console.WriteLine("Scraped {0} dishes in {1}.", dishes.Count, stopWatch.Elapsed);
		}

		private static void StoreDishes(List<Dish> dishes)
		{
			try
			{
				var repository = new MenuRepository();
				repository.DeleteThisWeek();

				foreach (var dishGroup in dishes.GroupBy(d => d.PartitionKey))
				{
					repository.StoreDishes(dishGroup);
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine("StoreDishes failed!\r\n{0}", ex.Message);
			}
		}

		private static List<Dish> GetDishes()
		{
			var dishes = new List<Dish>();
			var menuReaders = GetMenuReaders();

			Parallel.ForEach(menuReaders, reader =>
			{
				try
				{
					dishes.AddRange(reader.ReadWeeklyMenu());
				}
				catch (Exception ex)
				{
					Console.WriteLine("{0}.GetDishes failed!\r\n{1}", reader.GetType().Name, ex.Message);

					Exception innerException = ex;
					while(innerException.InnerException != null)
					{
						innerException = innerException.InnerException;
					}

					Console.WriteLine("{0}\r\n", innerException.Message);
					Console.WriteLine("{0}\r\n", innerException.StackTrace);
				}
			});

			return dishes;
		}

		private static IEnumerable<IMenuReader> GetMenuReaders()
		{
			IKernel kernel = new StandardKernel();
			kernel.Load(Assembly.GetExecutingAssembly());

			var menuReaders = kernel.GetAll<IMenuReader>();
			return menuReaders;
		}
	}
}
