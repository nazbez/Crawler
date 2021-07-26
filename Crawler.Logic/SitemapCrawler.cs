using System.Collections.Generic;

namespace Crawler.Logic
{
    public class SitemapCrawler
	{ 
		private ParserSitemap parser;
		private Downloader downloader;
		private string url;

		public SitemapCrawler(string url, ParserSitemap parser, Downloader downloader)
        {
			this.parser = parser;
			this.downloader = downloader;
			this.url = url;
        }
		public IEnumerable<string> GetUrls()
		{
			List<string> listOfUrls = new List<string> { };

			string document = downloader.Download(url);

			if (string.IsNullOrEmpty(document))
            {
				return listOfUrls;
            }

			var listOfSitemaps = parser.Parse(document, url, "sitemap") as List<string>;

			if (listOfSitemaps.Count == 0)
			{
				listOfUrls = parser.Parse(document, url, "url") as List<string>;
				return listOfUrls;
			}

			foreach (var sitemap in listOfSitemaps)
            {
				document = downloader.Download(sitemap);
				listOfUrls.AddRange(parser.Parse(document, sitemap, "url"));
            }

			return listOfUrls;
		}
	}
}
