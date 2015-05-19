using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class LansrattenReader : SabisReaderBase
	{
		public LansrattenReader(IWebScraper scraper) : base(scraper)
		{
		}

		protected override Restaurant Restaurant
		{
			get
			{
				return Restaurant.Länsrätten;
			}
		}
	}
}
