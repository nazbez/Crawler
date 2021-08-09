using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace Crawler.Logic.Parsers
{
    public enum Tag
    {
        Sitemap,
        Url
    }

    public class ParserSitemap
    { 
        public virtual IEnumerable<string> Parse(string doc, string url, Tag tag)
        {
            XmlDocument document = new XmlDocument();

            doc = TransformToValidDocument(doc);

            try
            {
                document.LoadXml(doc);
            }
            catch (XmlException)
            {
                return new List<string>(); 
            }

            string foundTag = tag == Tag.Sitemap ? "sitemap" : "url";

            List<string> result = new List<string>();
            var xmlSitemapList = document.GetElementsByTagName(foundTag);
            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] == null)
                {
                    continue;
                }

                string adress = GetAbsoluteString(url, node["loc"].InnerText);

                adress = СonvertToUnifiedForm(adress);

                if (!string.IsNullOrEmpty(adress) && !result.Contains(adress))
                {
                    result.Add(adress);
                }
            }

            return result;
        }
        
        private string GetAbsoluteString(string baseUrl, string url)
        {
            try
            {
                Uri uri = new Uri(new Uri(baseUrl), url);

                string adress = uri.ToString();

                return adress;
            }
            catch (UriFormatException)
            {
                return string.Empty;
            }
        }

        private string СonvertToUnifiedForm(string url)
        {
            return url.EndsWith('/') ? url.Remove(url.Length - 1, 1) : url;
        }

        private string TransformToValidDocument(string document)
        {
            return document.StartsWith("<") ?
                string.Join("", document.SkipWhile(x => x != '<')) :
                document;
        }
    }
}
