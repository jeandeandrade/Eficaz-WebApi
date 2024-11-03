using Application.Services;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;
using System.Security.Claims;
using Xunit;

namespace UnitTests.Application.Services
{
    public class AuthServiceTests
    {
        private readonly AuthService _authService;
        private readonly Mock<IAuthRepository> _authRepositoryMock;
        private readonly Mock<ITokenService> _tokenServiceMock;

        public AuthServiceTests()
        {
            _authRepositoryMock = new Mock<IAuthRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _authService = new AuthService(_authRepositoryMock.Object, _tokenServiceMock.Object);
        }

        [Fact]
        public void GetAuthenticatedUserId_ShouldReturnUserId()
        {
            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", "1")
            }, "mock"));

            var result = _authService.GetAuthenticatedUserId(userClaims);

            Assert.NotNull(result);
            Assert.Equal("1", result);
        }

        [Fact]
        public async Task SignIn_ShouldReturnToken()
        {
            var email = "test@example.com";
            var password = "password";
            var user = new User { Id = "1", Email = email };
            var token = "mock-token";

            _authRepositoryMock.Setup(repo => repo.GetUserByEmailAndPassword(email, password)).ReturnsAsync(user);
            _tokenServiceMock.Setup(service => service.CreateUserToken(user)).Returns(token);

            var result = await _authService.SignIn(email, password);

            Assert.NotNull(result);
            Assert.Equal(token, result);
        }

        [Fact]
        public async Task SignIn_ShouldThrowException_WhenUserNotFound()
        {
            var email = "test@example.com";
            var password = "wrongpassword";

            _authRepositoryMock.Setup(repo => repo.GetUserByEmailAndPassword(email, password)).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<Exception>(() => _authService.SignIn(email, password));
        }
    }
}
