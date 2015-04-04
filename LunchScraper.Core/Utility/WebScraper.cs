using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace LunchScraper.Core.Utility
{
	public class WebScraper : IWebScraper
	{
		public string ScrapeWebPage(string url)
		{
			var timer = Stopwatch.StartNew();

			var htmlResult = SendWebRequest(url, 0);

			timer.Stop();
			Debug.WriteLine(String.Concat("Finished: ", url, " After: ", timer.Elapsed));

			return htmlResult;
		}

		private string SendWebRequest(string url, int retryAttempt)
		{
			try
			{
				//create request
				var request = (HttpWebRequest) WebRequest.Create(url);
				request.Pipelined = true;
				request.KeepAlive = true;
				request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
				request.Proxy = new WebProxy();
				request.Proxy.Credentials = CredentialCache.DefaultCredentials;

				//get response
				using (var response = (HttpWebResponse) request.GetResponse())
				{
					using (var responseStream = response.GetResponseStream())
					{
						if (responseStream == null)
						{
							throw new WebException("Response streams was null.");
						}

						if (response.ContentEncoding.ToLower().Contains("gzip"))
						{
							return ReadHtmlToEnd(new GZipStream(responseStream, CompressionMode.Decompress));
						}

						if (response.ContentEncoding.ToLower().Contains("deflate"))
						{
							return ReadHtmlToEnd(new DeflateStream(responseStream, CompressionMode.Decompress));
						}
						
						return ReadHtmlToEnd(responseStream);
					}
				}
			}
			catch (WebException ex)
			{
				var failedResponse = ex.Response as HttpWebResponse;
				if (failedResponse != null)
				{
					Debug.WriteLine("Failed with status code: {0} {1}", failedResponse.StatusCode, (int)failedResponse.StatusCode);
				}

				return Retry(url, retryAttempt);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);

				return Retry(url, retryAttempt);
			}
		}

		private string ReadHtmlToEnd(Stream stream)
		{
			using (var reader = new StreamReader(stream, Encoding.UTF8))
			{
				return reader.ReadToEnd();
			}
		}

		private string Retry(string url, int retryAttempt)
		{
			if (retryAttempt >= 3)
			{
				Debug.WriteLine("Gave up on url {0} after {1} attempts", url, retryAttempt);
				return string.Empty;
			}

			retryAttempt++;
			Debug.WriteLine("Retry attempt {0} for url {1}", retryAttempt, url);

			return this.SendWebRequest(url, retryAttempt);
		}
	}

}