using System;
using System.Collections.Generic;
using System.IO;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class PontusFyranReader : IMenuReader
	{
		private readonly IPdfScraper _scraper;
		private readonly Dictionary<string, DateTime> _weekDays = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

		public PontusFyranReader(IPdfScraper scraper)
		{
			_scraper = scraper;

			_weekDays.Add("måndag", DateHelper.MondayThisWeek());
			_weekDays.Add("tisdag", DateHelper.TuesdayThisWeek());
			_weekDays.Add("onsdag", DateHelper.WednesdayThisWeek());
			_weekDays.Add("torsdag", DateHelper.ThursdayThisWeek());
			_weekDays.Add("fredag", DateHelper.FridayThisWeek());
		}

		public LunchMenu ReadWeeklyMenu()
		{
			var menu = new LunchMenu("Pontus på Fyran", "http://pontusfrithiof.com/pontus/pontus-pa-fyran/");

			var week = DateHelper.GetWeekNumber();
			var year = DateTime.Today.Year - 2000;
			var url = String.Format("http://pontusfrithiof.com/wp-content/uploads/2015/04/P4_V.{0}-{1}.pdf", week, year);

			string textFromPdf = _scraper.ScrapePdf(url);

			using (var reader = new StringReader(textFromPdf))
			{
				bool shouldAddDish = false;
				DateTime dishDate = DateTime.Today;
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					if (_weekDays.ContainsKey(line.Trim()))
					{
						shouldAddDish = true;
						dishDate = _weekDays[line.Trim()];
						continue;
					}

					if (string.IsNullOrWhiteSpace(line))
					{
						shouldAddDish = false;
					}

					if (shouldAddDish)
					{
						menu.Dishes.Add(new Dish(line.Trim(), dishDate));
					}
				}
			}

			return menu;
		}
	}
}
