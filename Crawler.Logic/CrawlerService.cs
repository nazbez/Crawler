using Crawler.Logic.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Crawler.Logic
{
    public class CrawlerService
    {
        private readonly HtmlCrawler _htmlCrawler;
        private readonly SitemapCrawler _sitemapCrawler;
        private readonly Validator _validator;
        private readonly Timer _timer;

        public CrawlerService(HtmlCrawler htmlCrawler, SitemapCrawler sitemapCrawler, Validator validator, Timer timer)
        {
            _htmlCrawler = htmlCrawler;
            _sitemapCrawler = sitemapCrawler;
            _validator = validator;
        }

        public virtual IEnumerable<CrawlingResult> Crawl(string url)
        {
            if (!_validator.IsValid(url))
            {
                throw new ArgumentException("\nError! Invalid input!\n");
            }

            var linksFromHtml = _htmlCrawler.GetUrls(url);

            var linksFromSitemap = _sitemapCrawler.GetUrls(url + "/sitemap.xml");

            var result = new List<CrawlingResult> { };

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

        public virtual IEnumerable<TimeOfResponseResult> GetTimeOfResponses(IEnumerable<CrawlingResult> links)
        {
 
            List<TimeOfResponseResult> result = new List<TimeOfResponseResult>() { };

            foreach (var url in links)
            {
                int time = _timer.CheckTimeResponse(url.Url);

                result.Add(new TimeOfResponseResult()
                {
                    Url = url.Url,
                    Time = time,
                });
            }

            return result;
        }
    }
}
