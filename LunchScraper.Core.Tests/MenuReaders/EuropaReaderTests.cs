using System;
using LunchScraper.Core.Domain;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class EuropaReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtml()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\EuropaV14.htm");
			var menuReader = new LansrattenReaderBase(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(13, menu.Dishes.Count);
			CollectionAssert.Contains(menu.Dishes, new Dish("Biff lindström med kryddsmör och skysås", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Bakad torskfilé med örttäcke och tomatsky", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Lins- & sötpotatisvindaloo med husets mango chutney", new DateTime(2015, 3, 30)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Köttfärsås och sedani regati samt riven grana padano", new DateTime(2015, 4, 1)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Mörbakat anklår med rödvinssås samt vitlöksstekt potatis och gröna bönor", new DateTime(2015, 4, 2)));
			CollectionAssert.Contains(menu.Dishes, new Dish("Vi tillönskar våra gäster en glad påsk", new DateTime(2015, 4, 3)));
		}
	}
}
