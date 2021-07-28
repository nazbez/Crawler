using Xunit;
using Moq;
using Crawler.Logic;
using System.Collections.Generic;

namespace Crawler.ConsoleApplication.Tests
{
    public class ConsoleAppTests
    {
        private readonly Mock<ConsoleImitator> _mockConsole;
        private readonly Mock<CrawlerLinksHandler> _mockHandler;
        private readonly Mock<HtmlCrawler> _mockHtmlCrawler;
        private readonly Mock<SitemapCrawler> _mockSitemapCrawler;
        private readonly ConsoleApp app;

        public ConsoleAppTests()
        {
            _mockConsole = new Mock<ConsoleImitator>();

            _mockHandler = new Mock<CrawlerLinksHandler>(new Timer());

            _mockHtmlCrawler = new Mock<HtmlCrawler>(new ParserHtml(), new Downloader());

            _mockSitemapCrawler = new Mock<SitemapCrawler>(new ParserSitemap(), new Downloader());

            app = new ConsoleApp(_mockConsole.Object, _mockHandler.Object, 
                _mockHtmlCrawler.Object, _mockSitemapCrawler.Object);
        }

        [Fact]
        public void Interract_InputNotUrl_OutputMessageAboutIt()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("Test");

            // Act
            app.Interract();

            // Assert
            _mockConsole.Verify(x => x.WriteLine("Your input is not url!"), Times.Once);
            _mockHandler.Verify(x => x.GetResultOfCrawling(It.IsAny<List<string>>(), It.IsAny<List<string>>()), Times.Never);
            _mockHtmlCrawler.Verify(x => x.GetUrls(It.IsAny<string>()), Times.Never);
            _mockSitemapCrawler.Verify(x => x.GetUrls(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public void InterractInput_InputValid_OutputMessages()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("https://url");

            // Act
            app.Interract();

            // Assert
            _mockConsole.Verify(x => x.WriteLine("Links which were founded only by html crawler"), Times.Once);
            _mockConsole.Verify(x => x.WriteLine("Links which were founded only by sitemap crawler"), Times.Once);
            _mockConsole.Verify(x => x.WriteLine("\nResult of crawling: "), Times.Once);
            _mockConsole.Verify(x => x.WriteLine("\nCount of links founded by html crawler: 0"), Times.Once);
            _mockConsole.Verify(x => x.WriteLine("\nCount of links founded by sitemap crawler: 0"), Times.Once);
        }

        [Fact]
        public void Interract_SitemapAndHtmlDocHaveTheSameLinks_OutputMessageAboutIt()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("https://url");
            _mockHtmlCrawler.Setup(x => x.GetUrls("https://url")).Returns(new List<string> { "url"});
            _mockSitemapCrawler.Setup(x => x.GetUrls("https://url/sitemap.xml")).Returns(new List<string> { "url" });

            // Act
            app.Interract();

            // Arrange
            _mockConsole.Verify(x => x.WriteLine("There are no unique links!"), Times.Exactly(2));
        }

        [Fact]
        public void Interract_SitemapAndHtmlDocHaveDifferentLinks_OutputLinks()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("https://url");
            _mockHtmlCrawler.Setup(x => x.GetUrls("https://url")).Returns(new List<string> { "url1" });
            _mockSitemapCrawler.Setup(x => x.GetUrls("https://url/sitemap.xml")).Returns(new List<string> { "url2" });

            // Act
            app.Interract();

            // Assert
            _mockConsole.Verify(x => x.WriteLine("1) url1"), Times.Once);
            _mockConsole.Verify(x => x.WriteLine("1) url2"), Times.Once);
        }

        [Fact]
        public void Interract_AfterMergingResultListIsEmpty_OutputMessageAboutIt()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("https://url");
            _mockHandler.Setup(x => x.GetResultOfCrawling(new List<string> { }, new List<string> { }))
                .Returns(new List<CrawlingResult> { });

            // Act
            app.Interract();

            // Assert
            _mockConsole.Verify(x => x.WriteLine("There are no links!"), Times.Once);
        }

        [Fact]
        public void Interract_AfterMergingResultListIsNotEmpty_OutputElemsOfList()
        {
            // Arrange
            _mockConsole.Setup(x => x.ReadLine()).Returns("https://url");

            _mockHtmlCrawler.Setup(x => x.GetUrls("https://url"))
                .Returns(new List<string> { "url1" });
     
            _mockSitemapCrawler.Setup(x => x.GetUrls("https://url/sitemap.xml"))
                .Returns(new List<string> { });

            _mockHandler.Setup(x => x.GetResultOfCrawling(new List<string> { "url1" }, new List<string> {  }))
                .Returns(new List<CrawlingResult> { new CrawlingResult("url1", 100, true, false )});

            // Act
            app.Interract();

            // Assert
            _mockConsole.Verify(x => x.WriteLine("1) url: url1;  time of response: 100"), Times.Once);
        }
    }
}
