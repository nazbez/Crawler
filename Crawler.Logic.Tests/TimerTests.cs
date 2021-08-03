using Xunit;

namespace Crawler.Logic.Tests
{
    public class TimerTests
    {
        // Arrange
        private readonly Timer _timer = new Timer();

        [Fact]
        public void CheckTimeResponse_InvalidParam_MinusOneAndNotEmptyErrorMsg()
        {
            // Act
            var timeResponseResult = _timer.CheckTimeResponse("Test");

            // Assert
            Assert.Equal(-1, timeResponseResult.Time);
            Assert.NotEqual("", timeResponseResult.ErrorMsg);
        }
    }
}
