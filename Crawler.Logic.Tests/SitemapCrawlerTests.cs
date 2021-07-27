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
        public void GetUrls_SitemapContainLinkOnOthersSitemap_ListWithUrls()
        {
            // Arrange
            mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("Document with sitemaps")
                .Returns("First sitemap document")
                .Returns("Second sitemap document");
            mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "URL1/sitemap.xml", "URL2/sitemap.xml"})
                .Returns(new List<string> { "Url1 from first sitemap", "Url2 from first sitemap"})
                .Returns(new List<string> { "Url1 from second sitemap"});

            // Act
            crawler = new SitemapCrawler("Test", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> 
            { 
                "Url1 from first sitemap", 
                "Url2 from first sitemap", 
                "Url1 from second sitemap" 
            }, result);
            mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Exactly(3));
            mockParser.Verify(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public void GetUrls_SitemapContainOnlyUrls_ListWithUrls()
        {
            // Arrange
            mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Document with urls");
            mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { })
                .Returns(new List<string> { "Url1", "Url2" });

            // Act
            crawler = new SitemapCrawler("Test", mockParser.Object, mockDownloader.Object);
            var result = crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> { "Url1", "Url2" }, result);
            mockParser.Verify(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Once);
        }
    }
}
