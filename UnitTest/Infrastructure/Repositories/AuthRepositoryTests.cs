using Core.Models;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Infrastructure.Repositories
{
    public class AuthRepositoryTests
    {
        private readonly DbContextOptions<EficazDbContext> _options;

        public AuthRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<EficazDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task GetUserByEmailAndPassword_ShouldReturnUser()
        {
            using (var context = new EficazDbContext(_options))
            {
                var authRepository = new AuthRepository(context);
                var user = new User
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
                };

                context.Users.Add(user);
                await context.SaveChangesAsync();

                // Act
                var result = await authRepository.GetUserByEmailAndPassword("joao.souza@example.com", "senha123");

                Assert.NotNull(result);
                Assert.Equal("joao.souza@example.com", result.Email);
                Assert.Equal("senha123", result.Senha);
            }
        }

        [Fact]
        public async Task GetUserByEmailAndPassword_ShouldReturnNull_WhenUserNotFound()
        {
            using (var context = new EficazDbContext(_options))
            {
                var authRepository = new AuthRepository(context);

                var result = await authRepository.GetUserByEmailAndPassword("notfound@example.com", "wrongpassword");

                Assert.Null(result);
            }
        }
    }
}
