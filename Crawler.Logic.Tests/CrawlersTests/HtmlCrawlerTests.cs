using Xunit;
using Moq;
using System.Collections.Generic;
using Crawler.Logic.Parsers;
using System;


namespace Crawler.Logic.Tests
{
    public class HtmlCrawlerTests
    {
        private readonly Mock<ParserHtml> _mockParser;
        private readonly Mock<Downloader> _mockDownloader;
        private readonly HtmlCrawler _crawler;

        public HtmlCrawlerTests()
        {
            _mockParser = new Mock<ParserHtml>();
            _mockDownloader = new Mock<Downloader>();
            _crawler = new HtmlCrawler(_mockParser.Object, _mockDownloader.Object);
        }

        [Fact]
        public void GetUrls_InvalidUrl_EmptyList()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");

            // Act
            var result = _crawler.GetUrls("Test");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetUrls_HtmlDoesntContainOtherUrls_ListWithOneUrl()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("<!doctype html><html></html>");
            _mockParser.Setup(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>())).Returns(Array.Empty<string>);
                
            // Act
            var result = _crawler.GetUrls("https://someurl.com") as List<string>;

            // Assert
            Assert.Equal(new List<string> { "https://someurl.com" }, result);
        }

        [Fact]
        public void GetUrls_HtmlContainsOtherUrls_ListOfUrls()
        {
            // Arrange
            _mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("<!doctype html><html><a href = \"https://someurl.com/2\"></a></html>")
                .Returns("<!doctype html><html><a href = \"https://someurl.com/3\"></a></html>")
                .Returns("<!doctype html><html></html>");

            _mockParser.SetupSequence(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "https://someurl.com/2" })
                .Returns(new List<string> { "https://someurl.com/3" })
                .Returns(new List<string> { });

            // Act
            var result = _crawler.GetUrls("https://someurl.com") as List<string>;

            // Assert
            Assert.Equal(new List<string> { "https://someurl.com", "https://someurl.com/2", "https://someurl.com/3" }, result);
        }
    }
}
