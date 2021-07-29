using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Crawler.Logic.Tests
{
    public class ParserSitemapTests
    {
        [Fact]
        public void Parse_InvalidParams_EmptyList()
        {
            // Arrange
            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse("Test", "Test", "Test") as List<string>;

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Parse_Sitemap_ListOfSitemaps()
        {
            // Arrange
            string document = "<urlset>" +
               "<url><loc>https://nure.ua/external/blagodiyna-diyalnist</loc></url>" +
               "<url><loc>https://nure.ua/ru/external/blagotvoritelnyiy-fond-inteko</loc></url>" +
               "<url><loc>https://nure.ua/en/external/charitable-foundation-inteco</loc></url>" +
               "</urlset>";

            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse(document, "https://nure.ua/external-sitemap.xml", "url") as List<string>;

            // Assert
            Assert.Equal(new List<string> 
            {
                "https://nure.ua/external/blagodiyna-diyalnist",
                "https://nure.ua/ru/external/blagotvoritelnyiy-fond-inteko",
                "https://nure.ua/en/external/charitable-foundation-inteco"
            }, result);
        }

        [Fact]
        public void Parse_IndexedSitemap_ListOfUrls()
        {
            // Arrange
            string document = "<sitemapindex>" +
               "<sitemap><loc>https://translate.google.com/mainsitemap.xml</loc></sitemap>" +
               "<sitemap><loc>https://translate.google.com/aboutsitemap.xml</loc></sitemap>" +
               "</sitemapindex>";

            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse(document, "https://translate.google.com/sitemap.xml", "sitemap") as List<string>;

            // Assert
            Assert.Equal(new List<string> 
            { 
                "https://translate.google.com/mainsitemap.xml", 
                "https://translate.google.com/aboutsitemap.xml" 
            }, result);
        }

        [Fact]
        public void Parse_UrlsAreNotAbsolute_ListOfAbsoluteUrls()
        {
            // Arrange
            string document = "<urlset>" +
               "<url><loc>/api/</loc></url>" +
               "<url><loc>/docs/</loc></url>" +
               "</urlset>";

            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse(document, "https://www.litedb.org/sitemap.xml", "sitemap") as List<string>;

            // Assert
            Assert.Empty(result.Where(x=>!x.StartsWith("https://www.litedb.org")));
        }

        [Fact]
        public void Parse_UrlsEndWithSlash_UrlsWithoutSlashesInTheEnd()
        {

            // Arrange
            string document = "<urlset>" +
                "<url><loc>/api/</loc></url>" +
                "<url><loc>/docs/</loc></url>" +
                "</urlset>";

            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse(document, "https://www.litedb.org/sitemap.xml", "sitemap") as List<string>;

            // Assert
            Assert.Empty(result.Where(x => !x.EndsWith("/")));
        }
    }
}
