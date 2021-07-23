using System.Collections.Generic;

namespace Crawler.Logic
{
    public class SitemapCrawler
    {
		private ParserSitemap parser;
		public IEnumerable<string> GetUrls(string url)
		{
			List<string> listOfUrls = new List<string> { };

			parser = new ParserSitemap();

			var listOfSitemaps = parser.Parse(url, "sitemap") as List<string>;

			if (listOfSitemaps.Count == 0)
			{
				listOfUrls = parser.Parse(url, "url") as List<string>;
				return listOfUrls;
			}

			foreach (var sitemap in listOfSitemaps)
            {
				listOfUrls.AddRange(parser.Parse(sitemap, "url"));
            }

			return listOfUrls;
		}
	}
}
