using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Storage;
using LunchScraper.DataStructures;
using LunchScraper.Models;
using WebGrease.Css.Extensions;

namespace LunchScraper.Controllers
{
	public class HomeController : Controller
	{
		private readonly MenuRepository _repository;
		private readonly Random _randomizer = new Random((int)DateTime.Now.Ticks);

		public HomeController(MenuRepository repository)
		{
			_repository = repository;
		}

		public ActionResult Index()
		{
			MenusModel model = GetModelFromSession();

			if (model == null)
			{
				model = RetrieveMenus();
			}

			return View(model);
		}

		public ActionResult Refresh()
		{
			MenusModel model = RetrieveMenus();

			return View("Index", model);
		}

		public ActionResult Approx()
		{
			var markovChains = this.GetMarkovChains();

			var lunchMenus = new List<LunchMenu>();

			for (int i = 0; i < markovChains.Count; i++)
			{
				var wordCount = 3 + _randomizer.Next(0, 5);
				string description = markovChains[i].GenerateSentence(wordCount, 15);
				var dishes = new List<Dish> { new Dish(description, DateTime.Today, 1) };

				lunchMenus.Add(new LunchMenu(Restaurant.GetById(i + 1), dishes));
			}

			var model = new MenusModel { LunchMenus = lunchMenus };

			return View(model);
		}

		private List<MarkovChain> GetMarkovChains()
		{
			var sessionObject = System.Web.HttpContext.Current.Application["MarkovChain"];

			if (sessionObject is List<MarkovChain>)
			{
				return sessionObject as List<MarkovChain>;
			}

			var allDishes = _repository.GetAllDishes();

			var markovChains = new List<MarkovChain>();

			foreach (var dishesGroupedByRestaurant in allDishes.GroupBy(d => d.RestaurantId).OrderBy(g => g.Key))
			{
				var dishes = dishesGroupedByRestaurant.Select(d => d.Description);

				var markov = new MarkovChain();
				dishes.ForEach(txt =>
				{
					markov.AddMultipleWords(txt);
					markov.AddWord(".");
				});

				markovChains.Add(markov);
			}

			System.Web.HttpContext.Current.Application.Lock();
			System.Web.HttpContext.Current.Application["MarkovChain"] = markovChains;
			System.Web.HttpContext.Current.Application.UnLock();

			return markovChains;
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

		private MenusModel RetrieveMenus()
		{
			var model = new MenusModel();
			var lunchMenus = new List<LunchMenu>();

			var dishes = _repository.GetThisWeekDishes();

			foreach (var dishGroup in dishes.GroupBy(d => d.RestaurantId))
			{
				var restaurant = Restaurant.GetById(dishGroup.Key);
				lunchMenus.Add(new LunchMenu(restaurant, dishGroup));
			}

			model.LunchMenus = lunchMenus.OrderBy(lm => lm.Restaurant.Id);

			System.Web.HttpContext.Current.Application.Lock();
			System.Web.HttpContext.Current.Application["Model"] = model;
			System.Web.HttpContext.Current.Application.UnLock();

			return model;
		}
	}
}