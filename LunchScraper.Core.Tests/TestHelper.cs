using System.IO;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LunchScraper.Core.Utility;
using NSubstitute;

namespace LunchScraper.Core.Tests
{
	public static class TestHelper
	{
		public static IWebScraper MockWebScraper(string htmlFile)
		{
			string html;

			using (var streamReader = new StreamReader(GetFullPath(htmlFile)))
			{
				html = streamReader.ReadToEnd();
			}

			var scraper = Substitute.For<IWebScraper>();

			scraper.ScrapeWebPage(Arg.Any<string>()).Returns(html);

			return scraper;
		}

		public static IPdfScraper MockPdfScraper(string pdfFile)
		{
			string textFromPdf;

			using (var pdfReader = new PdfReader(GetFullPath(pdfFile)))
			{
				textFromPdf = PdfTextExtractor.GetTextFromPage(pdfReader, 1);
			}

			var mockedScraper = Substitute.For<IPdfScraper>();

			mockedScraper.ScrapePdf(Arg.Any<string>()).Returns(textFromPdf);

			return mockedScraper;
		}

		private static string GetFullPath(string pdfFile)
		{
			string currentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			if (currentDirectory == null)
			{
				return string.Empty;
			}

			var testFile = System.IO.Path.Combine(currentDirectory, pdfFile);
			return testFile;
		}
	}
}
