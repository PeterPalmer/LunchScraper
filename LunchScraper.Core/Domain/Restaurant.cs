using System.Collections.Generic;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.Domain
{
	public class Restaurant
	{
		public int Id { get; private set; }
		public string Name { get; private set; }
		public string Url { get; private set; }

		public Restaurant(int id, string name, string url)
		{
			this.Id = id;
			this.Name = name;
			this.Url = url;
		}

		public static Restaurant Länsrätten = new Restaurant(1, "Länsrätten", string.Concat("http://www.sabis.se/lansforsakringar/dagens-lunch-v", DateHelper.GetWeekNumber()));
		public static Restaurant Europa = new Restaurant(2, "Europa", string.Concat("http://www.sabis.se/europa/dagens-lunch-v", DateHelper.GetWeekNumber()));
		public static Restaurant PontusPåFyran = new Restaurant(3, "Pontus på Fyran", "http://pontusfrithiof.com/pontus/pontus-pa-fyran/");
		public static Restaurant CafeTegeludden = new Restaurant(4, "Café Tegeludden", "http://cafetegeludden.kvartersmenyn.se/");
		public static Restaurant TennisHallen = new Restaurant(5, "Tennishallen", string.Concat("http://www.sabis.se/kungl-tennishallen/dagens-lunch-v", DateHelper.GetWeekNumber()));

		public static Restaurant GetById(int id)
		{
			Dictionary<int, Restaurant> lookup = new Dictionary<int, Restaurant>
			{
				{1, Länsrätten},
				{2, Europa},
				{3, PontusPåFyran},
				{4, CafeTegeludden},
				{5, TennisHallen},
			};

			return lookup[id];
		}
	}
}
