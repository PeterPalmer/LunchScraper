using System;
using System.Collections.Generic;
using System.Web.Mvc;
using LunchScraper.Core.Domain;
using LunchScraper.Core.MenuReaders;
using LunchScraper.Core.Utility;
using LunchScraper.Models;

namespace LunchScraper.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			MenusModel model = GetModelFromSession();

			if (model == null)
			{
				model = ScrapeMenus();
			}

			return View(model);
		}

		private MenusModel GetModelFromSession()
		{
			var sessionObject = System.Web.HttpContext.Current.Application["Model"];

			if (!(sessionObject is MenusModel))
			{
				return null;
			}

			var model = (MenusModel)sessionObject;

			if (DateTime.Now - model.CreatedAt > new TimeSpan(0, 30, 0))
			{
				return null;
			}

			return model;
		}

		private static MenusModel ScrapeMenus()
		{
			var model = new MenusModel();

			var scraper = new WebScraper();

			var menuReaders = new List<IMenuReader>
			{
				new LansrattenReader(scraper),
				new EuropaReader(scraper),
				new TegeluddenReader(scraper),
				new TennishallenReader(scraper)
			};

			var lunchMenus = new List<LunchMenu>();

			foreach (var reader in menuReaders)
			{
				var menu = reader.ReadWeeklyMenu();
				lunchMenus.Add(menu);
			}

			model.LunchMenus = lunchMenus;

			System.Web.HttpContext.Current.Application.Lock();
			System.Web.HttpContext.Current.Application["Model"] = model;
			System.Web.HttpContext.Current.Application.UnLock();

			return model;
		}
	}
}