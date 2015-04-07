using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Extensions;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class TegeluddenReader : MenuReaderBase
	{
		private static HashSet<string> _weekDays = new HashSet<string>(new[]
		{
			"måndag", "tisdag", "onsdag","torsdag","fredag"
		});

		public TegeluddenReader(IWebScraper scraper) : base(scraper)
		{
			this.RestaurantName = "Café Tegeludden";
		}

		public override LunchMenu ReadWeeklyMenu()
		{
			var url = "http://cafetegeludden.kvartersmenyn.se/";
			var weeklyMenu = new LunchMenu(RestaurantName, url);

			var html = Scraper.ScrapeWebPage(url);
			var cq = new CQ(html);

			var dayMenus = cq[".menyn p"];

			var date = DateHelper.MondayThisWeek();

			foreach (var dayMenu in dayMenus)
			{
				var dishes = dayMenu.InnerHTML.Split(new string[] { "<br />" }, StringSplitOptions.RemoveEmptyEntries);

				if(!ShouldParseDishes(dishes)) continue;

				foreach (var dish in dishes.Skip(1))
				{
					var description = dish.StripHtmlTags();

					if (_weekDays.Contains(description.ToLower().TrimEnd(':')))
					{
						date = date.AddDays(1);
						continue;
					}

					if (!string.IsNullOrWhiteSpace(description))
					{
						weeklyMenu.Dishes.Add(new Dish(dish.StripHtmlTags(), date));
					}
				}

				date = date.AddDays(1);
			}

			return weeklyMenu;
		}

		private bool ShouldParseDishes(string[] dishes)
		{
			if (!dishes.Any())
			{
				return false;
			}

			var firstLine = dishes[0].StripHtmlTags().ToLower();
			firstLine = firstLine.TrimEnd(':');

			if (!_weekDays.Contains(firstLine))
			{
				return false;
			}

			return true;
		}
	}
}
