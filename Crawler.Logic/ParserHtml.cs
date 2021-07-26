using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Crawler.Logic
{
    public class ParserHtml
    {
        public virtual IEnumerable<string> ParseUrls(string adress, string doc)
        {
            string url = adress;

            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(doc);

            List<string> listOfUrls = new List<string> { };

            var allUrls = document.DocumentNode.SelectNodes("//a[@href]");

            if (allUrls == null)
            {
                return listOfUrls;
            }

            foreach (var node in allUrls)
            {
                string href = node.Attributes["href"].Value;

                href = GetAbsoluteUrlString(url, href);

                if (href.Contains(url) && !href.Contains("#"))
                { 
                    listOfUrls.Add(href);
                }
            }

            return listOfUrls;
        }

        private string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);

            // Check that the link does not end in /
            return (url.ToString().EndsWith('/'))
                ? uri.ToString().Substring(0, uri.ToString().Length - 1)
                : uri.ToString();
        }
    }
}
