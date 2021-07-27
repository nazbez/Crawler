using Xunit;
using Moq;
using System.Collections.Generic;

namespace Crawler.Logic.Tests
{
    public class SitemapCrawlerTests
    {
        private readonly Mock<ParserSitemap> _mockParser;
        private readonly Mock<Downloader> _mockDownloader;
        private readonly SitemapCrawler _crawler;

        public SitemapCrawlerTests()
        {
            _mockParser = new Mock<ParserSitemap>();
            _mockDownloader = new Mock<Downloader>();
            _crawler = new SitemapCrawler("Test", _mockParser.Object, _mockDownloader.Object);
        }

        [Fact]
        public void GetUrls_InvalidParams_EmptyList()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");
            
            // Act
            var result = _crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> { }, result);           
        }

        [Fact]
        public void GetUrls_InvalidParams_CountOfCallsOfMethods()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("");

            // Act
            var result = _crawler.GetUrls();

            // Assert
            _mockParser.Verify(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
            _mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void GetUrls_SitemapContainLinkOnOthersSitemap_ListWithUrls()
        {
            // Arrange
            _mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("Document with sitemaps")
                .Returns("First sitemap document")
                .Returns("Second sitemap document");
            _mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "URL1/sitemap.xml", "URL2/sitemap.xml"})
                .Returns(new List<string> { "Url1 from first sitemap", "Url2 from first sitemap"})
                .Returns(new List<string> { "Url1 from second sitemap"});

            // Act
            var result = _crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> 
            { 
                "Url1 from first sitemap", 
                "Url2 from first sitemap", 
                "Url1 from second sitemap" 
            }, result);
        }

        [Fact]
        public void GetUrls_SitemapContainLinkOnOthersSitemaps_CountOfCallsOfMethods()
        {
            // Arrange
            _mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("Document with sitemaps")
                .Returns("First sitemap document")
                .Returns("Second sitemap document");
            _mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { "URL1/sitemap.xml", "URL2/sitemap.xml" })
                .Returns(new List<string> { "Url1 from first sitemap", "Url2 from first sitemap" })
                .Returns(new List<string> { "Url1 from second sitemap" });

            // Act
            var result = _crawler.GetUrls();

            // Assert
            _mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Exactly(3));
            _mockParser.Verify(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(3));
        }

        [Fact]
        public void GetUrls_SitemapContainOnlyUrls_ListWithUrls()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Document with urls");
            _mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { })
                .Returns(new List<string> { "Url1", "Url2" });

            // Act
            var result = _crawler.GetUrls();

            // Assert
            Assert.Equal(new List<string> { "Url1", "Url2" }, result);
        }

        [Fact]
        public void GetUrls_SitemapContainOnlyUrls_CountOfCallsOfMethods()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>())).Returns("Document with urls");
            _mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new List<string> { })
                .Returns(new List<string> { "Url1", "Url2" });

            // Act
            var result = _crawler.GetUrls();

            // Assert
            _mockParser.Verify(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            _mockDownloader.Verify(x => x.Download(It.IsAny<string>()), Times.Once);
        }
    }
}
