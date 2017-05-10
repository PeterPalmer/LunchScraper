using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class TegeluddenReader : KvartersmenynReaderBase
	{
		public TegeluddenReader(IWebScraper scraper)
			: base(scraper)
		{
		}

		protected override Restaurant Restaurant
		{
			get
			{
				return Restaurant.CafeTegeludden;
			}
		}
	}
}
