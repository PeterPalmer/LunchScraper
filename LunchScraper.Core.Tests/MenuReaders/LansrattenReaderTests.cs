using System.Linq;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class LansrattenReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtmlWeek14()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\LänsrättenV14.htm");
			var menuReader = new LansrattenReader(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(13, menu.Dishes.Count);

			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Röd curry på kalv med limeblad och kokosmjölk"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Pocherad miljöklassad torsk med ägg- & persiljesås samt gröna ärtor"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Lins- & sötpotatisvindaloo med husets mango chutney"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Thai fishcakes med sesamfrästa grönsaker och soyadipp"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Ekologisk ärtsoppa med svenskt fläsk samt pannkaka med Länsrättens bärsylt"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "GLAD PÅSK ÖNSKAR PERSONALEN PÅ LÄNSRÄTTEN"));
		}

		[TestMethod]
		public void ReadWeeklyMenu_FailGracefully()
		{
			// Arrange - Invalid input
			var scraper = TestHelper.MockWebScraper(@"TestInput\CafeTegeluddenV14.htm");
			var menuReader = new LansrattenReader(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(0, menu.Dishes.Count);
		}
	}
}
