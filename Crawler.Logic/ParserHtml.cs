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

                href = ConvertToUnifiedForm(href);

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

            return uri.ToString();
        }

        private string ConvertToUnifiedForm(string adress)
        {
            return (adress.EndsWith('/'))
                ? adress.Substring(0, adress.Length - 1)
                : adress;
        }
    }
}
