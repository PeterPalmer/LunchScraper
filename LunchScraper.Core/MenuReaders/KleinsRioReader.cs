using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Extensions;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class KleinsRioReader : MenuReaderBase
	{
		public KleinsRioReader(IWebScraper scraper)
			: base(scraper)
		{
		}

		private static readonly HashSet<string> _weekDays = new HashSet<string>(new[]
		{
			"måndag", "tisdag", "onsdag","torsdag","fredag"
		});

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.KleinsRio.Url);
			var cq = new CQ(html);

			var htmlTags = cq["div.entry-content p strong, div.entry-content h3 strong"].ToList();

			if (htmlTags == null || !htmlTags.Any())
			{
				return dishes;
			}

			var currentDate = DateHelper.MondayThisWeek().AddDays(-1);

			foreach (var tag in htmlTags)
			{
				var innerText = tag.InnerText.StripHtmlTags();

				if (StartsWithWeekDay(innerText))
				{
					currentDate = ParseDayOfWeek(innerText);
					continue;
				}

				if (string.IsNullOrWhiteSpace(innerText) && currentDate == DateHelper.FridayThisWeek())
				{
					break;
				}

				if (!string.IsNullOrWhiteSpace(innerText))
				{
					dishes.Add(new Dish(innerText, currentDate, Restaurant.KleinsRio.Id));
				}
			}

			if (currentDate == DateHelper.MondayThisWeek().AddDays(-1))
			{
				dishes.Clear();
			}

			return dishes;
		}

		private DateTime ParseDayOfWeek(string text)
		{
			switch (GetFirstWord(text).ToLower())
			{
				case "måndag":
					return DateHelper.MondayThisWeek();
				case "tisdag":
					return DateHelper.TuesdayThisWeek();
				case "onsdag":
					return DateHelper.WednesdayThisWeek();
				case "torsdag":
					return DateHelper.ThursdayThisWeek();
				case "fredag":
					return DateHelper.FridayThisWeek();
				default:
					return DateHelper.MondayThisWeek().AddDays(-1);
			}
		}

		private bool StartsWithWeekDay(string text)
		{
			return _weekDays.Contains(GetFirstWord(text).ToLower());
		}

		private string GetFirstWord(string text)
		{
			if (String.IsNullOrWhiteSpace(text))
			{
				return string.Empty;
			}

			var spacePos = text.IndexOf(' ');
			if (spacePos == -1)
			{
				return text;
			}

			return text.Substring(spacePos);
		}
	}
}
