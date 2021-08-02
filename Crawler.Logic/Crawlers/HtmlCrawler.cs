using System.Collections.Generic;
using System.Linq;
using Crawler.Logic.Parsers;

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
			List<string> result = new List<string>() { };
			List<string> queueOfUrls = new List<string>() { adress };

			while (queueOfUrls.Count() != 0)
            {
				string currentUrl = queueOfUrls.First();

				string document = _downloader.Download(currentUrl);

				if (string.IsNullOrEmpty(document))
				{
					queueOfUrls.RemoveAt(0);

					continue;
				}

				var foundedUrls = _parser.ParseUrls(currentUrl, document)
					                     .Where(x => !result.Contains(x) && !queueOfUrls.Contains(x));

				queueOfUrls.AddRange(foundedUrls);

				queueOfUrls.RemoveAt(0);

				result.Add(currentUrl);
			}

			return result;
		}
	}
}
