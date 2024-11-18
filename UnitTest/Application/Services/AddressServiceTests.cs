using Application.Services;
using Core.Models;
using Core.Repositories;
using Core.Services;
using Moq;
using Xunit;

namespace UnitTests.Application.Services
{
    public class AddressServiceTests
    {
        private readonly AddressService _addressService;
        private readonly Mock<IAddressRepository> _addressRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public AddressServiceTests()
        {
            _addressRepositoryMock = new Mock<IAddressRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _addressService = new AddressService(_addressRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task AddAddress_ShouldAddAddress()
        {
            var userId = "1";
            var user = new User { Id = userId, Nome = "nome" };
            var address = new Address {Id = "123", NomeRua = "Rua das Acácias" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _addressService.AddAddress(userId, address);

            _addressRepositoryMock.Verify(repo => repo.AddAddressAsync(It.IsAny<Address>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("Rua das Acácias", result.NomeRua);
            Assert.Equal(user, result.User);
        }

        [Fact]
        public async Task UpdateAddress_ShouldUpdateAddress()
        {
            var userId = "1";
            var addressId = "addressId";
            var user = new User { Id = userId, Nome = "nome", Enderecos = new List<Address> { new Address { Id = addressId, NomeRua = "Old Name" } } };
            var newAddress = new Address { Id = addressId, NomeRua = "New Name" };
            _userRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId)).ReturnsAsync(user);

            var result = await _addressService.UpdateAddress(userId, newAddress);

            _addressRepositoryMock.Verify(repo => repo.UpdateAddressAsync(addressId, It.IsAny<Address>()), Times.Once);
            Assert.NotNull(result);
            Assert.Equal("New Name", result.NomeRua);
        }

        [Fact]
        public async Task DeleteAddress_ShouldReturnTrue()
        {
            var addressId = "1";
            var address = new Address { Id = addressId, NomeRua = "Rua das Acácias" };
            _addressRepositoryMock.Setup(repo => repo.GetAddressByIdAsync(addressId)).ReturnsAsync(address);
            _addressRepositoryMock.Setup(repo => repo.DeleteAddressAsync(addressId)).ReturnsAsync(true);

            var result = await _addressService.DeleteAddress(addressId);

            _addressRepositoryMock.Verify(repo => repo.DeleteAddressAsync(addressId), Times.Once);
            Assert.True(result);
        }
    }
}
