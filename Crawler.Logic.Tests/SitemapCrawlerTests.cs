using Xunit;
using Moq;
using System.Collections.Generic;

namespace Crawler.Logic.Tests
{
    public class SitemapCrawlerTests
    {
        private Mock<ParserSitemap> mockParser;
        private Mock<Downloader> mockDownloader;
        private SitemapCrawler crawler;

        public SitemapCrawlerTests()
        {
            mockParser = new Mock<ParserSitemap>();
            mockDownloader = new Mock<Downloader>();
        }

        [Fact]
        public void GetUrls_InvalidParams_EmptyList()
        {
            // Arrange
            mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");
            
            // Act
            crawler = new SitemapCrawler("Test", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            mockParser.Verify(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            Assert.Equal(new List<string> { }, result);           
        }

        [Fact]
        public void GetUrls_CorrectParams_ListWithUrls()
        {
            var testList = new List<string>
            {
                "https://www.litedb.org/api",
                "https://www.litedb.org/docs",
                "https://www.litedb.org/docs/getting-started",
            };

           
            // Arrange
            mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Test");
            mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { })
                .Returns(testList);

            // Act
            crawler = new SitemapCrawler("Test", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            Assert.Equal(testList, result);
        }

    }
}
