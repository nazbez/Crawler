using Xunit;

namespace Crawler.Logic.Tests
{
    public class ValidatorTests
    {
        // Arrange
        private readonly Validator _validator = new Validator();

        [Fact]
        public void IsValid_InvalidParam_False()
        {
            // Act
            bool result = _validator.IsValid("Test");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_ValidParam_True()
        {
            // Act
            bool result = _validator.IsValid("https://google.com");

            // Assert
            Assert.True(result);
        }
    }
}
