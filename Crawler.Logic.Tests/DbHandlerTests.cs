using Moq;
using Xunit;
using System.Data;
using Crawler.Models;
using Crawler.Logic.Models;
using System.Collections.Generic;
using System.Threading;

namespace Crawler.Logic.Tests
{
    public class DbHandlerTests
    {
        private readonly Mock<IRepository<Test>> _mockTestRepository;
        private readonly Mock<IRepository<TestResult>> _mockTestResultRepository;
        private readonly DbHandler _dbHandler;

        public DbHandlerTests()
        {
            _mockTestRepository = new Mock<IRepository<Test>>();
            _mockTestResultRepository = new Mock<IRepository<TestResult>>();
            _dbHandler = new DbHandler(_mockTestRepository.Object, _mockTestResultRepository.Object);
        }


        [Fact]
        public void SaveResultAsync_AddTest()
        {
            //Arrange
            var crawlingResults = new List<CrawlingResult>() { new CrawlingResult() };
            var timeResponseResults = new List<TimeOfResponseResult> { new TimeOfResponseResult() };

            //Act
            _dbHandler.SaveResultAsync("https://url.com", crawlingResults, timeResponseResults).Wait();

            //Assert
            _mockTestRepository.Verify(r => r.AddAsync(It.IsAny<Test>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public void SaveResultAsync_AddTestResult()
        {
            //Arrange
            var crawlingResults = new List<CrawlingResult>() { new CrawlingResult() };
            var timeResponseResults = new List<TimeOfResponseResult> { new TimeOfResponseResult() };

            //Act
            _dbHandler.SaveResultAsync("https://url.com", crawlingResults, timeResponseResults).Wait();

            //Assert
            _mockTestResultRepository.Verify(r => r.AddRange(It.IsAny<IEnumerable<TestResult>>()), Times.Once);
        }
    }
}
