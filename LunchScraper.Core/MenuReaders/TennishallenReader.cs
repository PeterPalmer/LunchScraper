using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class TennishallenReader : SabisReaderBase
	{
		public TennishallenReader(IWebScraper scraper)
			: base(scraper, "Tennishallen")
		{
		}

		protected override string MenuUrl
		{
			get
			{
				return string.Concat("http://www.sabis.se/kungl-tennishallen/dagens-lunch-v", DateHelper.GetWeekNumber());
			}
		}
	}
}