using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Core.Services;

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

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }

        public async Task<Product> AddProduct(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                throw new ArgumentNullException(nameof(productDTO));
            }

            Product product = new Product 
            {
               Id = Guid.NewGuid().ToString(),
                DataCriacao = DateTime.Now,
                Titulo = productDTO.Titulo,
                SKU = productDTO.SKU,
                PrecoPor = productDTO.PrecoPor,
                CategoriaId = productDTO.CategoriaId,
                MarcaId = productDTO.MarcaId,
                Images = productDTO.Images,
            };
            
            await _productRepository.AddProductAsync(product);

            return product;
        }

        public async Task<Product> UpdateProduct(string productId, ProductDTO productDTO)
        {
            if(productId == null || productDTO == null) { 
                throw new ArgumentNullException("Produto ou o Id do produto não pode ser nulo");
            }

            var existingProduct = await _productRepository.GetProductByIdAsync(productId);

            if (existingProduct == null)
            {
                throw new ArgumentException("Produto não encontrado");
            }

            existingProduct.Titulo = productDTO.Titulo;
            existingProduct.SKU = productDTO.SKU;
            existingProduct.PrecoPor = productDTO.PrecoPor;
            existingProduct.DataCriacao = productDTO.DataCriacao;
            existingProduct.DataAtualizacao = DateTime.Now;
            existingProduct.CategoriaId = productDTO.CategoriaId;
            existingProduct.MarcaId = productDTO.MarcaId;
            existingProduct.Images = productDTO.Images;
                       
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

            return await _productRepository.DeleteProductAsync(productId);
        }
    }
}
