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
			var model = new MenusModel();
			model.Date = DateHelper.ThursdayThisWeek();

			var scraper = new WebScraper();

			var menuReaders = new List<IMenuReader>();
			menuReaders.Add(new LansrattenReaderBase(scraper));
			menuReaders.Add(new EuropaReader(scraper));
			menuReaders.Add(new TennishallenReader(scraper));

			var lunchMenus = new List<LunchMenu>();

			foreach (var reader in menuReaders)
			{
				var menu = reader.ReadWeeklyMenu();
				lunchMenus.Add(menu);
			}

			model.LunchMenus = lunchMenus;

			return View(model);
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}