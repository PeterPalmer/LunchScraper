using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class EuropaReader : SabisReaderBase
	{
		public EuropaReader(IWebScraper scraper)
			: base(scraper, "Europa")
		{
		}

		protected override string MenuUrl
		{
			get
			{
				return string.Concat("http://www.sabis.se/europa/dagens-lunch-v", DateHelper.GetWeekNumber());
			}
		}
	}
}