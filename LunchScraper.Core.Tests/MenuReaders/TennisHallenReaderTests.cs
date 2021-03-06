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
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(10, dishes.Count);
			Assert.IsTrue(dishes.Any(d => d.Description == "BBQ glacerad svensk karr� med rostad broccoli"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Dagens sallad: Varmr�kt lax med rostade rotfrukter samt senapsdressing"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Pannbiff med l�k samt inlagd gurka"));
			Assert.IsTrue(dishes.Any(d => d.Description == "P�sklunch: Lammstek med potatisgrat�ng samt liten dessert"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Inkokt lax med dillmajon�s och pressgurka"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Spenat och ricottafylld cannelloni"));
		}
	}
}