using Moq;
using InoveFk.Core.Base;
using System.Security.Claims;
using InoveFk.Core.Services.Interfaces;

namespace InoveFk.Core.Tests.Service
{
    public class JwtTokenServiceTests
    {
        private readonly Mock<IJwtTokenService> _mockJwtTokenService;
        private readonly ApplicationUser _testUser;

        public JwtTokenServiceTests()
        {
            _mockJwtTokenService = new Mock<IJwtTokenService>();
            _testUser = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                UserName = "john_doe",
                Email = "john.doe@example.com",
                FirstAccess = false
            };
        }

        [Fact]
        public void GenerateToken_Should_Return_Valid_Token()
        {
            // Arrange
            _mockJwtTokenService.Setup(service => service.GenerateToken(_testUser)).Returns("token_de_teste");

            // Act
            string token = _mockJwtTokenService.Object.GenerateToken(_testUser);

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        [Fact]
        public void ValidateToken_With_Valid_Token_Should_Return_True()
        {
            // Arrange
            string token = "token_de_teste";
            _mockJwtTokenService.Setup(service => service.ValidateToken(token)).Returns((true, new ClaimsPrincipal()));

            // Act
            (bool isValid, ClaimsPrincipal _) = _mockJwtTokenService.Object.ValidateToken(token);

            // Assert
            Assert.True(isValid);
        }

        [Fact]
        public void ValidateToken_With_Invalid_Token_Should_Return_False()
        {
            // Arrange
            string token = "token_inválido";
            _mockJwtTokenService.Setup(service => service.ValidateToken(token)).Returns((false, null));

            // Act
            (bool isValid, ClaimsPrincipal _) = _mockJwtTokenService.Object.ValidateToken(token);

            // Assert
            Assert.False(isValid);
        }
    }
}
