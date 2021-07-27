using Xunit;
using Moq;
using System.Collections.Generic;

namespace Crawler.Logic.Tests
{
    public class HtmlCrawlerTests
    {
        private readonly Mock<ParserHtml> _mockParser;
        private readonly Mock<Downloader> _mockDownloader;
        private HtmlCrawler crawler;

        public HtmlCrawlerTests()
        {
            _mockParser = new Mock<ParserHtml>();
            _mockDownloader = new Mock<Downloader>();
        }

        [Fact]
        public void GetUrls_NotUrl_EmptyList()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");

            // Act
            crawler = new HtmlCrawler("Not url", _mockParser.Object, _mockDownloader.Object);
            var result = crawler.GetUrls() as List<string>;

            // Assert
            Assert.Equal(new List<string> { }, result);
        }

        [Fact]
        public void GetUrls_NotUrl_CountOfCallsOfMethods()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");

            // Act
            crawler = new HtmlCrawler("Not url", _mockParser.Object, _mockDownloader.Object);
            var result = crawler.GetUrls() as List<string>;

            // Assert
            _mockParser.Verify(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetUrls_UrlDoesntContainOtherUrls_ListWithOneUrl()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Document");
            _mockParser.Setup(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string> { });
                
            // Act
            crawler = new HtmlCrawler("Url1", _mockParser.Object, _mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> { "Url1" }, result);
        }

        [Fact]
        public void GetUrls_UrlDoesntContainOtherUrls_CountOfCallsOfMethods()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Document");
            _mockParser.Setup(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string> { });

            // Act
            crawler = new HtmlCrawler("Url1", _mockParser.Object, _mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            _mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Once);
            _mockParser.Verify(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetUrls_UrlContainsOtherUrls_ListOfUrls()
        {
            // Arrange
            _mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("First document")
                .Returns("Second document")
                .Returns("Third document");
            _mockParser.SetupSequence(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "Url2" })
                .Returns(new List<string> { "Url3" })
                .Returns(new List<string> { "Url1", "Url2", "Url3"});

            // Act
            crawler = new HtmlCrawler("Url1", _mockParser.Object, _mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> { "Url1", "Url2", "Url3" }, result);
        }

        [Fact]
        public void GetUrls_UrlContainsOtherUrls_CountOfCallsOfMethods()
        {
            // Arrange
            _mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("First document")
                .Returns("Second document")
                .Returns("Third document");
            _mockParser.SetupSequence(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "Url2" })
                .Returns(new List<string> { "Url3" })
                .Returns(new List<string> { "Url1", "Url2", "Url3" });

            // Act
            crawler = new HtmlCrawler("Url1", _mockParser.Object, _mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            _mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Exactly(3));
            _mockParser.Verify(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }
    }
}
