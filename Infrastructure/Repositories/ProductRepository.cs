using Core.Models;
using Core.Repositories;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EficazDbContext _context;

        public ProductRepository(EficazDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            return await _context.Product.Include(u => u.Marca).Include(u => u.Categoria).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateProduct()
        {
          await _context.SaveChangesAsync();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<Product> UpdateProductAsync(string id, Product product)
        {
            var existingProduct = await _context.Product.FirstOrDefaultAsync(u => u.Id == id);

            if (existingProduct == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            existingProduct.Titulo = product.Titulo;
            existingProduct.Descricao = product.Descricao;
            existingProduct.SKU = product.SKU;
            existingProduct.PrecoPor = product.PrecoPor;
            existingProduct.DataCriacao = product.DataCriacao;
            existingProduct.DataAtualizacao = DateTime.Now;
            existingProduct.Categoria = product.Categoria;
            existingProduct.Marca = product.Marca;
            existingProduct.Images = product.Images;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var listProducts = await _context.Product.Include(u => u.Marca).Include(u => u.Categoria).ToListAsync();
            return listProducts;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var existingProduct = await _context.Product.FirstOrDefaultAsync(u => u.Id == id);

            if (existingProduct == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            existingProduct.Excluido = true; 
            
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
