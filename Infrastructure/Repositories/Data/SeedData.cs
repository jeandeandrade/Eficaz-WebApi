using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Repositories.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EficazDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EficazDbContext>>()))
            {
                if (!context.Users.Any())
                {
                    var users = new List<User>
                    {
                        new User
                        {
                            Id = Guid.NewGuid().ToString(),
                            Nome = "Maria Silva",
                            Apelido = "Maria",
                            Cpf = "123.456.789-00",
                            DataNascimento = new DateTime(1990, 1, 1),
                            Genero = "Feminino",
                            Telefone = "11987654321",
                            Email = "maria.silva@example.com",
                            Senha = "senha123",
                            Addresses = new List<Address>
                            {
                                new Address
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    NomeRua = "Rua das Flores",
                                    Bairro = "Jardim das Flores",
                                    Cep = "12345-678",
                                    Complemento = "Apt 101",
                                    Cidade = "São Paulo",
                                    NumeroResidencia = "100"
                                }
                            }
                        },
                        new User
                        {
                            Id = Guid.NewGuid().ToString(),
                            Nome = "João Souza",
                            Apelido = "João",
                            Cpf = "987.654.321-00",
                            DataNascimento = new DateTime(1985, 5, 15),
                            Genero = "Masculino",
                            Telefone = "21912345678",
                            Email = "joao.souza@example.com",
                            Senha = "senha123",
                            Addresses = new List<Address>
                            {
                                new Address
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    NomeRua = "Rua das Acácias",
                                    Bairro = "Jardim das Acácias",
                                    Cep = "23456-789",
                                    Complemento = "Casa 1",
                                    Cidade = "Rio de Janeiro",
                                    NumeroResidencia = "50"
                                }
                            }
                        },
                        // Adicione mais usuários aqui...
                    };

                    context.Users.AddRange(users);
                }

                context.SaveChanges();
            }
        }
    }
}
