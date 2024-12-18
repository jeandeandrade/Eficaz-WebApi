using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ProductDTO
    {
        public string Titulo { get; set; }

        public string Descricao { get; set; }
        public string SKU { get; set; }
        public int MarcaId { get; set; }
        public int CategoriaId { get; set; }
        public double PrecoDe { get; set; }
        public double PrecoPor { get; set; }
        public bool ProdutoDestaque { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public List<string?> Images { get; set; }
    }
}
