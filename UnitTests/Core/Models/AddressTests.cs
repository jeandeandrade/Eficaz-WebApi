using Core.Models;
using Xunit;

namespace UnitTests.Core.Models
{
    public class AddressTests
    {
        [Fact]
        public void CreateAddress_ShouldCreateAddressWithValidProperties()
        {
            var user = new User
            {
                Id = "123",
                Nome = "João",
                Apelido = "João",
                Cpf = "987.654.321-00",
                DataNascimento = DateTime.Now,
                Genero = "Masculino",
                Telefone = "21912345678",
                Email = "joao.souza@example.com",
                Senha = "senha123"
            };

            var address = new Address(user, "Rua das Acácias", "Jardim das Acácias", "23456-789", "Casa 1", "Rio de Janeiro", "50")
            {
                Id = "456"
            };

            Assert.NotNull(address);
            Assert.Equal(user, address.User);
            Assert.Equal(user.Id, address.UserId);
            Assert.Equal("Rua das Acácias", address.NomeRua);
            Assert.Equal("Jardim das Acácias", address.Bairro);
            Assert.Equal("23456-789", address.Cep);
            Assert.Equal("Casa 1", address.Complemento);
            Assert.Equal("Rio de Janeiro", address.Cidade);
            Assert.Equal("50", address.NumeroResidencia);
        }
    }
}
