using System;
using LunchScraper.Core.Domain;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class TennisHallenReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtml()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\TennishallenV14.htm");
			var menuReader = new LansrattenReaderBase(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(10, menu.Dishes.Count);
			CollectionAssert.Contains(menu.Dishes, new Dish("BBQ glacerad svensk karré med rostad broccoli", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Dagens sallad: Varmrökt lax med rostade rotfrukter samt senapsdressing", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Pannbiff med lök samt inlagd gurka", new DateTime(2015, 3, 31)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Påsklunch: Lammstek med potatisgratäng samt liten dessert", new DateTime(2015, 4, 1)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Inkokt lax med dillmajonäs och pressgurka", new DateTime(2015, 4, 2)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Spenat och ricottafylld cannelloni", new DateTime(2015, 4, 3)));
		}
	}
}