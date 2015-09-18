using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchScraper.Core.MenuReaders;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class CremeReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMeny_CanParseWeek38()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\CremeV38.htm");
			var menuReader = new CremeReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(dishes.Any(d => d.Description == "Halstrad lax med tryffelrisotto, fänkålsallad samt citron." && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Linguine med kyckling, champinjoner, persilja, ruccola samt parmesan." && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Tagliatelle med kräftstjärtar, hummersås, sparris samt parmesan" && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Spaghetti frutti di mare vitlök, persilja, tomat, vittvin samt parmesan." && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Steak frites med flanksteak, tomatsallad, pommes frites, rödvinssås samt sauce bearnaise." && d.Date.DayOfWeek == DayOfWeek.Friday));
		}
	}
}
