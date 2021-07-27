using System.Collections.Generic;
using System.Linq;

namespace Crawler.Logic
{
    public class HtmlCrawler
    {
		private readonly Downloader _downloader;
		private readonly ParserHtml _parser;
		private string url;

		public HtmlCrawler(string url, ParserHtml parser, Downloader downloader)
        {
			this.url = url;
			_parser = parser;
			_downloader = downloader;
        }
		public IEnumerable<string> GetUrls()
		{
			List<string> result = new List<string>() { };
			List<string> queueOfUrls = new List<string>() { url };

			do
			{
				string currentUrl = queueOfUrls.First();

				string document = _downloader.Download(currentUrl);

				if (string.IsNullOrEmpty(document))
                {
					queueOfUrls.RemoveAt(0);

					continue;
				}

				var foundedUrls = _parser.ParseUrls(currentUrl, document);

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
