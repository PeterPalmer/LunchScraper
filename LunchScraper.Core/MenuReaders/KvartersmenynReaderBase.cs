using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Extensions;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public abstract class KvartersmenynReaderBase : MenuReaderBase
	{
		public KvartersmenynReaderBase(IWebScraper scraper) : base(scraper)
		{
		}

		private static readonly HashSet<string> _weekDays = new HashSet<string>(new[]
		{
			"måndag", "tisdag", "onsdag","torsdag","fredag"
		});

		protected abstract Restaurant Restaurant { get; }

		public override List<Dish> ReadWeeklyMenu()
		{
			var sw = Stopwatch.StartNew();
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.Url);
			var cq = new CQ(html);

			var dayMenu = cq["div.meny"].FirstOrDefault();

			if (dayMenu == null)
			{
				return dishes;
			}

			var date = DateHelper.MondayThisWeek().AddDays(-1);
			var dishesArray = dayMenu.InnerHTML.Split(new[] { "<br />", "<br>" }, StringSplitOptions.RemoveEmptyEntries);

			int offset = 0;
			if (StartsWithWeekDay(dishesArray))
			{
				date = date.AddDays(1);
				offset = 1;
			}

			foreach (var dish in dishesArray.Skip(offset))
			{
				var description = dish.StripHtmlTags();

				if (_weekDays.Contains(description.ToLower().TrimEnd(':')))
				{
					date = date.AddDays(1);
					continue;
				}

				if (!string.IsNullOrWhiteSpace(description))
				{
					dishes.Add(new Dish(description, date, Restaurant.Id));
				}
			}

			if (date == DateHelper.MondayThisWeek().AddDays(-1))
			{
				dishes.Clear();
			}

			sw.Stop();
			Debug.WriteLine("[KvartersmenynReaderBase] Scraped {0} in {1}", Restaurant.Name, sw.Elapsed);

			return dishes;
		}

		private bool StartsWithWeekDay(string[] dishes)
		{
			if (!dishes.Any())
			{
				return false;
			}

			var firstLine = dishes[0].StripHtmlTags().ToLower();
			firstLine = firstLine.TrimEnd(':');

			return _weekDays.Contains(firstLine);
		}

	}
}
