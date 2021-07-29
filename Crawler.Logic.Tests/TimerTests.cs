using Xunit;

namespace Crawler.Logic.Tests
{
    public class TimerTests
    {
        // Arrange
        private readonly Timer _timer = new Timer();

        [Fact]
        public void CheckTimeResponse_InvalidParam_MinusOne()
        {
            // Act
            double time = _timer.CheckTimeResponse("Test");

            // Assert
            Assert.Equal(-1, time);
        }

        [Fact]
        public void CheckTimeResponse_InvalidParam_NotZero()
        {
            // Act
            double time = _timer.CheckTimeResponse("https://google.com");

            // Assert
            Assert.InRange(time, 50, 1000);
        }

        
    }
}
