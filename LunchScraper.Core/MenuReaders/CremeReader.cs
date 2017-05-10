using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Extensions;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class CremeReader : MenuReaderBase
	{
		private static DateTime EveryDayOfWeek = DateTime.MaxValue;

		public CremeReader(IWebScraper scraper) : base(scraper)
		{

		}

		private static readonly HashSet<string> _weekDays = new HashSet<string>(new[]
		{
			"måndag", "tisdag", "onsdag","torsdag","fredag"
		});

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.Creme.Url);
			var cq = new CQ(html);
			var currentDate = DateTime.MinValue;

			var dayMenu = cq["div.meny"].FirstOrDefault();

			if (dayMenu == null)
			{
				return dishes;
			}

			var date = DateHelper.MondayThisWeek().AddDays(-1);
			var dishesArray = dayMenu.InnerHTML.Split(new[] { "<br />", "<br>" }, StringSplitOptions.RemoveEmptyEntries);

			int offset = 0;
			if (StartsWithWeekDay(dishesArray))
			{
				date = date.AddDays(1);
				offset = 1;
			}

			bool areBelowWeekdays = false;
			string recurringDish = string.Empty;

			foreach (var dish in dishesArray.Skip(offset))
			{
				var description = dish.StripHtmlTags();

				if (_weekDays.Contains(description.ToLower().TrimEnd(':')))
				{
					date = date.AddDays(1);
					continue;
				}

				if (description.StartsWith("Veckans", StringComparison.OrdinalIgnoreCase))
				{
					recurringDish = description;
					areBelowWeekdays = true;
					continue;
				}

				if (!string.IsNullOrEmpty(recurringDish))
				{
					description = String.Concat(recurringDish, ": ", description);

					for (int i = 0; i < 5; i++)
					{
						dishes.Add(new Dish(description, DateHelper.MondayThisWeek().AddDays(i), Restaurant.Creme.Id));
					}

					recurringDish = string.Empty;
				}
				else if (!string.IsNullOrWhiteSpace(description))
				{
					if (areBelowWeekdays)
					{
						break;
					}

					dishes.Add(new Dish(description, date, Restaurant.Creme.Id));
				}
			}

			if (date == DateHelper.MondayThisWeek().AddDays(-1))
			{
				dishes.Clear();
			}

			return dishes;
		}

		private bool StartsWithWeekDay(string[] dishes)
		{
			if (!dishes.Any())
			{
				return false;
			}

			var firstLine = dishes[0].StripHtmlTags().ToLower();
			firstLine = firstLine.TrimEnd(':');

			return _weekDays.Contains(firstLine);
		}
	}
}
