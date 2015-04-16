using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
				new TennishallenReader(scraper),
				new PontusFyranReader(new PdfScraper())
			};

			var lunchMenus = new List<LunchMenu>();

			Parallel.ForEach(menuReaders, reader =>
			{
				try
				{
					var menu = reader.ReadWeeklyMenu();
					lunchMenus.Add(menu);
				}
				catch (Exception ex)
				{
					var exceptionModel = new HandleErrorInfo(ex, reader.GetType().Name, "ReadWeeklyMenu");
				}
			});

			model.LunchMenus = lunchMenus.OrderBy(lm => lm.Restaurant);

			System.Web.HttpContext.Current.Application.Lock();
			System.Web.HttpContext.Current.Application["Model"] = model;
			System.Web.HttpContext.Current.Application.UnLock();

			return model;
		}
	}
}