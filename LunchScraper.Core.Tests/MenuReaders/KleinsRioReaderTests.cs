using System;
using System.Linq;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class KleinsRioReaderTests
	{

		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtmlWeek19()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\KleinsRioV19.htm");
			var menuReader = new TegeluddenReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(dishes.Any(d => d.Description == "Rice bowl med svart ris, syrade böngroddar, morot, tofu & ananas." && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Nattbakad nötstek, gräddsås, inlagd gurka & rårörda lingon. Serveras med potatisstomp." && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Pulled pork, tortilla, mexican slaw & syrad rödlök Serveras med potatis stripes." && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Ingefärsglaserade revbensspjäll, rabarberkompott & äpple serveras med bakad potatis." && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Nudelwok med sojaprotein, schezuanpeppar & pak choi, serveras med chilimajonnäs." && d.Date.DayOfWeek == DayOfWeek.Friday));
		}

	}
}
