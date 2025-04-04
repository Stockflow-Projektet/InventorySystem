using Inventory.Core.Models;

namespace Inventory.Core;

public interface IProductService
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task<IEnumerable<Product>> QueryProductsAsync(string query);
        Task<Product> AddProductAsync(Product product);
        Task<Product> UpdateProductAsync(int id, Product product);
        Task<bool> DeleteProductAsync(int id);
    }
}