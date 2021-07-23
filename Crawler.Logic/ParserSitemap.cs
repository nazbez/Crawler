using System;
using System.Collections.Generic;
using System.Xml;

namespace Crawler.Logic
{
    public class ParserSitemap
    { 
        public IEnumerable<string> Parse(string url, string tag)
        {
            XmlDocument document = new XmlDocument();
            List<string> result = new List<string> { };

            try
            {
                document.Load(url);
            }
            catch (Exception)
            {
                return result;
            }
   
            var xmlSitemapList = document.GetElementsByTagName(tag);

            foreach (XmlNode node in xmlSitemapList)
            {
                if (node["loc"] != null)
                {
                    Uri uri = new Uri(new Uri(url), node["loc"].InnerText);

                    string adress = uri.ToString();

                    adress = adress.EndsWith('/') ?
                        adress.Remove(adress.Length-1, 1)
                        : adress;

                    result.Add(adress);
                }
            }

            return result;
        }      
    }
}
