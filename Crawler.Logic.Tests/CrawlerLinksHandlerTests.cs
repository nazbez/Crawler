using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;

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
            var result = _handler.GetResultOfCrawling(new List<string> { "test1" }, new List<string> { "test2" });

            // Assert
            Assert.IsType(new CrawlingResult().GetType(), result.First());
        }

        [Fact]
        public void GetResultsOfCrawling_ListOfUrls_AllLinksIsUnique()
        {
            // Arrange
            _mockTimer.SetupSequence(x => x.CheckTimeResponse(It.IsAny<string>()))
                .Returns(100).Returns(100);

            // Act
            var result = _handler.GetResultOfCrawling(new List<string> { "test1", "test2" }, new List<string> { "test2" });

            // Assert
            Assert.Equal(result.Distinct().Count(), result.Count());
        }
    }
}
