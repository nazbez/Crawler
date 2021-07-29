﻿using System.Collections.Generic;
using System.Linq;

namespace Crawler.Logic
{
    public class CrawlerLinksHandler
    {
        private readonly Timer _timer;
        public CrawlerLinksHandler(Timer timer)
        {
            _timer = timer;
        }

        public virtual IEnumerable<CrawlingResult> GetResultOfCrawling(List<string> urlsFromHtml, List<string> urlsFromSitemap)
        {
            List<CrawlingResult> result = new List<CrawlingResult>() { };

            var allLinks = urlsFromHtml.Union(urlsFromSitemap);

            foreach(var url in allLinks)
            {
                int time = _timer.CheckTimeResponse(url);

                bool isInHtml = urlsFromHtml.Contains(url);

                bool isInSitemap = urlsFromSitemap.Contains(url);

                result.Add(new CrawlingResult() 
                { 
                    Url = url, 
                    Time = time, 
                    IsInHtml = isInHtml, 
                    IsInSitemap = isInSitemap
                });
            }

            return result.OrderBy(x=>x.Time);
        }
    }
}