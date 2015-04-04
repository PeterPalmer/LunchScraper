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

		public string RestaurantName { get; protected set; }

		public abstract LunchMenu ReadWeeklyMenu();
	}
}
