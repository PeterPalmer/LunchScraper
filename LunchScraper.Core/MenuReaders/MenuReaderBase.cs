using System.Collections.Generic;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public abstract class MenuReaderBase: IMenuReader
	{
		protected readonly IWebScraper Scraper;

		protected MenuReaderBase(IWebScraper scraper)
		{
			this.Scraper = scraper;
		}

		public abstract List<Dish> ReadWeeklyMenu();
	}
}
