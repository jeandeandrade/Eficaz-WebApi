using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string SKU { get; set; }
        public Marca Marca { get; set; }
        public int MarcaId { get; set; }
        public Category Categoria { get; set; }
        public int CategoriaId { get; set; }
        public double PrecoDe { get; set; }
        public double PrecoPor { get; set; }
        public bool ProdutoDestaque { get; set; }
        public bool Excluido { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<string?> Images { get; set; }



        public Product()
        {
            DataCriacao = DateTime.Now;
        }
        public Product(string id, string titulo, string sku, Marca marca, Category categoria, double precoDe, double precoPor, bool produtoDestaque, bool excluido, DateTime dataCriacao, DateTime dataAtualizacao, List<string> images)
        {
            Id = id;
            Titulo = titulo;
            SKU = sku;
            Marca = marca;
            MarcaId = marca.Id;
            Categoria = categoria;
            CategoriaId = categoria.Id;
            PrecoDe = precoDe;
            PrecoPor = precoPor;
            ProdutoDestaque = produtoDestaque;
            Excluido = excluido;
            DataCriacao = dataCriacao;
            DataAtualizacao = dataAtualizacao;
            Images = images;
        }

        public Product(string titulo, string sku, Marca marca, Category categoria, double precoDe, double precoPor, bool produtoDestaque, bool excluido, DateTime dataCriacao, DateTime dataAtualizacao, List<string> images)
        {
            Titulo = titulo;
            SKU = sku;
            Marca = marca;
            MarcaId = marca.Id;
            Categoria = categoria;
            CategoriaId = categoria.Id;
            PrecoDe = precoDe;
            PrecoPor = precoPor;
            ProdutoDestaque = produtoDestaque;
            Excluido = excluido;
            DataCriacao = dataCriacao;
            DataAtualizacao = dataAtualizacao;
            Images = images;
        }
    }
}
