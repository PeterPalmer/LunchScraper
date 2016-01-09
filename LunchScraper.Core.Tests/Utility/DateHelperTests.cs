using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.Tests.Utility
{
	[TestClass]
	public class DateHelperTests
	{
		[TestMethod]
		public void GetWeekNumber_ReturnFirstFullWeek()
		{
			var week = DateHelper.GetWeekNumber(new DateTime(2016, 1, 4));

			Assert.AreEqual(1, week);
		}
	}
}
