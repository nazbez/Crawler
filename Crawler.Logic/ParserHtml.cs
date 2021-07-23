using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Crawler.Logic
{
    public class ParserHtml
    {
        private readonly string _url;
        private readonly HtmlDocument _document;

        public ParserHtml(string url, string document)
        {
            _url = url;
            _document = new HtmlDocument();
            _document.LoadHtml(document);
        }

        public IEnumerable<string> ParseUrls()
        {
            List<string> listOfUrls = new List<string> { };

            var allUrls = _document.DocumentNode.SelectNodes("//a[@href]");

            if (allUrls == null)
            {
                return listOfUrls;
            }

            foreach (var node in allUrls)
            {
                string href = node.Attributes["href"].Value;

                href = GetAbsoluteUrlString(_url, href);

                if (href.Contains(_url) && !href.Contains("#"))
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
