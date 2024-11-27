using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Presentation.Controllers;

namespace UnitTests.Presentation.Controllers
{
    public class AuthControllerTests
    {
        private readonly AuthController _controller;
        private readonly Mock<IAuthService> _authServiceMock;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task SignIn_ShouldReturnToken()
        {
            // Arrange
            var signInDTO = new SignInDTO { Email = "test@example.com", Password = "password" };
            var token = "mock-token";

            _authServiceMock.Setup(service => service.SignIn(signInDTO.Email, signInDTO.Password)).ReturnsAsync(token);

            // Act
            var result = await _controller.SignIn(signInDTO);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(nameof(_controller.SignIn), createdAtActionResult.ActionName);
            Assert.Equal(token, createdAtActionResult.Value);
        }

        [Fact]
        public async Task SignIn_ShouldReturnBadRequest_WhenCredentialsAreInvalid()
        {
            // Arrange
            var signInDTO = new SignInDTO { Email = "test@example.com", Password = "wrongpassword" };

            _authServiceMock.Setup(service => service.SignIn(signInDTO.Email, signInDTO.Password)).ThrowsAsync(new Exception("Usuário e/ou senha inválidos"));

            // Act
            var result = await _controller.SignIn(signInDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Usuário e/ou senha inválidos", badRequestResult.Value);
        }
    }
}
