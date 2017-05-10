using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class KleinsRioReader : KvartersmenynReaderBase
	{
		public KleinsRioReader(IWebScraper scraper)
			: base(scraper)
		{
		}

		protected override Restaurant Restaurant
		{
			get
			{
				return Restaurant.KleinsRio;
			}
		}
	}
}
