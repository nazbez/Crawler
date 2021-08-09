using System.Collections.Generic;
using System.Linq;
using Crawler.Logic.Parsers;

namespace Crawler.Logic
{
    public class SitemapCrawler
	{ 
		private readonly ParserSitemap _parser;
		private readonly Downloader _downloader;

		public SitemapCrawler(ParserSitemap parser, Downloader downloader)
        {
			_parser = parser;
			_downloader = downloader;
        }

		public virtual IEnumerable<string> GetUrls(string url)
		{
			List<string> listOfUrls = new List<string>();

			string document = _downloader.Download(url);

			if (string.IsNullOrEmpty(document))
            {
				return listOfUrls;
            }

			var listOfSitemaps = _parser.Parse(document, url, Tag.Sitemap);

			if (listOfSitemaps.Count() == 0)
			{
				listOfUrls = _parser.Parse(document, url, Tag.Url).ToList();

				return listOfUrls;
			}

			foreach (var sitemap in listOfSitemaps)
            {
				document = _downloader.Download(sitemap);

				var parsedLinks = _parser.Parse(document, sitemap, Tag.Url)
					                     .Where(x => !listOfUrls.Contains(x));

				listOfUrls.AddRange(parsedLinks);
            }

			return listOfUrls;
		}
	}
}
