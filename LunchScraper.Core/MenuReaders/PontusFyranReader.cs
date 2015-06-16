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

		public List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var week = DateHelper.GetWeekNumber();
			var month = DateTime.Today.Month;
			var year = DateTime.Today.Year;

			var url = String.Format("http://pontusfrithiof.com/wp-content/uploads/{0}/{1}/P4_V.{2}-{3}.pdf", year, month.ToString("D2"), week, year - 2000);

			string textFromPdf = "";
			try
			{
				textFromPdf = _scraper.ScrapePdf(url);
			}
			catch (System.Net.WebException)
			{
				url = String.Format("http://pontusfrithiof.com/wp-content/uploads/{0}/{1}/V.{2}-{3}.pdf", year, month.ToString("D2"), week, year - 2000);
				textFromPdf = _scraper.ScrapePdf(url);
			}

			using (var reader = new StringReader(textFromPdf))
			{
				bool shouldAddDish = false;
				DateTime dishDate = DateTime.Today;
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					if (string.IsNullOrWhiteSpace(line))
					{
						continue;
					}

					var split = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					var firstWord = split[0];

					if (_weekDays.ContainsKey(firstWord.Trim()))
					{
						shouldAddDish = true;

						if (_weekDays.TryGetValue(firstWord, out dishDate))
						{
							continue;
						}
					}

					if (line.Trim().Equals("FYRANS FREDAGSLÅDA", StringComparison.OrdinalIgnoreCase))
					{
						break;
					}

					if (shouldAddDish)
					{
						dishes.Add(new Dish(line.Trim(), dishDate, Restaurant.PontusPåFyran.Id));
					}
				}
			}

			return dishes;
		}
	}
}
