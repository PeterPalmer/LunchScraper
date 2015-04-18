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
			Days = new List<DayModel>
			{
				new DayModel(DateHelper.MondayThisWeek()),
				new DayModel(DateHelper.TuesdayThisWeek()),
				new DayModel(DateHelper.WednesdayThisWeek()),
				new DayModel(DateHelper.ThursdayThisWeek()),
				new DayModel(DateHelper.FridayThisWeek()),
				new DayModel(DateHelper.SaturdayThisWeek()),
				new DayModel(DateHelper.SundayThisWeek())
			};

			CreatedAt = DateTime.Now;
		}

		public DateTime CreatedAt { get; set; }
		public IEnumerable<LunchMenu> LunchMenus { get; set; }
		public List<DayModel> Days { get; private set; }
	}
}
