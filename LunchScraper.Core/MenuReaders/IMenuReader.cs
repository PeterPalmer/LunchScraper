using System.Collections.Generic;
using LunchScraper.Core.Domain;

namespace LunchScraper.Core.MenuReaders
{
	public interface IMenuReader
	{
		List<Dish> ReadWeeklyMenu();
	}
}