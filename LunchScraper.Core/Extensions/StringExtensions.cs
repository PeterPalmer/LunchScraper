using System;
using System.Net;
using System.Text.RegularExpressions;

namespace LunchScraper.Core.Extensions
{
	public static class StringExtensions
	{
		static public String StripHtmlTags(this String source)
		{
			String stripped = Regex.Replace(source, "(?s)<!--.*?-->", string.Empty);
			stripped = Regex.Replace(stripped, "<.*?>", string.Empty);
			return WebUtility.HtmlDecode(stripped).Trim();
		}
	}
}
