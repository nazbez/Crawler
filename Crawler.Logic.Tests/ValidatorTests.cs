using Xunit;

namespace Crawler.Logic.Tests
{
    public class ValidatorTests
    {
        // Arrange
        private readonly Validator _validator = new Validator();

        [Fact]
        public void IsValid_InvalidParam_ReturnErrorMessage()
        {
            // Act
            string result = _validator.IsValid("Test");

            // Assert
            Assert.Equal("Invalid input!", result);
        }

        [Fact]
        public void IsValid_ValidParam_ReturnEmptyString()
        {
            // Act
            string result = _validator.IsValid("https://google.com");

            // Assert
            Assert.Equal("", result);
        }
    }
}
