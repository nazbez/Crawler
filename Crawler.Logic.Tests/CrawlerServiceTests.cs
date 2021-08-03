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
        private readonly Mock<Timer> _mockTimer;
        private readonly Mock<Validator> _mockValidator;
        private readonly CrawlerService _service;

        public CrawlerServiceTests()
        {
            _mockHtmlCrawler = new Mock<HtmlCrawler>(new ParserHtml(), new Downloader());
            _mockSitemapCrawler = new Mock<SitemapCrawler>(new ParserSitemap(), new Downloader());
            _mockTimer = new Mock<Timer>();
            _mockValidator = new Mock<Validator>();
            _service = new CrawlerService(_mockHtmlCrawler.Object, _mockSitemapCrawler.Object, _mockValidator.Object, _mockTimer.Object);
        }

        [Fact]
        public void Crawl_InvalidUrl_ThrowException()
        {
            // Arrange
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns("Invalid input!");

            // Act
            var exception = Assert.Throws(new ArgumentException().GetType(), () => _service.GetLinksFromHtmlAndSitemap("Test"));

            // Assert
            Assert.Equal("Invalid input!", exception.Message);
        }

        [Fact]
        public void Crawl_ValidUrl_ListOfModels()
        {
            // Arrange
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns("");
            _mockHtmlCrawler.Setup(x => x.GetUrls(It.IsAny<string>())).Returns(new List<string> { "https://someurl" });
            _mockSitemapCrawler.Setup(x => x.GetUrls(It.IsAny<string>())).Returns(new List<string> { "https://someurl", "https://someurl/1" });

            // Act
            var result = _service.GetLinksFromHtmlAndSitemap("https://someurl");

            // Assert
            Assert.Equal(new List<CrawlingResult>
            {
                new CrawlingResult {Url = "https://someurl", IsInHtml = true, IsInSitemap = true },
                new CrawlingResult {Url = "https://someurl/1", IsInHtml = false, IsInSitemap = true },
            }, result);
        }

        [Fact]
        public void Crawl_ValidUrl_AllLinksIsUniqueInResult() 
        {
            // Arrange
            _mockValidator.Setup(x => x.IsValid(It.IsAny<string>())).Returns("");
            _mockHtmlCrawler.Setup(x => x.GetUrls(It.IsAny<string>())).Returns(new List<string> { "https://someurl" });
            _mockSitemapCrawler.Setup(x => x.GetUrls(It.IsAny<string>())).Returns(new List<string> { "https://someurl" });

            // Act
            var result = _service.GetLinksFromHtmlAndSitemap("https://someurl");

            // Assert
            Assert.Equal(result.Distinct(), result);
        }

        [Fact]
        public void GetTimeOfResponses_CollectionsOfStrings_CollectionOfModels()
        {
            // Arrange
            _mockTimer.Setup(x => x.CheckTimeResponse(It.IsAny<string>())).Returns(new TimeOfResponseResult { Url = "https://someurl", Time = 100, ErrorMsg = "" });

            var links = new List<CrawlingResult> { new CrawlingResult { Url = "https://someurl", IsInHtml = true, IsInSitemap = false } };

            // Act
            var result = _service.GetResponseTime(links);

            // Assert
            Assert.IsType(new TimeOfResponseResult().GetType(), result.First());
        }
    }
}
