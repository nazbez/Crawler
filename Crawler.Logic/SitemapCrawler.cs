using System.Collections.Generic;

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

		public virtual IEnumerable<string> GetUrls(string adress)
		{
			string url = adress;

			List<string> listOfUrls = new List<string> { };

			string document = _downloader.Download(url);

			if (string.IsNullOrEmpty(document))
            {
				return listOfUrls;
            }

			var listOfSitemaps = _parser.Parse(document, url, "sitemap") as List<string>;

			if (IsEmptyListOfIndexedSitemaps(listOfSitemaps))
			{
				listOfUrls = _parser.Parse(document, url, "url") as List<string>;
				return listOfUrls;
			}

			foreach (var sitemap in listOfSitemaps)
            {
				document = _downloader.Download(sitemap);
				listOfUrls.AddRange(_parser.Parse(document, sitemap, "url"));
            }

			return listOfUrls;
		}

		private bool IsEmptyListOfIndexedSitemaps(List<string> listOfSitemaps)
        {
			return listOfSitemaps.Count == 0;
		}
	}
}
