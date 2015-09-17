using System;
using System.Collections.Generic;
using System.Net;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class AihayaReader : MenuReaderBase
	{
		public AihayaReader(IWebScraper scraper) : base(scraper)
		{

		}

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.Aihaya.Url);
			var cq = new CQ(html);
			var menuDate = DateHelper.MondayThisWeek();

			var lunchMenuTags = cq[".lunch_menu .menu_header, .lunch_menu .td_title"];

			if (lunchMenuTags == null)
			{
				return dishes;
			}

			foreach (var tag in lunchMenuTags)
			{
				if (tag.HasClass("menu_header"))
				{
					menuDate = ParseWeekDay(WebUtility.HtmlDecode(tag.InnerText).Trim());
					continue;
				}

				var description = tag.InnerText;

				if (WebUtility.HtmlDecode(description).Trim().Equals("(v) – finns som vegeteriskt alternativ", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				var dish = new Dish(description, menuDate, Restaurant.Aihaya.Id);
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
