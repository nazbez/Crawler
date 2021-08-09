using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Crawler.Logic.Parsers
{
    public class ParserHtml
    {
        public virtual IEnumerable<string> ParseUrls(string url, string doc)
        {
            HtmlDocument document = new HtmlDocument();

            document.LoadHtml(doc);

            List<string> listOfUrls = new List<string>();

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

                if (href.Contains("#"))
                {
                    continue;
                }

                if (href.Contains(new Uri(url).Host) && !listOfUrls.Contains(href))
                {
                    listOfUrls.Add(href);
                }
            }

            return listOfUrls;
        }

        private string GetAbsoluteUrlString(string baseUrl, string url)
        {
            try
            {
                Uri uri = new Uri(new Uri(baseUrl), url);

                return uri.ToString();
            }
            catch (UriFormatException)
            {
                return string.Empty;
            }
        
        }  

        private string ConvertToUnifiedForm(string adress)
        {
            return (adress.EndsWith('/'))
                ? adress.Substring(0, adress.Length - 1)
                : adress;
        }
    }
}
