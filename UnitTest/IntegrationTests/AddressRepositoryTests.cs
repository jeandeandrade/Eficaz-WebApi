using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTest.IntegrationTests
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
                await context.SaveChangesAsync();

                var result = await addressRepository.GetAddressByIdAsync(address.Id);

                Assert.NotNull(result);
                Assert.Equal(address.Id, result.Id);
            }
        }

        [Fact]
        public async Task AddAddressAsync_ShouldAddAddress()
        {
            using (var context = new EficazDbContext(_options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

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

                await addressRepository.AddAddressAsync(address);
                var addressFromDb = await context.Address.FindAsync(address.Id);

                Assert.NotNull(addressFromDb);
                Assert.Equal(address.Id, addressFromDb.Id);
            }
        }

        private DbContextOptions<EficazDbContext> CreateNewContextOptions()
        {
            return new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task UpdateAddressAsync_ShouldUpdateAddress()
        {
            var options = CreateNewContextOptions();

            using (var context = new EficazDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

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
                await context.SaveChangesAsync();

                address.NomeRua = "Rua Nova";

                await addressRepository.UpdateAddressAsync(address.Id, address);
                var addressFromDb = await context.Address.FindAsync(address.Id);

                Assert.NotNull(addressFromDb);
                Assert.Equal("Rua Nova", addressFromDb.NomeRua);
            }
        }


        [Fact]
        public async Task DeleteAddressAsync_ShouldReturnTrue()
        {
            var options = new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new EficazDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

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
                await context.SaveChangesAsync();

                var result = await addressRepository.DeleteAddressAsync(address.Id);

                Assert.True(result);
                var addressFromDb = await context.Address.FindAsync(address.Id);
                Assert.Null(addressFromDb);
            }
        }
    }
}