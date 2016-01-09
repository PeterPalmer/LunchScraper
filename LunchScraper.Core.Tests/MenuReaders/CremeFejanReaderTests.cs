using System;
using System.Collections.Generic;
using System.Linq;
using LunchScraper.Core.MenuReaders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LunchScraper.Core.Tests.MenuReaders
{
	[TestClass]
	[DeploymentItem("TestInput", "TestInput")]
	public class CremeFejanReaderTests
	{
		[TestMethod]
		public void ReadWeeklyMeny_CanParseWeek38()
		{
			//// Arrange
			//var scraper = TestHelper.MockWebScraper(@"TestInput\CremeFejan.htm");
			//var menuReader = new CremeFejanReader(scraper);

			//// Act
			//var dishes = menuReader.ReadWeeklyMenu(new DateTime(2015, 9, 28));

			//// Assert
			//Assert.IsTrue(dishes.Any(d => d.Description == "Örtmarinerad fläskfilé med grönpepparsås, stompad potatis & grillad tomat" && d.Date.DayOfWeek == DayOfWeek.Monday));
			//Assert.IsTrue(dishes.Any(d => d.Description == "Fransk fiskgryta med fänkål, selleri, rotfrukter, surdegskrutonger & saffransaioli" && d.Date.DayOfWeek == DayOfWeek.Tuesday));
			//Assert.IsTrue(dishes.Any(d => d.Description == "Grillad kycklingklubba med klyftpotatis, sallad med röda linser samt chilicreme" && d.Date.DayOfWeek == DayOfWeek.Wednesday));
			//Assert.IsTrue(dishes.Any(d => d.Description == "Wallenbergare med potatispuré, rårörda lingon, ärtor, skirat smör samt gräddsås" && d.Date.DayOfWeek == DayOfWeek.Thursday));
			//Assert.IsTrue(dishes.Any(d => d.Description == "Rosagrillad flankstek med ljummen potatissallad, rödvinsås & bear-naise" && d.Date.DayOfWeek == DayOfWeek.Friday));
		}

	}
}
