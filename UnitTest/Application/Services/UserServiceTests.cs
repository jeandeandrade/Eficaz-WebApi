using Application.Services;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;
using Xunit;

namespace UnitTests.Application.Services
{
    public class UserServiceTests
    {
        private readonly IUserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUser()
        {
            var userId = "1";
            var user = new User { Id = userId, Nome = "nome" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _userService.GetUserByIdAsync(userId);

            Assert.NotNull(result);
            Assert.Equal("nome", result.Nome);
        }

        [Fact]
        public async Task AddUser_ShouldAddUser()
        {
            var user = new User { Id = "123", Nome = "nome" };
            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>())).ReturnsAsync(user);

            var result = await _userService.AddUser(user);

            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("nome", result.Nome);
        }

        [Fact]
        public async Task UpdateUser_ShouldUpdateUser()
        {
            var userId = "1";
            var user = new User { Id = userId, Nome = "nome" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.UpdateUserAsync(userId, It.IsAny<User>())).ReturnsAsync(user);

            var result = await _userService.UpdateUser(userId, user);

            _userRepositoryMock.Verify(repo => repo.UpdateUserAsync(userId, It.IsAny<User>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("nome", result.Nome);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnTrue()
        {
            var userId = "1";
            var user = new User { Id = userId, Nome = "nome" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.DeleteUserAsync(userId)).ReturnsAsync(true);

            var result = await _userService.DeleteUser(userId);

            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(userId), Times.Once);
            Assert.True(result);
        }
    }
}
