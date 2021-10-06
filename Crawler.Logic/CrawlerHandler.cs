using Crawler.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.Logic
{
    public class CrawlerHandler
    {
        private readonly HtmlCrawler _htmlCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly Validator _validator;
        private readonly Timer _timer;

        public CrawlerHandler(HtmlCrawler htmlCrawler, SitemapCrawler sitemapCrawler, Validator validator, Timer timer)
        {
            _htmlCrawler = htmlCrawler;
            _sitemapCrawler = sitemapCrawler;
            _validator = validator;
            _timer = timer;
        }

        public virtual IEnumerable<CrawlingResult> GetLinksFromHtmlAndSitemap(string url)
        {
            string errorMsg = _validator.IsValid(url);

            if (!(errorMsg == ""))
            {
                throw new ArgumentException(errorMsg);
            }

            var linksFromHtml = _htmlCrawler.GetUrls(url);

            var linksFromSitemap = _sitemapCrawler.GetUrls(url + "/sitemap.xml");

            var result = new List<CrawlingResult>();

            foreach (var item in linksFromHtml.Union(linksFromSitemap))
            {
                bool isInHtml = linksFromHtml.Contains(item);

                bool isInSitemap = linksFromSitemap.Contains(item);

                result.Add(new CrawlingResult()
                {
                    Url = item,
                    IsInHtml = isInHtml,
                    IsInSitemap = isInSitemap
                });
            }

            return result;
        }

        public virtual IEnumerable<TimeOfResponseResult> GetResponseTime(IEnumerable<CrawlingResult> links)
        {
            List<TimeOfResponseResult> result = new();

            foreach (var url in links)
            {
                var responseResult = _timer.CheckTimeResponse(url.Url);

                result.Add(responseResult);
            }

            return result;
        }
    }
}
