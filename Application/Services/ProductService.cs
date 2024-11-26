using Core.Models;
using Core.Repositories;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProductByIdAsync(string id)
        {
            Product? product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                throw new ArgumentException("Produto não cadastrado");
            }

            return product;
        }

        public async Task<Product> AddProduct(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            product.Id = Guid.NewGuid().ToString();
            product.DataCriacao = DateTime.Now;
            await _productRepository.AddProductAsync(product);

            return product;
        }

        public async Task<Product> UpdateProduct(string productId, Product product)
        {
            if(productId == null || product == null) { 
                throw new ArgumentNullException("Produto ou o Id do produto não pode ser nulo");
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(productId);

            if (existingProduct == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            existingProduct.Titulo = product.Titulo;
            existingProduct.SKU = product.SKU;
            existingProduct.PrecoPor = product.PrecoPor;
            existingProduct.DataCriacao = product.DataCriacao;
            existingProduct.DataAtualizacao = DateTime.Now;
            existingProduct.Categoria = product.Categoria;
            existingProduct.Marca = product.Marca;

            await _productRepository.UpdateProductAsync(productId, existingProduct);

            return existingProduct;
        }


        public async Task<bool> DeleteProduct(string productId)
        {
            if(string.IsNullOrEmpty(productId)) { 
                throw new ArgumentNullException("O productId não pode ser nulo"); 
            }

            var product = await _productRepository.GetProductByIdAsync(productId);
            
            if (product == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }


        }
    }
}
