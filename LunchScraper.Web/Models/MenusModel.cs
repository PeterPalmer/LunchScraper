using System;
using System.Collections.Generic;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;

namespace LunchScraper.Models
{
	public class MenusModel
	{
		public MenusModel()
		{
			this.Date = DateTime.Today;
		}

		public IEnumerable<LunchMenu> LunchMenus { get; set; }
		public DateTime Date { get; set; }

		public String Today
		{
			get
			{
				return DateHelper.VeckodagPåSvenska(Date.DayOfWeek);
			}
		}

		public String Yesterday
		{
			get
			{
				return DateHelper.VeckodagPåSvenska(Date.DayOfWeek - 1);
			}
		}

		public String Tomorrow
		{
			get
			{
				return DateHelper.VeckodagPåSvenska(Date.DayOfWeek + 1);
			}
		}

		public bool ShowNextDayLink
		{
			get
			{
				return this.Date.DayOfWeek < DayOfWeek.Friday;
			}
		}

		public bool ShowPrevioustDayLink
		{
			get
			{
				return this.Date.DayOfWeek > DayOfWeek.Monday;
			}
		}
	}
}