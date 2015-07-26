using System;
using System.Configuration;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LunchScraper.AzureKeepWarmJob
{
	class Program
	{
		static void Main(string[] args)
		{
			var username = ConfigurationManager.AppSettings["CredentialUsername"];
			var password = ConfigurationManager.AppSettings["CredentialPassword"];

			var client = new HttpClient(new HttpClientHandler()
			{
				Credentials = new NetworkCredential(username, password)
			});

			var runner = new Runner(client);
			const string siteUrl = "https://lunchscraper.scm.azurewebsites.net/azurejobs/#/jobs/triggered/MenuReaderJob";
			const int waitTime = 300;

			Task.WaitAll(runner.HitSite(siteUrl, waitTime));
		}

		private class Runner
		{
			private readonly HttpClient _client;

			public Runner(HttpClient client)
			{
				_client = client;
			}

			public async Task HitSite(string siteUrl, int waitTime)
			{
				while (true)
				{
					try
					{
						var request = await _client.GetAsync(new Uri(siteUrl));
						Trace.TraceInformation("{0}: {1}", DateTime.Now, request.StatusCode);
					}
					catch (Exception ex)
					{
						Trace.TraceError(ex.ToString());
					}
					await Task.Delay(waitTime * 1000);
				}
			}
		}
	}
}
