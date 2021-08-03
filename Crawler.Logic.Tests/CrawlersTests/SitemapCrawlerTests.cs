using Xunit;
using Moq;
using System.Collections.Generic;
using Crawler.Logic.Parsers;
using System;

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
            _crawler = new SitemapCrawler(_mockParser.Object, _mockDownloader.Object);
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
        public void GetUrls_SitemapContainLinkOnOthersSitemaps_ListWithUrls()
        {
            // Arrange
            _mockDownloader.SetupSequence(x => x.Download(It.IsAny<string>()))
                .Returns("<sitemapindex><sitemap><loc>https://someurl1/sitemap.xml</loc></sitemap></sitemapindex>")
                .Returns("<urlset><url><loc>https://someurl</loc></url></urlset>");

            _mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Tag>()))
                .Returns(new List<string> { "https://someurl1/sitemap.xml" })
                .Returns(new List<string> { "https://someurl" });

            // Act
            var result = _crawler.GetUrls("Test");

            // Assert
            Assert.Equal(new List<string> { "https://someurl" }, result);
        }

        [Fact]
        public void GetUrls_SitemapContainOnlyUrls_ListWithUrls()
        {
            // Arrange
            _mockDownloader.Setup(x => x.Download(It.IsAny<string>()))
                .Returns("<urlset><url><loc>https://someurl</loc></url></urlset>");

            _mockParser.SetupSequence(x => x.Parse(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Tag>()))
                .Returns(Array.Empty<string>)
                .Returns(new List<string> { "https://someurl" });

            // Act
            var result = _crawler.GetUrls("https://someurl/sitemap.xml");

            // Assert
            Assert.Equal(new List<string> { "https://someurl" }, result);
        }
    }
}
