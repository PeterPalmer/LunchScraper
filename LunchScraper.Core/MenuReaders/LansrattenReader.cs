using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class LansrattenReaderBase : SabisReaderBase
	{
		public LansrattenReaderBase(IWebScraper scraper)
			: base(scraper, "Länsrätten")
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
