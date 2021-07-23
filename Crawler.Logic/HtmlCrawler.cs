using System.Collections.Generic;
using System.Linq;

namespace Crawler.Logic
{
    public class HtmlCrawler
    {
		private Downloader downloader;
		private ParserHtml parser;

		public IEnumerable<string> GetUrls(string url)
		{
			List<string> result = new List<string> { };
			List<string> queueOfUrls = new List<string> { url };

			do
			{
				string currentUrl = queueOfUrls.First();

				downloader = new Downloader(currentUrl);

				string document = downloader.Download();

				if (string.IsNullOrEmpty(document))
                {
					queueOfUrls.RemoveAt(0);

					result.Add(currentUrl);

					continue;
				}

				parser = new ParserHtml(currentUrl, document);

				var foundedUrls = parser.ParseUrls();

				queueOfUrls.AddRange(foundedUrls);

				queueOfUrls.RemoveAt(0);

				result.Add(currentUrl);

				result = result.Distinct().ToList();

				queueOfUrls = queueOfUrls.Distinct().Where(url => !result.Contains(url)).ToList();

			} while (queueOfUrls.Count() != 0);

			return result;
		}
	}
}
