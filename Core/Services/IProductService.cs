using Core.Models;

namespace Core.Services
{
    public interface IProductService
    {
        public Task<Product> GetProductByIdAsync(string id);
        public Task<Product> AddProduct(Product product);
        public Task<Product> UpdateProduct(string productId, Product product);
        public Task<List<Product>> GetAllProductsAsync();
        public Task<bool> DeleteProduct(string productId);
    }
}
