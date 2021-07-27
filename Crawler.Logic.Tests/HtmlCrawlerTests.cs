using Xunit;
using Moq;
using System.Collections.Generic;

namespace Crawler.Logic.Tests
{
    public class HtmlCrawlerTests
    {
        private Mock<ParserHtml> mockParser;
        private Mock<Downloader> mockDownloader;
        private HtmlCrawler crawler;

        public HtmlCrawlerTests()
        {
            mockParser = new Mock<ParserHtml>();
            mockDownloader = new Mock<Downloader>();
        }

        [Fact]
        public void GetUrls_NotUrl_EmptyList()
        {
            // Arrange
            mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");

            // Act
            crawler = new HtmlCrawler("Not url", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls() as List<string>;

            // Assert
            mockParser.Verify(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(new List<string> { }, result);
        }

        [Fact]
        public void GetUrls_UrlDoesntContainOtherUrls_ListWithOneUrl()
        {
            // Arrange
            mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Document");
            mockParser.Setup(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>())).Returns(new List<string> { });
                

            // Act
            crawler = new HtmlCrawler("Url1", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Once);
            mockParser.Verify(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal(new List<string> { "Url1" }, result);
        }

        [Fact]
        public void GetUrls_UrlContainsOtherUrls_ListOfUrls()
        {
            // Arrange
            mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("First document")
                .Returns("Second document")
                .Returns("Third document");
            mockParser.SetupSequence(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "Url1", "Url2" })
                .Returns(new List<string> { "Url3" })
                .Returns(new List<string> { "Url1", "Url2", "Url3"});


            // Act
            crawler = new HtmlCrawler("Url1", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Exactly(3));
            mockParser.Verify(x => x.ParseUrls(It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
            Assert.Equal(new List<string> { "Url1", "Url2", "Url3" }, result);
        }
    }
}
