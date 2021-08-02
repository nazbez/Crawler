using Xunit;
using System.Collections.Generic;
using Crawler.Logic.Parsers;

namespace Crawler.Logic.Tests
{
    public class ParserHtmlTests
    {
        // Arrange
        private readonly ParserHtml _parser = new ParserHtml();

        [Fact]
        public void ParseUrls_InvalidUrl_EmptyList()
        {
            // Act
            var result = _parser.ParseUrls("Test", "Test");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void ParseUrls_ValidUrl_ListOfUrls()
        {
            // Act
            var result = _parser.ParseUrls("https://nazar.com",
                "<a href=\"https://nazar.com/bio\"</a> " +
                "<a href=\"https://nazar.com/info\"</a> " +
                "<a href=\"https://nazar.com/education\"</a>");

            // Assert
            Assert.Equal(new List<string>
            {
                "https://nazar.com/bio",
                "https://nazar.com/info",
                "https://nazar.com/education"
            }, result);
        }

        [Fact]
        public void ParseUrls_IgnoreUrlWithOtherDomain()
        {
            // Act
            var result = _parser.ParseUrls("https://nazar.com", 
                "<a href=\"https://google.com/\"></a> " +
                "<a href=\"https://nazar.com/bio\"</a> " +
                "<a href=\"https://nazar.com/info\"</a> " +
                "<a href=\"https://nazar.com/education\"</a>");

            // Assert
            Assert.Equal(new List<string> 
            { 
                "https://nazar.com/bio", 
                "https://nazar.com/info", 
                "https://nazar.com/education" 
            }, result);
        }

        [Fact]
        public void ParseUrls_HtmlHasRelativeUrls_ListOfAbsoluteUrls()
        {
            // Act
            var result = _parser.ParseUrls("https://nazar.com", 
                "<a href=\"/bio\"</a>" +
                "<a href=\"/info\"</a> " +
                "<a href=\"/education\"</a>");

            // Assert
            Assert.Equal(new List<string> 
            { 
                "https://nazar.com/bio", 
                "https://nazar.com/info", 
                "https://nazar.com/education" 
            }, result);
        }

        [Fact]
        public void ParseUrls_IgnoreUrlsWithAnchor()
        {
            // Act
            var result = _parser.ParseUrls("https://nazar.com", 
                "<a href=\"https://nazar.com/bio#\"</a> " +
                "<a href=\"https://nazar.com/bio\"</a>");

            // Assert
            Assert.Equal(new List<string> { "https://nazar.com/bio" }, result);
        }

        [Fact]
        public void ParseUrls_UrlsEndWithSlash_RemoveSlashes()
        {
            // Act
            var result = _parser.ParseUrls("https://nazar.com", 
                "<a href=\"https://nazar.com/bio/\"</a>" +
                "<a href=\"https://nazar.com/info/\"</a>" +
                "<a href=\"https://nazar.com/education/\"</a>");

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
