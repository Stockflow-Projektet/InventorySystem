using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.RepositoryInterfaces;

public interface IProductRepository
{
    void AddProductToDb(Product product);
    List<Product> GetAllProductsFromDb();
    Product GetProductByIdFromDb(int id);
    List<Product> QueryProductsFromDb(string query);
    void UpdateAsync(Product product);
    void DeleteAsync(int id);
}