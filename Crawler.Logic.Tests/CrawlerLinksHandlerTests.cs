using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Crawler.Logic.Models;

namespace Crawler.Logic.Tests
{
    public class CrawlerLinksHandlerTests
    {
        private readonly Mock<Timer> _mockTimer;
        private readonly CrawlerLinksHandler _handler;

        public CrawlerLinksHandlerTests()
        {
            _mockTimer = new Mock<Timer>();
            _handler = new CrawlerLinksHandler(_mockTimer.Object);
        }

        [Fact]
        public void GetResultsOfCrawling_ListsOfUrls_CollectionOfTypeCrawlingResult()
        {
            // Arrange
            _mockTimer.SetupSequence(x => x.CheckTimeResponse(It.IsAny<string>()))
                .Returns(100).Returns(100);

            // Act
            var result = _handler.GetResultOfCrawling(new List<string> { "test1" });

            // Assert
            Assert.IsType(new TimeOfResponseResult().GetType(), result.First());
        }
    }
}
