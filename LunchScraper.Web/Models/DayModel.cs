using System;
using LunchScraper.Core.Utility;

namespace LunchScraper.Models
{
	public class DayModel
	{
		public DayModel(DateTime date)
		{
			this.Date = date;
			this.Id = (int)date.DayOfWeek;

			if (date == DateTime.Today)
			{
				IsVisible = true;
			}
		}

		public int Id { get; set; }
		public DateTime Date { get; private set; }
		public bool IsVisible { get; set; }

		public String Today
		{
			get
			{
				return DateHelper.VeckodagPåSvenska(Date.DayOfWeek).ToUpper();
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
				return DayOfWeek.Sunday < Date.DayOfWeek && Date.DayOfWeek < DayOfWeek.Friday;
			}
		}

		public bool ShowPrevioustDayLink
		{
			get
			{
				return this.Date.DayOfWeek > DayOfWeek.Monday;
			}
		}

		public bool ShowApproxLink
		{
			get
			{
				return this.Date.DayOfWeek >= DayOfWeek.Friday || this.Date.DayOfWeek == DayOfWeek.Sunday;
			}
		}
	}
}