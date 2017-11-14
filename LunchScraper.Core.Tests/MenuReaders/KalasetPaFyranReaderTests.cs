using System.Linq;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class KalasetPaFyranReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtml()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\KalasetPaFyranV41.htm");
			var menuReader = new KalasetPaFyranReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(15, dishes.Count);
			Assert.IsTrue(dishes.Any(d => d.Description == "Citronpocherad koljafilé | krämig äggsås | örtslungad potatis"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Fiskgratäng | sejfilé  | räkor | saffranssås| duchesse"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Enchiladas | fefferoni | salsa | smetana"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Jordärtskockssoppa | vegowraps"));
			Assert.IsTrue(dishes.Any(d => d.Description == "Basilikabakad torskfilé | blomkål ø potatispuré | tomatsky"));
		}
	}
}
