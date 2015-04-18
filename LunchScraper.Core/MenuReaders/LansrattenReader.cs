using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class LansrattenReader : SabisReaderBase
	{
		public LansrattenReader(IWebScraper scraper) : base(scraper, "Länsrätten", 1)
		{
		}

		protected override string MenuUrl
		{
			get
			{
				return string.Concat("http://www.sabis.se/lansforsakringar/dagens-lunch-v", DateHelper.GetWeekNumber());
			}
		}
	}
}
