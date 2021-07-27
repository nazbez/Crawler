using Crawler.Logic;
using System;
using System.Collections.Generic;

namespace Crawler.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new HtmlCrawler("https://www.ukad-group.com", new ParserHtml(), new Downloader());
            Console.WriteLine(((List<string>)x.GetUrls()).Count);

            Console.WriteLine();

            var a = new SitemapCrawler("https://www.ukad-group.com/sitemap.xml", new ParserSitemap(), new Downloader());
            Console.WriteLine(((List<string>)a.GetUrls()).Count);
        }
    }
}
