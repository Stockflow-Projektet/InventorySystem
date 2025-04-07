using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Services.Interfaces;

public interface IProductService
{
    void AddProduct(string productDto);
    List<Product> GetProducts();
    Product GetProductById(int id);
    List<Product> QueryProducts(string query);
    void UpdateProduct(int id);
    void DeleteProduct(int id);
}