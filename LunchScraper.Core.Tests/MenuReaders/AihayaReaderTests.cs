using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchScraper.Core.MenuReaders;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class AihayaReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMeny_CanParseWeek38()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\AiyaraV38.htm");
			var menuReader = new AihayaReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			var monday = DateTime.Today.AddDays(DayOfWeek.Monday - DateTime.Today.DayOfWeek);
			Assert.IsTrue(dishes.Any(d => d.Description == "1. Geang phed gai - Stark\nKyckling med röd curry, kokosmjölk, bambuskott, röd paprika, \naubergine, zucchini & kaffirlimeblad (v)" && d.Date == monday));
		}
	}
}
