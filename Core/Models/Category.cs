using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Excluido { get; set; }
        public string Descricao { get; set; }
        public List<Product> Produtos { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public Category () {}

        public Category(int id, string nome, bool excluido, string descricao, List<Product> produtos, DateTime dataCriacao, DateTime dataAtualizacao)
        {
            Id = id;
            Nome = nome;
            Excluido = excluido;
            Descricao = descricao;
            Produtos = produtos;
            DataCriacao = dataCriacao;
            DataAtualizacao = dataAtualizacao;
        }

        public Category(string nome, bool excluido, string descricao, List<Product> produtos, DateTime dataCriacao, DateTime dataAtualizacao)
        {
            Nome = nome;
            Excluido = excluido;
            Descricao = descricao;
            Produtos = produtos;
            DataCriacao = dataCriacao;
            DataAtualizacao = dataAtualizacao;
        }
    }
}
