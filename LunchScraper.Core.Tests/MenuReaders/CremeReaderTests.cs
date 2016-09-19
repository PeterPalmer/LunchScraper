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
		public void ReadWeeklyMeny_CanParseWeek33()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\CremeV33.htm");
			var menuReader = new CremeReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(dishes.Any(d => d.Description == "15-timmarbakad högrev med tryffelcreme, röstipotatis och rödvinssås" && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Mustig fisk och skaldjurgryta med saffransrostade grönsaker, räkor, lax, sej och limeaioli" && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Chevregratinerad kalvfärsbiff med rostade betor, rosmarinsky och ugnsbakade körsbärstomater" && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "soja och honungsglacerade kycklinglårfiléer med krispiga grönsaker, nudelsallad och korianderdipp" && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Gärdets rosagrillade picanha \"black and white\" med ljummen potatissallad, rödvinssås och bearnaise" && d.Date.DayOfWeek == DayOfWeek.Friday));
		}

		[TestMethod]
		public void ReadWeeklyMeny_CanParseWeek38()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\CremeV38.htm");
			var menuReader = new CremeReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(dishes.Any(d => d.Description == "Nattbakad fläskkarré med rökig glaze, rostad potatis och avocadocreme" && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Halstrad regnbågsfilé med hollandaise, citron, grönsaksjulienne och gräslöksslungad färskpotatis" && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Wallenbergare med ärtor, lingon, skirat smör och potatispure" && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Tandorikyckling med gurksallad, basmatiris , grönsaker och myntayoghurt" && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Gärdets rosagrillade picanha \"black and white\" med ljummen potatissallad, rödvinssås och bearnaise" && d.Date.DayOfWeek == DayOfWeek.Friday));
		}
	}
}
