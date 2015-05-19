using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class EuropaReader : SabisReaderBase
	{
		public EuropaReader(IWebScraper scraper) : base(scraper)
		{
		}

		protected override Restaurant Restaurant
		{
			get
			{
				return Restaurant.Europa;
			}
		}

	}
}