using Xunit;
using Moq;
using Crawler.Logic;
using System.Collections.Generic;

namespace Crawler.ConsoleApplication.Tests
{
    public class ConsoleAppTests
    {
        private readonly Mock<ConsoleImitator> _mockConsole;
        private readonly Mock<Printer> _mockPrinter;
        private readonly Mock<CrawlerService> _mockService;
        private ConsoleApp app;

        public ConsoleAppTests()
        {
            _mockConsole = new Mock<ConsoleImitator>();
            _mockPrinter = new Mock<Printer>();
            _mockService = new Mock<CrawlerService>(new HtmlCrawler(new ParserHtml(), new Downloader()), 
                new SitemapCrawler(new ParserSitemap(), new Downloader()), 
                new CrawlerLinksHandler(new Timer()));
            app = new ConsoleApp(_mockConsole.Object, _mockService.Object, _mockPrinter.Object);
        }

        [Fact]
        public void Interract_NotUrl_MessageAboutIt()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("Test");

            // Act
            app.Interract();

            // Assert
            _mockConsole.Verify(x => x.WriteLine("\nError! Invalid input\n"), Times.Once);
        }

        [Fact]
        public void Interract_NotUrl_ProgramEnd()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("Test");

            // Act
            app.Interract();

            // Assert
            _mockService.Verify(x => x.Crawl(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void Interract_Url_ProgramCallAllMethods()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("https://nazar.com");
            _mockService.Setup(x => x.Crawl("https://nazar.com")).Returns(new List<CrawlingResult> { });

            // Act
            app.Interract();

            // Assert
            _mockPrinter.Verify(x => x.PrintDifference(It.IsAny<IEnumerable<CrawlingResult>>(), It.IsAny<string>()), Times.Exactly(2));
            _mockPrinter.Verify(x => x.PrintTimeOfResponse(It.IsAny<IEnumerable<CrawlingResult>>()), Times.Once);
            _mockPrinter.Verify(x => x.PrintCountOfLinks(It.IsAny<List<CrawlingResult>>(), It.IsAny<string>()), Times.Exactly(2));
        }
    }
}
