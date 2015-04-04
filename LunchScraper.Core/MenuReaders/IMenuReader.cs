using LunchScraper.Core.Domain;

namespace LunchScraper.Core.MenuReaders
{
	public interface IMenuReader
	{
		LunchMenu ReadWeeklyMenu();
	}
}