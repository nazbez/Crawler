using Xunit;
using Moq;
using Crawler.Logic.Models;
using Crawler.Logic.Parsers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Crawler.Logic.Tests
{
    public class CrawlerServiceTests
    {
        private readonly Mock<HtmlCrawler> _mockHtmlCrawler;
        private readonly Mock<SitemapCrawler> _mockSitemapCrawler;
        private readonly Mock<CrawlerLinksHandler> _mockHandler;
        private readonly Mock<Validator> _mockValidator;
        private CrawlerService service;

        public CrawlerServiceTests()
        {
            _mockHtmlCrawler = new Mock<HtmlCrawler>(new ParserHtml(), new Downloader());
            _mockSitemapCrawler = new Mock<SitemapCrawler>(new ParserSitemap(), new Downloader());
            _mockHandler = new Mock<CrawlerLinksHandler>(new Timer());
            _mockValidator = new Mock<Validator>();
            service = new CrawlerService(_mockHtmlCrawler.Object, _mockSitemapCrawler.Object, _mockHandler.Object, _mockValidator.Object);
        }

        [Fact]
        public void Crawl_InvalidParam_ThrowException()
        {
            // Arrange
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(false);

            // Act
            var exception = Assert.Throws(new ArgumentException().GetType(), () => service.Crawl("Test"));

            // Assert
            Assert.Equal("\nError! Invalid input!\n", exception.Message);
        }

        [Fact]
        public void Crawl_ValidParam_ListOfModels()
        {
            // Arrange
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns(true);
            _mockHtmlCrawler.Setup(x => x.GetUrls(It.IsAny<string>())).Returns(new List<string> { "url1" });
            _mockSitemapCrawler.Setup(x => x.GetUrls(It.IsAny<string>())).Returns(new List<string> { "url2" });

            // Act
            var result = service.Crawl("Test");

            // Assert
            Assert.Equal(new List<CrawlingResult>
            {
                new CrawlingResult {Url = "url1", IsInHtml = true, IsInSitemap = false },
                new CrawlingResult {Url = "url2", IsInHtml = false, IsInSitemap = true },
            }, result);
        }

        [Fact]
        public void GetTimeOfResponses_CollectionsOfStrings_CollectionOfModels()
        {
            // Arrange
            _mockHandler.Setup(x => x.GetResultOfCrawling(It.IsAny<IEnumerable<string>>()))
                .Returns(new List<TimeOfResponseResult>()
                {
                    new TimeOfResponseResult { Url = "URL", Time = 100 } 
                });

            // Act
            var result = service.GetTimeOfResponses(new List<CrawlingResult> { });

            // Assert
            Assert.IsType(new TimeOfResponseResult().GetType(), result.First());
        }
    }
}
