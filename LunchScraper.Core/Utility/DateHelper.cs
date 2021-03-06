﻿using System;
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
			var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");

			var today = TimeZoneInfo.ConvertTime(DateTime.Today, timeZone);

			if (today.DayOfWeek == DayOfWeek.Monday)
			{
				return today;
			}

			if (today.DayOfWeek == DayOfWeek.Sunday)
			{
				return today.AddDays(-6);
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

		public static int GetWeekNumber(DateTime date)
		{
			CultureInfo swedish = new CultureInfo("sv-SE");
			Calendar calendar = swedish.DateTimeFormat.Calendar;

			return calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
		}

		public static int GetWeekNumber()
		{
			return GetWeekNumber(DateTime.Today);
		}
	}
}
