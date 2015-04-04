using System.Collections.Generic;

namespace LunchScraper.Core.Domain
{
	public class LunchMenu
	{
		public List<Dish> Dishes { get; private set; }
		public string Restaurant { get; private set; }
		public string Webpage { get; private set; }

		public LunchMenu(string restaurant, string webpage)
		{
			this.Dishes = new List<Dish>();
			this.Restaurant = restaurant;
			this.Webpage = webpage;
		}
	}
}
