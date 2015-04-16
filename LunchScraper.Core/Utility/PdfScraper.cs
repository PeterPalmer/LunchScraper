using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace LunchScraper.Core.Utility
{
	public class PdfScraper : IPdfScraper
	{
		public string ScrapePdf(string url)
		{
			string textFromPdf;

			using (var reader = new PdfReader(url))
			{
				textFromPdf = PdfTextExtractor.GetTextFromPage(reader, 1);
			}

			return textFromPdf;
		}
	}
}
