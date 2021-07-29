using Xunit;
using Moq;
using System.Collections.Generic;

namespace Crawler.Logic.Tests
{
    public class CrawlerServiceTests
    {
        private readonly Mock<HtmlCrawler> _mockHtmlCrawler;
        private readonly Mock<SitemapCrawler> _mockSitemapCrawler;
        private readonly Mock<CrawlerLinksHandler> _mockHandler;
        private CrawlerService service;

        public CrawlerServiceTests()
        {
            _mockHandler = new Mock<CrawlerLinksHandler>(new Timer());
            _mockHtmlCrawler = new Mock<HtmlCrawler>(new ParserHtml(), new Downloader());
            _mockSitemapCrawler = new Mock<SitemapCrawler>(new ParserSitemap(), new Downloader());
            service = new CrawlerService(_mockHtmlCrawler.Object, _mockSitemapCrawler.Object, _mockHandler.Object);
        }

        [Fact] 
        public void Crawl_InvalidParam_EmptyList()
        {
            // Arrange
            _mockHtmlCrawler.Setup(x => x.GetUrls(It.IsAny<string>()))
                .Returns(new List<string> { });
            _mockSitemapCrawler.Setup(x => x.GetUrls(It.IsAny<string>()))
                .Returns(new List<string> { });
            _mockHandler.Setup(x => x.GetResultOfCrawling(new List<string> { }, new List<string> { }))
                .Returns(new List<CrawlingResult>{ });

            // Act
            var result = service.Crawl("Test");

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void Crawl_ValidParam_NonEmptyList()
        {
            // Arrange
            var element = new CrawlingResult { Url = "url1", Time = 100, IsInHtml = true, IsInSitemap = true };
            _mockHtmlCrawler.Setup(x => x.GetUrls(It.IsAny<string>()))
                .Returns(new List<string> { "url1" });
            _mockSitemapCrawler.Setup(x => x.GetUrls(It.IsAny<string>()))
                .Returns(new List<string> { "url1" });
            _mockHandler.Setup(x => x.GetResultOfCrawling(new List<string> { "url1" }, new List<string> { "url1" }))
               .Returns(new List<CrawlingResult> { element });
            
            // Act
            var result = service.Crawl("Test");

            // Assert
            Assert.NotEmpty(result);
            Assert.Contains(element, result);
        }
    }
}
