using Xunit;
using System.Collections.Generic;

namespace Crawler.Logic.Tests
{
    public class ParserHtmlTests
    {
        [Fact]
        public void ParseUrls_InvalidParams_EmptyList()
        {
            // Arrange
            ParserHtml parser = new ParserHtml("Test", "Test");

            // Act
            var result = parser.ParseUrls() as List<string>;

            // Assert
            Assert.Equal(new List<string> { }, result);
        }

        [Fact]
        public void ParserUrls_ValidParams_ListOfUrls()
        {
            // Arrange
            ParserHtml parser = new ParserHtml("https://nazar.com", "<a href=\"https://google.com/\"></a>  <a href=\"https://nazar.com/bio\"</a>  <a href=\"https://nazar.com/info\"</a> <a href=\"https://nazar.com/education\"</a>");

            // Act
            var result = parser.ParseUrls() as List<string>;

            // Assert
            Assert.Equal(new List<string> { "https://nazar.com/bio", "https://nazar.com/info", "https://nazar.com/education" }, result);
            Assert.Equal(3, result.Count);
        }
 

    }
}
