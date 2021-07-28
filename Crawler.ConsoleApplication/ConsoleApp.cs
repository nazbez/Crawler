using Crawler.Logic;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.ConsoleApplication
{
    public class ConsoleApp
    {
        private readonly ConsoleImitator _console;
        private readonly CrawlerLinksHandler _handler;
        private readonly HtmlCrawler _htmlCrawler;
        private readonly SitemapCrawler _sitemapCrawler;

        public ConsoleApp(ConsoleImitator console, CrawlerLinksHandler handler,
           HtmlCrawler htmlCrawler, SitemapCrawler sitemapCrawler)
        {
            _console = console;
            _handler = handler;
            _htmlCrawler = htmlCrawler;
            _sitemapCrawler = sitemapCrawler;
        }

        public void Interract()
        {
            _console.WriteLine("Enter the url adress of website: ");

            string url = _console.ReadLine();

            if (!IsValidInput(url))
            {
                _console.WriteLine("Your input is not url!");
                return;
            } 

            url = TransformToUnifiedForm(url);

            var linksFromHtml = _htmlCrawler.GetUrls(url);

            var linksFromSitemap = _sitemapCrawler.GetUrls(url + "/sitemap.xml");

            _console.WriteLine("Links which were founded only by html crawler");

            _console.WriteLine("");

            PrintUniqueLinks(linksFromHtml, linksFromSitemap);

            _console.WriteLine("Links which were founded only by sitemap crawler");

            _console.WriteLine("");

            PrintUniqueLinks(linksFromSitemap, linksFromHtml);

            var results = _handler.GetResultOfCrawling(linksFromHtml as List<string>, linksFromSitemap as List<string>);

            _console.WriteLine("\nResult of crawling: ");

            PrintResultOfCrawling(results);

            _console.WriteLine($"\nCount of links founded by html crawler: {linksFromHtml.Count()}");
            _console.WriteLine($"\nCount of links founded by sitemap crawler: {linksFromSitemap.Count()}");

        }

        private bool IsValidInput(string url)
        {
            return url.StartsWith("https://") || url.StartsWith("http://");
        }

        private string TransformToUnifiedForm(string str)
        {
            return str.EndsWith("/") ? str.Substring(0, str.Length - 1) : str;
        }

        private void PrintUniqueLinks(IEnumerable<string> listOfUrls1, IEnumerable<string> listOfUrls2)
        {
            var result = listOfUrls1.Except(listOfUrls2);

            if (result.Count() == 0)
            {
                _console.WriteLine("There are no unique links!");
                return;
            }

            int count = 0;

            foreach(var url in result)
            {
                _console.WriteLine($"{++count}) {url}");
            }
        }

        private void PrintResultOfCrawling(IEnumerable<CrawlingResult> results)
        {
            if (results.Count() == 0)
            {
                _console.WriteLine("There are no links!");
                return;
            }

            int count = 0;

            foreach(var result in results)
            {
                _console.WriteLine($"{++count}) url: {result.Url};  time of response: {result.Time}");
            }
        }
    }
}
