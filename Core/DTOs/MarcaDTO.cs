using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MarcaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Excluido { get; set; }
        public string Descricao { get; set; }
        public List<ProductDTO> Produtos { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
