using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Infrastructure.Repositories
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<EficazDbContext> _options;

        public UserRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;
        }

        [Fact]
        public async Task AddUserAsync_ShouldAddUser()
        {
            using (var context = new EficazDbContext(_options))
            {
                var userRepository = new UserRepository(context);
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Nome = "nome",
                    Apelido = "apelido",
                    Cpf = "12345678900",
                    DataNascimento = DateTime.Now,
                    Genero = "Masculino",
                    Telefone = "123456789",
                    Email = "email@example.com",
                    Senha = "password"
                };

                await userRepository.AddUserAsync(user);
                var userFromDb = await context.Users.FindAsync(user.Id);

                Assert.NotNull(userFromDb);
                Assert.Equal("nome", userFromDb.Nome);
            }
        }

        [Fact]
        public async Task GetAddressByIdAsync_ShouldReturnAddress()
        {
            var options = new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new EficazDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                // Arrange
                var addressRepository = new AddressRepository(context);
                var address = new Address
                {
                    Id = Guid.NewGuid().ToString(),
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
                await context.SaveChangesAsync();  // Garantir que o endereço foi salvo

                // Act
                var result = await addressRepository.GetAddressByIdAsync(address.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(address.Id, result.Id);
            }
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldUpdateUser()
        {
            using (var context = new EficazDbContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var userRepository = new UserRepository(context);
                var userId = Guid.NewGuid().ToString();
                var user = new User
                {
                    Id = userId,
                    Nome = "nome",
                    Apelido = "apelido",
                    Cpf = "12345678900",
                    DataNascimento = DateTime.Now,
                    Genero = "Masculino",
                    Telefone = "123456789",
                    Email = "email@example.com",
                    Senha = "password"
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                user.Nome = "novo_nome";

                var result = await userRepository.UpdateUserAsync(userId, user);
                var userFromDb = await context.Users.FindAsync(userId);

                Assert.NotNull(userFromDb);
                Assert.Equal("novo_nome", userFromDb.Nome);
            }
        }

        private DbContextOptions<EficazDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldDeleteUser()
        {
            var options = CreateNewContextOptions();

            using (var context = new EficazDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var userRepository = new UserRepository(context);
                var userId = Guid.NewGuid().ToString();
                var user = new User
                {
                    Id = userId,
                    Nome = "nome",
                    Apelido = "apelido",
                    Cpf = "12345678900",
                    DataNascimento = DateTime.Now,
                    Genero = "Masculino",
                    Telefone = "123456789",
                    Email = "email@example.com",
                    Senha = "password"
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                var result = await userRepository.DeleteUserAsync(userId);
                var userFromDb = await context.Users.FindAsync(userId);

                Assert.True(result);
                Assert.Null(userFromDb);
            }
        }
    }
}