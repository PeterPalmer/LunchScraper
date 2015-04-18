using System;
using System.Collections.Generic;
using System.Globalization;

namespace LunchScraper.Core.Utility
{
	public static class DateHelper
	{
		private static readonly Dictionary<string, Func<DateTime>> _dateLookup = new Dictionary<string, Func<DateTime>>();

		static DateHelper()
		{
			_dateLookup.Add("måndag", MondayThisWeek);
			_dateLookup.Add("tisdag", TuesdayThisWeek);
			_dateLookup.Add("onsdag", WednesdayThisWeek);
			_dateLookup.Add("torsdag", ThursdayThisWeek);
			_dateLookup.Add("fredag", FridayThisWeek);
		}

		public static DateTime MondayThisWeek()
		{
			var today = DateTime.Today;
			if (today.DayOfWeek == DayOfWeek.Monday)
			{
				return today;
			}

			return today.AddDays(DayOfWeek.Monday - today.DayOfWeek);
		}

		public static DateTime TuesdayThisWeek()
		{
			return MondayThisWeek().AddDays(1);
		}

		public static DateTime WednesdayThisWeek()
		{
			return MondayThisWeek().AddDays(2);
		}

		public static DateTime ThursdayThisWeek()
		{
			return MondayThisWeek().AddDays(3);
		}

		public static DateTime FridayThisWeek()
		{
			return MondayThisWeek().AddDays(4);
		}

		public static DateTime SaturdayThisWeek()
		{
			return MondayThisWeek().AddDays(5);
		}

		public static DateTime SundayThisWeek()
		{
			return MondayThisWeek().AddDays(6);
		}

		public static DateTime WeekdayToDate(string weekday)
		{
			if (!_dateLookup.ContainsKey(weekday.ToLower()))
			{
				return DateTime.Today;
			}

			return _dateLookup[weekday.ToLower()]();
		}

		public static string VeckodagPåSvenska(DayOfWeek dayOfWeek)
		{
			CultureInfo swedish = new CultureInfo("sv-SE");
			DateTimeFormatInfo dtfi = swedish.DateTimeFormat;

			return dtfi.DayNames[(int)dayOfWeek];
		}

		public static int GetWeekNumber()
		{
			GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);

			return cal.GetWeekOfYear(DateTime.Today, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
		}
	}
}
