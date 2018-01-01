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
		public void ReadWeeklyMenu_CanParseHtmlWeek37()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\KleinsRioV37.htm");
			var menuReader = new KleinsRioReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(dishes.Any(d => d.Description == "Halloumigryta med tomater, linser, chili och kokosris." && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Filodegspaj med getost, svartkål och rödbetor. Toppas med pinjenötter och frasig grönkål." && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Rotfruktsgratäng med kål, svamp och fransk tomatsallad." && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Indisk linssoppa med tomat, spenat och garam masala." && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Helstekt biffrad med rödvinssky, purjolök, bacon och gräslöksstomp." && d.Date.DayOfWeek == DayOfWeek.Friday));
		}

		[TestMethod]
		public void ReadWeeklyMenu_CanParseHtmlWeek1()
		{
			// Arrange
			var scraper = TestHelper.MockWebScraper(@"TestInput\KleinsRioV1.htm");
			var menuReader = new KleinsRioReader(scraper);

			// Act
			var dishes = menuReader.ReadWeeklyMenu();

			// Assert
			Assert.IsTrue(dishes.Any(d => d.Description == "Halloumigryta med tomater, linser, chili och kokosris." && d.Date.DayOfWeek == DayOfWeek.Monday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Spaghetti carbonara, bacon, vitlök, äggula och grana padano." && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Rotfruktsgratäng med kål, svamp och fransk tomatsallad." && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Vitvinspocherad torsk med dillfrö, hummersky och pommes duchesse." && d.Date.DayOfWeek == DayOfWeek.Thursday));
			Assert.IsTrue(dishes.Any(d => d.Description == "Pankofriterad kolja med srirachamajo, chilipicklad gurka, mangosalsa och jasminris." && d.Date.DayOfWeek == DayOfWeek.Friday));
			Assert.AreEqual(3, dishes.Count(d => d.Date.DayOfWeek == DayOfWeek.Friday));
		}

	}
}
