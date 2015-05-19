using System;
using System.Collections.Generic;
using System.Diagnostics;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.Storage
{
	[TestClass]
	public class MenuRepositoryTest
	{
		[TestMethod, Ignore]
		public void TestMethod1()
		{
			var repository = new MenuRepository();

			var dishes = new List<Dish>();
			dishes.Add(new Dish("Pannkakor", DateTime.Today, 1));
			dishes.Add(new Dish("Ärtsoppa", DateTime.Today, 2));

			repository.StoreDishes(dishes);
		}

		[TestMethod, Ignore]
		public void TestMethod2()
		{
			var repository = new MenuRepository();
			var dishes = repository.GetThisWeekDishes();

			//Assert.AreEqual(2, dishes.Count());
		}

		[TestMethod, Ignore]
		public void DeleteThisWeek()
		{
			var sw = Stopwatch.StartNew();

			var repository = new MenuRepository();
			repository.DeleteThisWeek();

			sw.Stop();
			Debug.WriteLine("Elapsed: {0}", sw.Elapsed);
		}
	}
}
