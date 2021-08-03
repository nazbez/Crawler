using System.Collections.Generic;
using System.Linq;
using Xunit;
using Crawler.Logic.Parsers;

namespace Crawler.Logic.Tests
{
    public class ParserSitemapTests
    {
        private readonly ParserSitemap _parser = new ParserSitemap();

        [Fact]
        public void Parse_InvalidUrl_EmptyList()
        {
            // Act
            var result = _parser.Parse("Test", "Test", Tag.Url);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Parse_ValidUrl_ListOfSitemaps()
        {
            // Act
            string document = "<urlset>" +
               "<url><loc>https://nure.ua/external/blagodiyna-diyalnist</loc></url>" +
               "<url><loc>https://nure.ua/ru/external/blagotvoritelnyiy-fond-inteko</loc></url>" +
               "<url><loc>https://nure.ua/en/external/charitable-foundation-inteco</loc></url>" +
               "</urlset>";

            var result = _parser.Parse(document, "https://nure.ua/external-sitemap.xml", Tag.Url);

            // Assert
            Assert.Equal(new List<string> 
            {
                "https://nure.ua/external/blagodiyna-diyalnist",
                "https://nure.ua/ru/external/blagotvoritelnyiy-fond-inteko",
                "https://nure.ua/en/external/charitable-foundation-inteco"
            }, result);
        }

        [Fact]
        public void Parse_UrlsAreRelative_ListOfAbsoluteUrls()
        {
            // Act
            string document = "<urlset>" +
               "<url><loc>/api/</loc></url>" +
               "<url><loc>/docs/</loc></url>" +
               "</urlset>";
            
            var result = _parser.Parse(document, "https://www.litedb.org/sitemap.xml", Tag.Url);

            // Assert
            Assert.Empty(result.Where(x=>!x.StartsWith("https://www.litedb.org")));
        }

        [Fact]
        public void Parse_UrlsEndWithSlash_RemoveSlashes()
        {
            // Act
            string document = "<urlset>" +
                "<url><loc>/api/</loc></url>" +
                "<url><loc>/docs/</loc></url>" +
                "</urlset>";
           
            var result = _parser.Parse(document, "https://www.litedb.org/sitemap.xml", Tag.Url);

            // Assert
            Assert.Empty(result.Where(x => x.EndsWith("/")));
        }
    }
}
