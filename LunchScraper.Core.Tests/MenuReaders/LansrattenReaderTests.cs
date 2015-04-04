using System;
using LunchScraper.Core.Domain;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class LansrattenReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtml()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\LänsrättenV14.htm");
			var menuReader = new LansrattenReaderBase(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(13, menu.Dishes.Count);
			CollectionAssert.Contains(menu.Dishes, new Dish("Röd curry på kalv med limeblad och kokosmjölk", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Pocherad miljöklassad torsk med ägg- & persiljesås samt gröna ärtor", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Lins- & sötpotatisvindaloo med husets mango chutney", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Thai fishcakes med sesamfrästa grönsaker och soyadipp", new DateTime(2015, 4, 1)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Ekologisk ärtsoppa med svenskt fläsk samt pannkaka med Länsrättens bärsylt", new DateTime(2015, 4, 2)));
			CollectionAssert.Contains(menu.Dishes, new Dish("GLAD PÅSK ÖNSKAR PERSONALEN PÅ LÄNSRÄTTEN", new DateTime(2015, 4, 3)));
		}
	}
}
