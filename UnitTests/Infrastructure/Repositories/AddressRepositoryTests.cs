using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Infrastructure.Repositories
{
    public class AddressRepositoryTests
    {
        private readonly DbContextOptions<EficazDbContext> _options;

        public AddressRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task GetAddressByIdAsync_ShouldReturnAddress()
        {
            using (var context = new EficazDbContext(_options))
            {
                // Limpa o banco antes de cada teste
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Arrange
                var addressRepository = new AddressRepository(context);
                var address = new Address
                {
                    Id = Guid.NewGuid().ToString(), // Gere um ID único
                    NomeRua = "Rua das Acácias",
                    Bairro = "Jardim das Acácias",
                    Cep = "23456-789",
                    Complemento = "Casa 1",
                    Cidade = "Rio de Janeiro",
                    NumeroResidencia = "50",
                    UserId = "1",
                    User = new User
                    {
                        Id = "1",
                        Nome = "João",
                        Apelido = "João",
                        Cpf = "987.654.321-00",
                        DataNascimento = DateTime.Now,
                        Genero = "Masculino",
                        Telefone = "21912345678",
                        Email = "joao.souza@example.com",
                        Senha = "senha123"
                    }
                };

                context.Address.Add(address);
                await context.SaveChangesAsync();

                // Act
                var result = await addressRepository.GetAddressByIdAsync(address.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(address.Id, result.Id);
            }
        }

        [Fact]
        public async Task AddAddressAsync_ShouldAddAddress()
        {
            using (var context = new EficazDbContext(_options))
            {
                // Limpa o banco antes de cada teste
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Arrange
                var addressRepository = new AddressRepository(context);
                var address = new Address
                {
                    Id = Guid.NewGuid().ToString(), // Gere um ID único
                    NomeRua = "Rua das Acácias",
                    Bairro = "Jardim das Acácias",
                    Cep = "23456-789",
                    Complemento = "Casa 1",
                    Cidade = "Rio de Janeiro",
                    NumeroResidencia = "50",
                    UserId = "1",
                    User = new User
                    {
                        Id = "1",
                        Nome = "João",
                        Apelido = "João",
                        Cpf = "987.654.321-00",
                        DataNascimento = DateTime.Now,
                        Genero = "Masculino",
                        Telefone = "21912345678",
                        Email = "joao.souza@example.com",
                        Senha = "senha123"
                    }
                };

                // Act
                await addressRepository.AddAddressAsync(address);
                var addressFromDb = await context.Address.FindAsync(address.Id);

                // Assert
                Assert.NotNull(addressFromDb);
                Assert.Equal(address.Id, addressFromDb.Id);
            }
        }

        [Fact]
        public async Task UpdateAddressAsync_ShouldUpdateAddress()
        {
            using (var context = new EficazDbContext(_options))
            {
                // Limpa o banco antes de cada teste
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Arrange
                var addressRepository = new AddressRepository(context);
                var address = new Address
                {
                    Id = Guid.NewGuid().ToString(), // Gere um ID único
                    NomeRua = "Rua das Acácias",
                    Bairro = "Jardim das Acácias",
                    Cep = "23456-789",
                    Complemento = "Casa 1",
                    Cidade = "Rio de Janeiro",
                    NumeroResidencia = "50",
                    UserId = "1",
                    User = new User
                    {
                        Id = "1",
                        Nome = "João",
                        Apelido = "João",
                        Cpf = "987.654.321-00",
                        DataNascimento = DateTime.Now,
                        Genero = "Masculino",
                        Telefone = "21912345678",
                        Email = "joao.souza@example.com",
                        Senha = "senha123"
                    }
                };

                context.Address.Add(address);
                await context.SaveChangesAsync();

                address.NomeRua = "Rua Nova";

                // Act
                await addressRepository.UpdateAddressAsync(address.Id, address);
                var addressFromDb = await context.Address.FindAsync(address.Id);

                // Assert
                Assert.NotNull(addressFromDb);
                Assert.Equal("Rua Nova", addressFromDb.NomeRua);
            }
        }

        [Fact]
        public async Task DeleteAddressAsync_ShouldReturnTrue()
        {
            using (var context = new EficazDbContext(_options))
            {
                // Limpa o banco antes de cada teste
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Arrange
                var addressRepository = new AddressRepository(context);
                var address = new Address
                {
                    Id = Guid.NewGuid().ToString(), // Gere um ID único
                    NomeRua = "Rua das Acácias",
                    Bairro = "Jardim das Acácias",
                    Cep = "23456-789",
                    Complemento = "Casa 1",
                    Cidade = "Rio de Janeiro",
                    NumeroResidencia = "50",
                    UserId = "1",
                    User = new User
                    {
                        Id = "1",
                        Nome = "João",
                        Apelido = "João",
                        Cpf = "987.654.321-00",
                        DataNascimento = DateTime.Now,
                        Genero = "Masculino",
                        Telefone = "21912345678",
                        Email = "joao.souza@example.com",
                        Senha = "senha123"
                    }
                };

                context.Address.Add(address);
                await context.SaveChangesAsync();

                // Act
                var result = await addressRepository.DeleteAddressAsync(address.Id);
                var addressFromDb = await context.Address.FindAsync(address.Id);

                // Assert
                Assert.True(result);
                Assert.Null(addressFromDb);
            }
        }
    }
}
