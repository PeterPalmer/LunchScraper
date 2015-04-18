using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LunchScraper.Core.Domain;
using LunchScraper.Core.MenuReaders;
using LunchScraper.Models;

namespace LunchScraper.Controllers
{
	public class HomeController : Controller
	{
		private readonly IMenuReader[] _readers;

		public HomeController(IMenuReader[] readers)
		{
			_readers = readers;
		}

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

		private MenusModel ScrapeMenus()
		{
			var model = new MenusModel();
			var lunchMenus = new List<LunchMenu>();

			Parallel.ForEach(_readers, reader =>
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

			model.LunchMenus = lunchMenus.OrderBy(lm => lm.SortOrder);

			System.Web.HttpContext.Current.Application.Lock();
			System.Web.HttpContext.Current.Application["Model"] = model;
			System.Web.HttpContext.Current.Application.UnLock();

			return model;
		}
	}
}