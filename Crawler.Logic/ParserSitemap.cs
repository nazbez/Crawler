using System;
using System.Collections.Generic;
using System.Xml;

namespace Crawler.Logic
{
    public class ParserSitemap
    { 
        public virtual IEnumerable<string> Parse(string doc, string url, string tag)
        {
            XmlDocument document = new XmlDocument();
            List<string> result = new List<string> { };

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

                    result.Add(adress);
                }
            }

            return result;
        }
        
        private string GetAbsoluteString(string baseUrl, string url)
        {
            Uri uri = new Uri(new Uri(baseUrl), url);

            string adress = uri.ToString();

            return adress;
        }

        private string СonvertToUnifiedForm(string url)
        {
            return url.EndsWith('/') ? url.Remove(url.Length - 1, 1) : url;
        }
    }
}
