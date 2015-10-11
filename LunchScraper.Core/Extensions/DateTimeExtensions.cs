using System;

namespace LunchScraper.Core.Extensions
{
	public static class DateTimeExtensions
	{
		public static DateTime FromUnixTime(this long unixTime)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return epoch.AddSeconds(unixTime);
		}

		public static DateTime FromUnixTime(this string unixTime)
		{
			long epoch = 0;
			if (long.TryParse(unixTime, out epoch))
			{
				return epoch.FromUnixTime();
			}

			return DateTime.MinValue;
		}

		public static long ToUnixTime(this DateTime date)
		{
			var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return Convert.ToInt64((date - epoch).TotalSeconds);
		}
	}
}
