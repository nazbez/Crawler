using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;

namespace Crawler.Logic
{
    public class ParserSitemap
    { 
        public virtual IEnumerable<string> Parse(string doc, string url, string tag)
        {
            XmlDocument document = new XmlDocument();
            List<string> result = new List<string> { };

            doc = TransformToValidDocument(doc);

            try
            {
                document.LoadXml(doc);
            }
            catch (XmlException)
            {
                return result;
            }

            var xmlSitemapList = document.GetElementsByTagName(tag);

            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] != null)
                {
                    string adress = GetAbsoluteString(url, node["loc"].InnerText);

                    adress = СonvertToUnifiedForm(adress);

                    if (!string.IsNullOrEmpty(adress))
                    {
                        result.Add(adress);
                    }
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
