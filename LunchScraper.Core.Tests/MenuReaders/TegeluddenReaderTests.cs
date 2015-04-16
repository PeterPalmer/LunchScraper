using System;
using System.Linq;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class TegeluddenReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtmlWeek14()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\CafeTegeluddenV14.htm");
			var menuReader = new TegeluddenReader(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			var monday = DateTime.Today.AddDays(DayOfWeek.Monday - DateTime.Today.DayOfWeek);

			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Köttfärslimpa med kokt potatis, gräddsås och lingon" && d.Date == monday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Chili con carne med ris" && d.Date == monday.AddDays(1)));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Pasta pollo med kyckling, tomat, grädde och spenat" && d.Date == monday.AddDays(2)));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Fläsknoisette med stekt potatis, kantarellsås och bearnaise" && d.Date == monday.AddDays(3)));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Stängt glad påsk" && d.Date == monday.AddDays(4)));
		}

		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtmlWeek15()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\CafeTegeluddenV15.htm");
			var menuReader = new TegeluddenReader(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			var monday = DateTime.Today.AddDays(DayOfWeek.Monday - DateTime.Today.DayOfWeek);

			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Annandag påsk" && d.Date == monday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Stekt fläsk med raggmunk och lingon / löksås och kokt potatis" && d.Date == monday.AddDays(1)));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Kalvfärsbiff med ört potatismos, gräddsås och kokt potatis" && d.Date == monday.AddDays(2)));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Ugnsgrillad laxfilé med kokt potatis och romsås" && d.Date == monday.AddDays(3)));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Havets wallenbergare med potatismos, skirat smör och rårörda lingon" && d.Date == monday.AddDays(4)));
		}
	}
}
