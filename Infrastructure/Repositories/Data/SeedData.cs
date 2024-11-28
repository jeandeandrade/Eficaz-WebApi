using Microsoft.EntityFrameworkCore;
using Core.Models;
using Microsoft.Extensions.DependencyInjection;
using Core.DTOs;
using static System.Net.WebRequestMethods;

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
                // Seed Users
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
                            ImageUrl = "",
                            Enderecos = new List<Address>
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
                            ImageUrl = "",
                            Enderecos = new List<Address>
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
                    };

                    context.Users.AddRange(users);
                }

                context.SaveChanges();

                if (!context.Category.Any())
                {
                    var categories = new List<Category>
                    {
                        new Category
                        {
                            Id = 1,
                            Nome = "Eletrônicos",
                            Excluido = false,
                            Descricao = "Produtos eletrônicos diversos",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow,
                            ImageUrl = "https://worldshoptb.com/wp-content/uploads/2021/05/img-worldshop-banner-site.png"
                        },
                        new Category
                        {
                            Id = 2,
                            Nome = "Eletrodomésticos",
                            Excluido = false,
                            Descricao = "Eletrodomésticos para casa",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow,
                            ImageUrl = "https://www.simplesdecoracao.com.br/wp-content/uploads/2015/04/tudo.jpg"
                        },
                        new Category
                        {
                            Id = 3,
                            Nome = "Moda",
                            Excluido = false,
                            Descricao = "Roupas e acessórios",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow,
                            ImageUrl = "https://marketingalternativobtl.com/wp-content/uploads/2021/12/tipos-de-disenos-de-moda.jpg"
                        },
                        new Category
                        {
                            Id = 4,
                            Nome = "Esportes",
                            Excluido = false,
                            Descricao = "Artigos esportivos",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow,
                            ImageUrl = "https://img.freepik.com/premium-photo/international-sports-day-6-april_10221-18936.jpg"
                        }
                    };

                    context.Category.AddRange(categories);
                }

                context.SaveChanges();

                if (!context.Marca.Any())
                {
                    var brands = new List<Marca>
                    {
                        new Marca
                        {
                            Id = 1,
                            Nome = "Samsung",
                            Excluido = false,
                            Descricao = "Tecnologia e inovação",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow
                        },
                        new Marca
                        {
                            Id = 2,
                            Nome = "LG",
                            Excluido = false,
                            Descricao = "Eletrodomésticos e eletrônicos",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow
                        },
                        new Marca
                        {
                            Id = 3,
                            Nome = "Nike",
                            Excluido = false,
                            Descricao = "Roupas e calçados esportivos",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow
                        },
                        new Marca
                        {
                            Id = 4,
                            Nome = "Adidas",
                            Excluido = false,
                            Descricao = "Estilo esportivo e conforto",
                            DataCriacao = DateTime.UtcNow,
                            DataAtualizacao = DateTime.UtcNow
                        }
                    };

                    context.Marca.AddRange(brands);
                }

                context.SaveChanges();

                // Seed Products
                if (!context.Product.Any())
                {
#pragma warning disable CS8619 // A anulabilidade de tipos de referência no valor não corresponde ao tipo de destino.
                    var products = new List<Product>
                        {
                            new Product
                            {
                                Titulo = "Smartphone Galaxy S24 Ultra",
                                Descricao = "Smartphone da Gabini",
                                SKU = "SM-GALAXY-01",
                                MarcaId = 1,
                                CategoriaId = 1,
                                PrecoDe = 1500.00,
                                PrecoPor = 1200.00,
                                ProdutoDestaque = true,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_604807-MLA79305392360_092024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_981605-MLA76719255700_062024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_842385-MLA76718903804_062024-O.webp"
                                }
                            },
                            new Product
                            {
                                Titulo = "Smart Tv 55 4k Quantum Dot Nanocell 55qned7s Hdmi 2.1 LGSmart Tv 55 4k Quantum Dot Nanocell 55qned7s Hdmi 2.1 LG",
                                Descricao = "Smart Tv da Gabini",
                                SKU = "TV-LED-LG-01",
                                MarcaId = 2,
                                CategoriaId = 2,
                                PrecoDe = 4500.00,
                                PrecoPor = 2900.00,
                                ProdutoDestaque = true,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_948342-MLU79289418119_092024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_861343-MLU79289348687_092024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_814140-MLU79289565413_092024-O.webp"
                                }
                            },
                            new Product
                            {
                                Titulo = "Tenis Unissex Air Sb Dunk Low Pro Chlorophyll Couro Original",
                                Descricao = "Tenis da Gabini",
                                SKU = "TENIS-NIKE-AIR-01",
                                MarcaId = 3,
                                CategoriaId = 3,
                                PrecoDe = 400.00,
                                PrecoPor = 350.00,
                                ProdutoDestaque = false,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_956749-MLB80626886338_112024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_628093-MLB80890019569_112024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_612381-MLB80626836738_112024-O.webp"
                                }
                            },
                            new Product
                            {
                                Titulo = "Camiseta Adidas",
                                Descricao = "Camiseta da Gabini",
                                SKU = "CAM-ADIDAS-01",
                                MarcaId = 4,
                                CategoriaId = 3,
                                PrecoDe = 120.00,
                                PrecoPor = 100.00,
                                ProdutoDestaque = false,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_917429-MLB53470141922_012023-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_748552-MLB53470227687_012023-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_947802-MLB53470141924_012023-O.webp"
                                }
                            },
                            // Novos produtos
                            new Product
                            {
                                Titulo = "Notebook Dell Inspiron",
                                Descricao = "Notebook da Gabini",
                                SKU = "NOTE-DELL-01",
                                MarcaId = 2,
                                CategoriaId = 1,
                                PrecoDe = 5000.00,
                                PrecoPor = 4800.00,
                                ProdutoDestaque = true,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_688100-MLU78192187634_082024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_829433-MLU78192197414_082024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_681256-MLU78192187646_082024-O.webp"
                                }
                            },
                            new Product
                            {
                                Titulo = "Fone De Ouvido Wave Buds Sem Fio Preto Jbl",
                                Descricao = "Fone De Ouvido da Gabini",
                                SKU = "FONE-JBL-01",
                                MarcaId = 3,
                                CategoriaId = 2,
                                PrecoDe = 300.00,
                                PrecoPor = 250.00,
                                ProdutoDestaque = true,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_959513-MLU72748569671_112023-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_812710-MLU71148617421_082023-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_776187-MLU76000268247_042024-O.webp"
                                }
                            },
                            new Product
                            {
                                Titulo = "Smartwatch Amazfit GTR Mini Misty Pink, Linha Fashion, Resistente à Água, GPS, Bluetooth, Silicone",
                                Descricao = "Smartwatch da Gabini",
                                SKU = "RELOGIO-SMART-01",
                                MarcaId = 4,
                                CategoriaId = 4,
                                PrecoDe = 800.00,
                                PrecoPor = 750.00,
                                ProdutoDestaque = false,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_640159-MLU77330528147_062024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_922158-MLU77330422345_062024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_935708-MLU77117746034_062024-O.webp"
                                }
                            },
                            new Product
                            {
                                Titulo = "Mouse Gamer Logitech G403 HERO Sensor Hero 25K",
                                Descricao = "Mouse Gamer da Gabini",
                                SKU = "MOUSE-GAMER-01",
                                MarcaId = 4,
                                CategoriaId = 1,
                                PrecoDe = 150.00,
                                PrecoPor = 120.00,
                                ProdutoDestaque = false,
                                DataCriacao = DateTime.UtcNow,
                                DataAtualizacao = DateTime.UtcNow,
                                Images = new List<string>
                                {
                                    "https://http2.mlstatic.com/D_NQ_NP_876753-MLA74781932161_022024-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_616979-MLA43229493908_082020-O.webp",
                                    "https://http2.mlstatic.com/D_NQ_NP_858064-MLA79668854313_092024-O.webp"
                                }
                            }
                        };

                    context.Product.AddRange(products);
                }

                context.SaveChanges();
            }
        }
    }
}
