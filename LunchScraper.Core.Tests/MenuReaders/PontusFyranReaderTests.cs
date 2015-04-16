using System;
using System.Linq;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class PontusFyranReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMenu_ParsePdf_Week15()
		{
			// Arrange
			var scraper = TestHelper.MockPdfScraper(@"TestInput\PontusFyranV15.pdf");
			var reader = new PontusFyranReader(scraper);

			// Act
			var menu = reader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Stängt!" && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Blandfärslimpa med rökt fläsk, svampsås, örpotatispuré, lingon och saltgurka (G/L)" && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Spanskt bondris med musslor, räkor, chorizo och mojo rojo (G)" && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Orientaliska nudelkakor med tofu, soja och jasminris (G)" && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Sotad torsk med fänkål, hollandaisesås, forellrom och dillkokt potatis" && d.Date.DayOfWeek == DayOfWeek.Friday));
		}

		[TestMethod]
		public void ReadWeeklyMenu_ParsePdf_Week16()
		{
			// Arrange
			var scraper = TestHelper.MockPdfScraper(@"TestInput\PontusFyranV16.pdf");
			var reader = new PontusFyranReader(scraper);

			// Act
			var menu = reader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Fläskkarré ”Sichuan” med jordnötssås, jasminris och heta wontonchips (G)" && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Nötfärsbiffar med rostad paprika, grönpepparsås och friterad potatis (L)" && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Kyckling i bengalisk curry med linser, kokos och jasminris" && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Pocherad torsk med ägg 63°, dillkokt potatis, brynt smör och pepparrot (G/L)" && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(menu.Dishes.Any(d => d.Description == "Grekisk paj med fetaost, tzatziki och mixsallad (G/L)" && d.Date.DayOfWeek == DayOfWeek.Friday));
		}
	}
}
