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
		public CremeReader(IWebScraper scraper) : base(scraper)
		{

		}

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.Creme.Url);
			var cq = new CQ(html);
			var menuDate = DateHelper.MondayThisWeek();

			var lunchMenuTags = cq[".category .media-heading, .courses .media-body"];

			if (lunchMenuTags == null)
			{
				return dishes;
			}

			foreach (var tag in lunchMenuTags)
			{
				if (tag.HasClass("media-heading"))
				{
					menuDate = ParseWeekDay(WebUtility.HtmlDecode(tag.InnerText).Trim());
					continue;
				}

				var heading = cq.Select(".media-heading", tag).FirstElement();

				if (heading == null || !WebUtility.HtmlDecode(heading.InnerHTML).Trim().StartsWith("Dagens", StringComparison.OrdinalIgnoreCase))
				{
					continue;
				}

				var description = cq.Select("p", tag).FirstElement().InnerText;
				var dish = new Dish(description, menuDate, Restaurant.Creme.Id);
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
