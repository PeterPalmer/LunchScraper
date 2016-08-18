using System;
using System.Collections.Generic;
using System.Net;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class CremeReader : MenuReaderBase
	{
		private static DateTime EveryDayOfWeek = DateTime.MaxValue;

		public CremeReader(IWebScraper scraper) : base(scraper)
		{

		}

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.Creme.Url);
			var cq = new CQ(html);
			var currentDate = DateTime.MinValue;

			var lunchMenuTags = cq["div.meny"];

			if (lunchMenuTags == null)
			{
				return dishes;
			}

			foreach (var tag in lunchMenuTags[0].ChildNodes)
			{
				if (tag.NodeName != "#text" && WebUtility.HtmlDecode(tag.InnerText).Equals("Alltid på Gärdet", StringComparison.OrdinalIgnoreCase))
				{
					break;
				}

				if (tag.NodeName.Equals("strong", StringComparison.OrdinalIgnoreCase))
				{
					currentDate = ParseWeekDay(WebUtility.HtmlDecode(tag.InnerText).Trim());
					continue;
				}

				var heading = WebUtility.HtmlDecode(tag.NodeValue);

				if (String.IsNullOrWhiteSpace(heading) || currentDate == DateTime.MinValue)
				{
					continue;
				}

				if (currentDate == EveryDayOfWeek)
				{
					for (int i = 0; i < 5; i++)
					{
						dishes.Add(new Dish(heading, DateHelper.MondayThisWeek().AddDays(i), Restaurant.Creme.Id));
					}
				}
				else
				{
					dishes.Add(new Dish(heading, currentDate, Restaurant.Creme.Id));
				}
			}

			return dishes;
		}

		private DateTime ParseWeekDay(string text)
		{
			if (text.StartsWith("Måndag", StringComparison.OrdinalIgnoreCase))
			{
				return DateHelper.MondayThisWeek();
			}

			if (text.StartsWith("Tisdag", StringComparison.OrdinalIgnoreCase))
			{
				return DateHelper.TuesdayThisWeek();
			}

			if (text.StartsWith("Onsdag", StringComparison.OrdinalIgnoreCase))
			{
				return DateHelper.WednesdayThisWeek();
			}

			if (text.StartsWith("Torsdag", StringComparison.OrdinalIgnoreCase))
			{
				return DateHelper.ThursdayThisWeek();
			}

			if (text.StartsWith("Fredag", StringComparison.OrdinalIgnoreCase))
			{
				return DateHelper.FridayThisWeek();
			}

			if (text.StartsWith("Veckans pasta", StringComparison.OrdinalIgnoreCase) ||
			    text.StartsWith("Veckans sallad", StringComparison.OrdinalIgnoreCase))
			{
				return EveryDayOfWeek;
			}

			return DateTime.MinValue;
		}
	}
}
