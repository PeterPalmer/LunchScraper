using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using CsQuery;
using LunchScraper.Core.Domain;
using LunchScraper.Core.Utility;
using LunchScraper.Core.Extensions;

namespace LunchScraper.Core.MenuReaders
{
	public class CremeFejanReader : MenuReaderBase
	{
		private readonly HashSet<string> _validHeaders;
		private readonly Dictionary<string, DateTime> _dateLookup;

		private const string VeckansPasta = "Veckans Pasta";
		private const string VeckansSallad = "Veckans Sallad";

		public CremeFejanReader(IWebScraper scraper) : base(scraper)
		{
			_validHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Måndag", "Tisdag", "Onsdag", "Torsdag", "Fredag", VeckansPasta, VeckansSallad };

			_dateLookup = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase)
			{
				{"Måndag", DateHelper.MondayThisWeek()},
				{"Tisdag", DateHelper.TuesdayThisWeek()},
				{"Onsdag", DateHelper.WednesdayThisWeek()},
				{"Torsdag", DateHelper.ThursdayThisWeek()},
				{"Fredag", DateHelper.FridayThisWeek()}
			};
		}

		public override List<Dish> ReadWeeklyMenu()
		{
			var dishes = new List<Dish>();

			var html = Scraper.ScrapeWebPage("https://www.facebook.com/Crème-Gärdet-228787733810379/timeline");
			var cq = new CQ(html);

			var hiddenElements = cq["code"];

			var builder = new StringBuilder();

			foreach (var hiddenElem in hiddenElements)
			{
				builder.AppendLine(hiddenElem.InnerHTML.Replace("<!--", "").Replace("-->", ""));
			}

			var newHtml = builder.ToString();
			cq = new CQ(newHtml);

			var userContentWrappers = cq[".userContentWrapper"];

			if (userContentWrappers == null)
			{
				return dishes;
			}

			var monday = DateHelper.MondayThisWeek();
			var friday = DateHelper.FridayThisWeek();

			foreach (var wrapper in userContentWrappers)
			{
				var dateTag = cq.Select("abbr", wrapper).FirstElement();
				var epochDate = dateTag?.Attributes["data-utime"];

				var postDate = epochDate.FromUnixTime();

				if (postDate < monday || postDate > friday)
				{
					continue;
				}

				var paragraphs = cq.Select(".userContent p", wrapper).ToList();

				foreach (var paragraph in paragraphs)
				{
					var contentText = WebUtility.HtmlDecode(paragraph.InnerHTML).Trim();

					string header;
					var description = ParseContent(contentText, out header);

					if (string.IsNullOrEmpty(header))
					{
						continue;
					}

					if (header.Equals(VeckansPasta, StringComparison.OrdinalIgnoreCase) ||
						header.Equals(VeckansSallad, StringComparison.OrdinalIgnoreCase))
					{
						dishes.AddRange(_dateLookup.Values.Select(weekday => new Dish(description, weekday, Restaurant.Creme.Id)));
						continue;
					}

					var menuDate = _dateLookup[header];
					var dish = new Dish(description, menuDate, Restaurant.Creme.Id);
					dishes.Add(dish);
				}

			}

			return dishes;
		}

		private string ParseContent(string contentText, out string header)
		{
			var lines = contentText.Split(new[] { "<br>", "<br />" }, StringSplitOptions.RemoveEmptyEntries);
			lines = lines.Select(line => line.Replace("95:-", "").Trim()).ToArray();
			header = null;

			if (!lines.Any(IsMenuHeader))
			{
				return String.Empty;
			}

			header = lines.First(IsMenuHeader);

			return String.Join(" ", lines.Skip(1).Where(line => !IsMenuHeader(line)));
		}

		private bool IsMenuHeader(string input)
		{
			return _validHeaders.Contains(input);
		}
	}
}
