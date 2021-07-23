using System.Collections.Generic;
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
            var result = parser.Parse("Test", "Test") as List<string>;

            // Assert
            Assert.Equal(new List<string> { }, result);
        }

        [Fact]
        public void Parse_IndexSitemap_ListOfSitemaps()
        {
            // Arrange
            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse("https://nure.ua/sitemap.xml", "sitemap") as List<string>;

            // Assert
            Assert.Equal(48, result.Count);
        }

        [Fact]
        public void Parse_Sitemap_ListOfUrls()
        {
            // Arrange
            ParserSitemap parser = new ParserSitemap();

            // Act
            var result = parser.Parse("https://nure.ua/post-sitemap1.xml", "url") as List<string>;

            // Assert
            Assert.Equal(1000, result.Count);
        }
    }
}
