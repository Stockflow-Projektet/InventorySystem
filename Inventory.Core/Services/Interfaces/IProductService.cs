using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Services.Interfaces;

public interface IProductService
{
    Task AddProduct(ProductCreationArgs productCreationArgs);
    Task<IEnumerable<Product>> GetProducts();
    Task<Product> GetProductById(int id);
    Task<IEnumerable<Product>> QueryProducts(string query);
    Task UpdateProduct(int id, ProductCreationArgs productCreationArgs);
    Task DeleteProduct(int id);
}