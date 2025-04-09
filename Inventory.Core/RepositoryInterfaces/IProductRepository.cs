using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.RepositoryInterfaces;

public interface IProductRepository
{
    Task AddProductToDb(Product product);
    Task<IEnumerable<Product>> GetAllProductsFromDb();
    Task<Product> GetProductByIdFromDb(int id);
    Task<IEnumerable<Product>> QueryProductsFromDb(string query);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}