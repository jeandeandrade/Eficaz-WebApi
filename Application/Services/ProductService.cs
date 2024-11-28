using Core.DTOs;
using Core.Models;
using Core.Repositories;
using Core.Services;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IImageService _imageService;

        public ProductService(IProductRepository productRepository, IImageService imageService)
        {
            _productRepository = productRepository;
            _imageService = imageService;
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

        public async Task<string> UploadProductImage(string productId, FileData file)
        {
            Product product = await GetProductByIdAsync(productId);

            string uploadedFileUrl = await _imageService.UploadImage(file, "products", productId);

            if (product.Images == null)
            {
                product.Images = new List<string?>();
            }

            product.Images.Add(uploadedFileUrl);

            await _productRepository.UpdateProduct();

            return uploadedFileUrl;
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
                Descricao = productDTO.Descricao,
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
            existingProduct.Descricao = productDTO.Descricao;
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
