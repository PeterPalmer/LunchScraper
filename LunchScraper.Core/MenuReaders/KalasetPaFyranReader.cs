using System;
using System.Collections.Generic;
using System.Net;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class KalasetPaFyranReader : MenuReaderBase
	{
		public KalasetPaFyranReader(IWebScraper scraper) : base(scraper)
		{
		}

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.KalasetPåFyran.Url);
			var cq = new CQ(html);
			var menuDate = DateHelper.MondayThisWeek();

			var lunchMenuTags = cq["#main-content p > strong, #main-content li"];

			if (lunchMenuTags == null)
			{
				return dishes;
			}

			foreach (var tag in lunchMenuTags)
			{
				if (tag.NodeName.Equals("strong", StringComparison.OrdinalIgnoreCase))
				{
					menuDate = ParseWeekDay(WebUtility.HtmlDecode(tag.InnerText).Trim());
					continue;
				}

				var description = WebUtility.HtmlDecode(tag.InnerText).Trim();

				/*if (WebUtility.HtmlDecode(description).Trim().Equals("(v) – finns som vegeteriskt alternativ", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}*/

				var dish = new Dish(description, menuDate, Restaurant.KalasetPåFyran.Id);
				dishes.Add(dish);
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

			return DateTime.MinValue;
		}
	}
}
