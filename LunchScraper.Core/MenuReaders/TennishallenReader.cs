using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class TennishallenReader : SabisReaderBase
	{
		public TennishallenReader(IWebScraper scraper) : base(scraper)
		{
		}

		protected override Restaurant Restaurant
		{
			get
			{
				return Restaurant.TennisHallen;
			}
		}
	}
}