using Core.Models;
using Xunit;

namespace UnitTests.Core.Models
{
    public class UserTests
    {
        [Fact]
        public void CreateUser_UsingFullConstructor_ShouldSetPropertiesCorrectly()
        {
            var user = new User("nome", "apelido", "cpf", DateTime.Now, "genero", "telefone", "email", "senha") { Id = "123" };

            Assert.NotNull(user);
            Assert.Equal("123", user.Id);
            Assert.Equal("nome", user.Nome);
            Assert.Equal("apelido", user.Apelido);
            Assert.Equal("cpf", user.Cpf);
            Assert.Equal(DateTime.Now.Date, user.DataNascimento.Date);
            Assert.Equal("genero", user.Genero);
            Assert.Equal("telefone", user.Telefone);
            Assert.Equal("email", user.Email);
            Assert.Equal("senha", user.Senha);
            Assert.NotNull(user.Enderecos);
            Assert.Empty(user.Enderecos);
        }

        [Fact]
        public void CreateUser_UsingConstructorWithoutId_ShouldSetPropertiesCorrectly()
        {
            // Act
            var user = new User("nome", "apelido", "cpf", DateTime.Now, "genero", "telefone", "email", "senha")
            {
                Id = "123"
            };

            Assert.NotNull(user);
            Assert.Equal("123", user.Id);
            Assert.Equal("nome", user.Nome);
            Assert.Equal("apelido", user.Apelido);
            Assert.Equal("cpf", user.Cpf);
            Assert.Equal(DateTime.Now.Date, user.DataNascimento.Date);
            Assert.Equal("genero", user.Genero);
            Assert.Equal("telefone", user.Telefone);
            Assert.Equal("email", user.Email);
            Assert.Equal("senha", user.Senha);
            Assert.NotNull(user.Enderecos);
            Assert.Empty(user.Enderecos);
        }

        [Fact]
        public void CreateUser_UsingParameterlessConstructor_ShouldInitializeDefaultValues()
        {
            var user = new User
            {
                Id = "123",
                Nome = "nome",
                Apelido = "apelido",
                Cpf = "cpf",
                DataNascimento = DateTime.Now,
                Genero = "genero",
                Telefone = "telefone",
                Email = "email",
                Senha = "senha"
            };

            Assert.NotNull(user);
            Assert.Equal("123", user.Id);
            Assert.Equal("nome", user.Nome);
            Assert.Equal("apelido", user.Apelido);
            Assert.Equal("cpf", user.Cpf);
            Assert.Equal(DateTime.Now.Date, user.DataNascimento.Date);
            Assert.Equal("genero", user.Genero);
            Assert.Equal("telefone", user.Telefone);
            Assert.Equal("email", user.Email);
            Assert.Equal("senha", user.Senha);
            Assert.NotNull(user.Enderecos);
            Assert.Empty(user.Enderecos);
        }
    }
}
