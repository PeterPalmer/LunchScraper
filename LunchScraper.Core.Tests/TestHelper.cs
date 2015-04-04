using System.IO;
using System.Reflection;
using LunchScraper.Core.Utility;
using NSubstitute;

namespace LunchScraper.Core.Tests
{
	public static class TestHelper
	{
		public static IWebScraper MockWebScraper(string htmlFile)
		{
			string currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			var testFile = Path.Combine(currentDirectory, htmlFile);

			string html;

			using (var streamReader = new StreamReader(testFile))
			{
				html = streamReader.ReadToEnd();
			}

			var scraper = Substitute.For<IWebScraper>();

			scraper.ScrapeWebPage(Arg.Any<string>()).Returns(html);

			return scraper;
		}
	}
}
