using LunchScraper.Core.MenuReaders;
using LunchScraper.Core.Utility;
using Ninject.Modules;

namespace LunchScraper.MenuReaderJob
{
	public class NinjectBindings : NinjectModule
	{
		public override void Load()
		{
			Bind<IWebScraper>().To<WebScraper>();
			Bind<IPdfScraper>().To<PdfScraper>();

			Bind<IMenuReader>().To<LansrattenReader>();
			Bind<IMenuReader>().To<EuropaReader>();
			Bind<IMenuReader>().To<PontusFyranReader>();
			Bind<IMenuReader>().To<TegeluddenReader>();
			Bind<IMenuReader>().To<TennishallenReader>();
		}
	}
}
