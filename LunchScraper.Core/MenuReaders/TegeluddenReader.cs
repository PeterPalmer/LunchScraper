﻿using System;
using System.Collections.Generic;
using System.Linq;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Extensions;
using LunchScraper.Core.Utility;

namespace LunchScraper.Core.MenuReaders
{
	public class TegeluddenReader : MenuReaderBase
	{
		private static readonly HashSet<string> _weekDays = new HashSet<string>(new[]
		{
			"måndag", "tisdag", "onsdag","torsdag","fredag"
		});

		public TegeluddenReader(IWebScraper scraper)
			: base(scraper)
		{
		}

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage(Restaurant.CafeTegeludden.Url);
			var cq = new CQ(html);

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

			foreach (var dish in dishesArray.Skip(offset))
			{
				var description = dish.StripHtmlTags();

				if (_weekDays.Contains(description.ToLower().TrimEnd(':')))
				{
					date = date.AddDays(1);
					continue;
				}

				if (!string.IsNullOrWhiteSpace(description))
				{
					dishes.Add(new Dish(dish.StripHtmlTags(), date, Restaurant.CafeTegeludden.Id));
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

			if (!_weekDays.Contains(firstLine))
			{
				return false;
			}

			return true;
		}
	}
}
