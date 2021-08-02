using Xunit;
using System.Collections.Generic;
using Crawler.Logic.Parsers;

namespace Crawler.Logic.Tests
{
    public class ParserHtmlTests
    {
        [Fact]
        public void ParseUrls_InvalidParams_EmptyList()
        {
            // Arrange
            ParserHtml parser = new ParserHtml();

            // Act
            var result = parser.ParseUrls("Test", "Test") as List<string>;

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ParserUrls_ValidParams_ListOfUrls()
        {
            // Arrange
            ParserHtml parser = new ParserHtml();

            // Act
            var result = parser.ParseUrls("https://nazar.com",
                "<a href=\"https://nazar.com/bio\"</a> " +
                "<a href=\"https://nazar.com/info\"</a> " +
                "<a href=\"https://nazar.com/education\"</a>") as List<string>;

            // Assert
            Assert.Equal(new List<string>
            {
                "https://nazar.com/bio",
                "https://nazar.com/info",
                "https://nazar.com/education"
            }, result);
        }

        [Fact]
        public void ParserUrls_IgnoreUrlWithOtherDomain()
        {
            // Arrange
            ParserHtml parser = new ParserHtml();

            // Act
            var result = parser.ParseUrls("https://nazar.com", 
                "<a href=\"https://google.com/\"></a> " +
                "<a href=\"https://nazar.com/bio\"</a> " +
                "<a href=\"https://nazar.com/info\"</a> " +
                "<a href=\"https://nazar.com/education\"</a>") as List<string>;

            // Assert
            Assert.Equal(new List<string> 
            { 
                "https://nazar.com/bio", 
                "https://nazar.com/info", 
                "https://nazar.com/education" 
            }, result);
        }

        [Fact]
        public void ParserUrls_HtmlHasNotAbsoluteUrls_ListOfAbsoluteUrls()
        {
            // Arrange
            ParserHtml parserHtml = new ParserHtml();

            // Act
            var result = parserHtml.ParseUrls("https://nazar.com", 
                "<a href=\"/bio\"</a>" +
                "<a href=\"/info\"</a> " +
                "<a href=\"/education\"</a>") as List<string>;

            // Assert
            Assert.Equal(new List<string> 
            { 
                "https://nazar.com/bio", 
                "https://nazar.com/info", 
                "https://nazar.com/education" 
            }, result);
        }

        [Fact]
        public void ParseUrls_IgnoreUrlsWhichHaveAnchor()
        {
            // Arrange
            ParserHtml parserHtml = new ParserHtml();

            // Act
            var result = parserHtml.ParseUrls("https://nazar.com", 
                "<a href=\"https://nazar.com/bio#\"</a> " +
                "<a href=\"https://nazar.com/bio\"</a>") as List<string>;

            // Assert
            Assert.Equal(new List<string> { "https://nazar.com/bio" }, result);
        }

        [Fact]
        public void ParseUrls_UrlsEndWithSlash_UrlsWithoutSlashesInTheEnd()
        {
            // Arrange
            ParserHtml parserHtml = new ParserHtml();

            // Act
            var result = parserHtml.ParseUrls("https://nazar.com", 
                "<a href=\"https://nazar.com/bio/\"</a>" +
                "<a href=\"https://nazar.com/info/\"</a>" +
                "<a href=\"https://nazar.com/education/\"</a>") as List<string>;

            // Assert
            Assert.Equal(new List<string> 
            { 
                "https://nazar.com/bio", 
                "https://nazar.com/info", 
                "https://nazar.com/education" 
            }, result);
        }
    }
}
