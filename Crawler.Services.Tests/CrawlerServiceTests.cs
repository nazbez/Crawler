using System;
using Moq;
using Xunit;
using Crawler.Logic;
using Crawler.Logic.Parsers;
using System.Data;
using Crawler.DbModels;


namespace Crawler.Services.Tests
{
    public class CrawlerServiceTests
    {
        private readonly Mock<CrawlerHandler> _mockCrawlerHandler;
        private readonly Mock<DbHandler> _mockDbHandler;
        private readonly CrawlerService _crawlerService;

        public CrawlerServiceTests()
        {
            _mockCrawlerHandler = new Mock<CrawlerHandler>(new HtmlCrawler(new ParserHtml(), new Downloader()),
                new SitemapCrawler(new ParserSitemap(), new Downloader()),
                new Validator(), 
                new Timer());

            _mockDbHandler = new Mock<DbHandler>(new Mock<IRepository<Test>>().Object, (new Mock<IRepository<TestResult>>().Object));

            _crawlerService = new CrawlerService(_mockCrawlerHandler.Object, _mockDbHandler.Object);
        }

        public void InterractAsync_ShouldReturnAddedItemId()
        {
            // Arrange
            _mockCrawlerHandler.Setup(x => x.GetLinksFromHtmlAndSitemap("https://url")).Returns()
            // Act

            // Assert
        }

    }
}
