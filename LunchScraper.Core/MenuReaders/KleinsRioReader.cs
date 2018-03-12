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

		private static readonly HashSet<string> _weekDays = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"måndag", "tisdag", "onsdag","torsdag","fredag"
		};

		private static readonly HashSet<string> _breakTags = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"Steak Frites 125:-", "Öppet: Mån – Fre 10:00 – 16:00", "pris 99.-", "Öppet: Mån – Fre 10.00 – 16.00. Frukost från 10.00. Lunch 11.00 – 14.00."
		};

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.KleinsRio.Url);
			var cq = new CQ(html);

			var htmlTags = cq["div.entry-content p strong, div.entry-content p, div.entry-content h3 strong, div.entry-content h3"].ToList();

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

				if ((string.IsNullOrWhiteSpace(innerText) && tag.NodeName.Equals("STRONG") || _breakTags.Contains(innerText)) && currentDate == DateHelper.FridayThisWeek())
				{
					break;
				}

				if (!string.IsNullOrWhiteSpace(innerText))
				{
					dishes.Add(new Dish(innerText, currentDate, Restaurant.KleinsRio.Id));
				}

				if (innerText.Equals("Steak Frites 125:-", StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
			}

			dishes.RemoveAll(d => d.Date == DateHelper.MondayThisWeek().AddDays(-1));

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
			return _weekDays.Contains(GetFirstWord(text));
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
