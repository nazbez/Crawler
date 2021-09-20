using Crawler.Logic.Parsers;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.Logic
{
    public class HtmlCrawler
    {
		private readonly Downloader _downloader;
		private readonly ParserHtml _parser;

		public HtmlCrawler(ParserHtml parser, Downloader downloader)
        {
			_parser = parser;
			_downloader = downloader;
        }
		public virtual IEnumerable<string> GetUrls(string adress)
		{
			List<string> result = new List<string>();
			List<string> queueOfUrls = new List<string>() { adress };

			while (queueOfUrls.Count != 0)
            {
				string currentUrl = queueOfUrls.First();

				string document = _downloader.Download(currentUrl);

				if (string.IsNullOrEmpty(document))
				{
					queueOfUrls.RemoveAt(0);

					continue;
				}

				var foundedUrls = _parser.ParseUrls(currentUrl, document);

				var newLinks = foundedUrls
					           .Where(url => !result.Contains(url) && !queueOfUrls.Contains(url));

				queueOfUrls.AddRange(newLinks);

				queueOfUrls.RemoveAt(0);

				result.Add(currentUrl);
			}

			return result;
		}
	}
}
