using System.Diagnostics;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public abstract class SabisReaderBase : MenuReaderBase
	{
		protected SabisReaderBase(IWebScraper scraper, string restaurantName) : base(scraper)
		{
			this.RestaurantName = restaurantName;
		}

		protected abstract string MenuUrl { get; }

		public override LunchMenu ReadWeeklyMenu()
		{
			var sw = Stopwatch.StartNew();

			var weeklyMenu = new LunchMenu(RestaurantName, MenuUrl);

			var html = Scraper.ScrapeWebPage(MenuUrl);
			var cq = new CQ(html);
			var dayContainerDivs = cq["div.lunch-day-container"];

			var menuDate = DateHelper.MondayThisWeek();

			foreach (var dayContainer in dayContainerDivs)
			{
				var dishSpans = cq.Select("li > span.lunch-dish-description", dayContainer);

				foreach (var span in dishSpans)
				{
					var dish = new Dish(span.InnerText, menuDate);
					weeklyMenu.Dishes.Add(dish);
				}

				menuDate = menuDate.AddDays(1);
			}

			sw.Stop();
			Debug.WriteLine("[SabisReaderBase] Scraped {0} in {1}", this.RestaurantName, sw.Elapsed);

			return weeklyMenu;
		}

	}
}