using System.Collections.Generic;
using LunchScraper.Core.Domain;

namespace LunchScraper.Core.Storage
{
	public interface IMenuRepository
	{
		void StoreDish(Dish dish);
		void StoreDishes(IEnumerable<Dish> dishes);
		void DeleteThisWeek();
		IEnumerable<Dish> GetAllDishes();
		IEnumerable<Dish> GetThisWeekDishes();
	}
}