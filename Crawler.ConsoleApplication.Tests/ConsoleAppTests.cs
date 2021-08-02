using Xunit;
using Moq;
using Crawler.Logic;
using Crawler.Logic.Parsers;
using Crawler.Logic.Models;
using System.Collections.Generic;
using System;

namespace Crawler.ConsoleApplication.Tests
{
    public class ConsoleAppTests
    {
        private readonly Mock<Printer> _mockPrinter;
        private readonly Mock<CrawlerService> _mockService;
        private ConsoleApp app;

        public ConsoleAppTests()
        {
            _mockPrinter = new Mock<Printer>();
            _mockService = new Mock<CrawlerService>(
                new HtmlCrawler(new ParserHtml(), new Downloader()),
                new SitemapCrawler(new ParserSitemap(), new Downloader()),
                new CrawlerLinksHandler(new Timer()),
                new Validator());
            app = new ConsoleApp(_mockPrinter.Object, _mockService.Object);
        }

        [Fact]
        public void Interract_InvalidParam_EndTheProgram()
        {
            // Arrange
            _mockService.Setup(x => x.Crawl(It.IsAny<string>())).Throws(new ArgumentException());

            // Act
            app.Interract();

            // Assert
            _mockService.Verify(x => x.GetTimeOfResponses(It.IsAny<IEnumerable<CrawlingResult>>()), Times.Never);
            _mockPrinter.Verify(x => x.PrintUniqueLinks(It.IsAny<IEnumerable<string>>(), It.IsAny<string>()), Times.Never);
            _mockPrinter.Verify(x => x.PrintTimeOfResponse(It.IsAny<IEnumerable<TimeOfResponseResult>>()), Times.Never);
            _mockPrinter.Verify(x => x.PrintCountOfLinks(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}
