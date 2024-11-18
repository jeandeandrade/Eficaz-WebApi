using Core.Services;
using Core.Models;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Presentation.Controllers;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace UnitTests.Presentation.Controllers
{
    public class UserControllerTests
    {
        private readonly UserController _controller;
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<IAuthService> _authServiceMock;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _authServiceMock = new Mock<IAuthService>();
            _controller = new UserController(_userServiceMock.Object, _authServiceMock.Object);

            var userClaims = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = userClaims }
            };

            _authServiceMock.Setup(auth => auth.GetAuthenticatedUserId(It.IsAny<ClaimsPrincipal>())).Returns("1");
        }

        [Fact]
        public async Task GetUser_ShouldReturnUser()
        {
            var userId = "1";
            var user = new User { Id = userId, Nome = "nome" };
            _userServiceMock.Setup(service => service.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _controller.GetUser();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("nome", returnedUser.Nome);
        }

        [Fact]
        public async Task PostUser_ShouldReturnCreatedUser()
        {
            var userDto = new UserDTO
            {
                Nome = "nome",
                Apelido = "apelido",
                Email = "email@example.com",
                Cpf = "cpf",
                DataNascimento = DateTime.Now,
                Genero = "genero",
                Telefone = "telefone",
                Enderecos = new List<AddressDTO>()
            };
            var user = new User
            {
                Nome = "nome",
                Apelido = "apelido",
                Email = "email@example.com",
                Cpf = "cpf",
                DataNascimento = DateTime.Now,
                Genero = "genero",
                Telefone = "telefone",
                Enderecos = new List<Address>()
            };
            _userServiceMock.Setup(service => service.AddUser(It.IsAny<User>())).ReturnsAsync(user);

            var result = await _controller.PostUser(userDto);

            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnedUser = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal("nome", returnedUser.Nome);
        }

        [Fact]
        public async Task UpdateUser_ShouldReturnUpdatedUser()
        {
            var userId = "1";
            var userDto = new UserDTO
            {
                Nome = "nome",
                Apelido = "apelido",
                Email = "email@example.com",
                Cpf = "cpf",
                DataNascimento = DateTime.Now,
                Genero = "genero",
                Telefone = "telefone",
                Enderecos = new List<AddressDTO>()
            };
            var user = new User
            {
                Id = userId,
                Nome = "nome",
                Apelido = "apelido",
                Email = "email@example.com",
                Cpf = "cpf",
                DataNascimento = DateTime.Now,
                Genero = "genero",
                Telefone = "telefone",
                Enderecos = new List<Address>()
            };
            _userServiceMock.Setup(service => service.UpdateUser(userId, It.IsAny<User>())).ReturnsAsync(user);

            var result = await _controller.UpdateUser(userDto);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("nome", returnedUser.Nome);
        }

        [Fact]
        public async Task DeleteUser_ShouldReturnOk()
        {
            var userId = "1";
            _userServiceMock.Setup(service => service.DeleteUser(userId)).ReturnsAsync(true);

            var result = await _controller.DeleteUser();

            Assert.IsType<OkResult>(result);
        }
    }
}
