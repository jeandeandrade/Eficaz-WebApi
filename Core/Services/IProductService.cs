using Core.DTOs;
using Core.Models;

namespace Core.Services
{
    public interface IProductService
    {
        public Task<Product> GetProductByIdAsync(string id);
        public Task<string> UploadProductImage(string productId, FileData file);
        public Task<Product> AddProduct(ProductDTO productDTO);
        public Task<Product> UpdateProduct(string productId, ProductDTO productDTO);
        public Task<List<Product>> GetAllProductsAsync();
        public Task<bool> DeleteProduct(string productId);
    }
}
