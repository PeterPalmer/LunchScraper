using System;
using System.Collections.Generic;
using System.Linq;

namespace LunchScraper.Core.Domain
{
	public class LunchMenu
	{
		public List<Dish> Dishes { get; private set; }
		public Restaurant Restaurant { get; private set; }

		public LunchMenu(Restaurant restaurant, IEnumerable<Dish> dishes)
		{
			this.Restaurant = restaurant;
			this.Dishes = dishes.ToList();
		}

		public IEnumerable<Dish> GetDishes(DateTime day)
		{
			return Dishes.Where(d => d.Date.Date == day).OrderBy(d => d.Description);
		}
	}
}
