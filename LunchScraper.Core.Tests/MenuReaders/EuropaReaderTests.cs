using System.Linq;
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
			var menuReader = new EuropaReader(scraper);

			// Act
			var menu = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.AreEqual(13, menu.Dishes.Count);

			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Biff lindström med kryddsmör och skysås"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Bakad torskfilé med örttäcke och tomatsky"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Lins- & sötpotatisvindaloo med husets mango chutney"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Köttfärsås och sedani regati samt riven grana padano"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Mörbakat anklår med rödvinssås samt vitlöksstekt potatis och gröna bönor"));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Vi tillönskar våra gäster en glad påsk"));
		}
	}
}
