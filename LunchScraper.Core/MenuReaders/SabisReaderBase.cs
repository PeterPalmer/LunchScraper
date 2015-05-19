using System.Collections.Generic;
using System.Diagnostics;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public abstract class SabisReaderBase : MenuReaderBase
	{
		protected SabisReaderBase(IWebScraper scraper) : base(scraper)
		{
		}

		protected abstract Restaurant Restaurant { get; }

		public override List<Dish> ReadWeeklyMenu()
		{
			var sw = Stopwatch.StartNew();

			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.Url);
			var cq = new CQ(html);
			var dayContainerDivs = cq["div.lunch-day-container"];

			var menuDate = DateHelper.MondayThisWeek();

			foreach (var dayContainer in dayContainerDivs)
			{
				var dishSpans = cq.Select("li > span.lunch-dish-description", dayContainer);

				foreach (var span in dishSpans)
				{
					var dish = new Dish(span.InnerText, menuDate, Restaurant.Id);
					dishes.Add(dish);
				}

				menuDate = menuDate.AddDays(1);
			}

			sw.Stop();
			Debug.WriteLine("[SabisReaderBase] Scraped {0} in {1}", this.Restaurant.Name, sw.Elapsed);

			return dishes;
		}

	}
}