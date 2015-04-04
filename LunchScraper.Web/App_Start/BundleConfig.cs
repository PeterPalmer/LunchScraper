using System.Web.Optimization;

namespace LunchScraper
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/js/bundle").Include(
				"~/Scripts/ScraperMain.js"));

			bundles.Add(new ScriptBundle("~/js/ieFixes").Include("~/Scripts/html5shiv.js", "~/Scripts/respond.src.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

#if !DEBUG
			BundleTable.EnableOptimizations = true;
#endif
		}
	}
}
