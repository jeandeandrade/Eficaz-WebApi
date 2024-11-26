using Core.Models;

namespace Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(string id);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(string id, Product product);
        Task<List<Product>> GetAllProductsAsync();
        Task<bool> DeleteProductAsync(string id);
    }
}
