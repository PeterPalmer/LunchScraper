using System.Linq;
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
			var menuReader = new TennishallenReader(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(10, menu.Dishes.Count);
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "BBQ glacerad svensk karré med rostad broccoli"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Dagens sallad: Varmrökt lax med rostade rotfrukter samt senapsdressing"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Pannbiff med lök samt inlagd gurka"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Påsklunch: Lammstek med potatisgratäng samt liten dessert"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Inkokt lax med dillmajonäs och pressgurka"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Spenat och ricottafylld cannelloni"));
		}
	}
}