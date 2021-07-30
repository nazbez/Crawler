using System.Collections.Generic;

namespace Crawler.Logic
{
    public class CrawlerService
    {
        private readonly HtmlCrawler _htmlCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly CrawlerLinksHandler _handler;

        public CrawlerService(HtmlCrawler htmlCrawler, SitemapCrawler sitemapCrawler, CrawlerLinksHandler handler)
        {
            _htmlCrawler = htmlCrawler;
            _sitemapCrawler = sitemapCrawler;
            _handler = handler;
        }
        public virtual IEnumerable<CrawlingResult> Crawl(string url)
        {
            var linksFromHtml = _htmlCrawler.GetUrls(url);

            var linksFromSitemap = _sitemapCrawler.GetUrls(url + "/sitemap.xml");

            return _handler.GetResultOfCrawling(linksFromHtml as List<string>, linksFromSitemap as List<string>);
        }
    }
}
