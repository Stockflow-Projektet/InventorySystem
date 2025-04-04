using Inventory.Core.Models;
using Inventory.Core.Repositories;

namespace Inventory.Core;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository) => _repository = repository;

    public async Task<IEnumerable<Product>> GetProductsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Product> GetProductByIdAsync(int productId)
    {
        return await _repository.GetByIdAsync(productId);
    }

    public async Task<IEnumerable<Product>> QueryProductsAsync(string query)
    {
        return await _repository.QueryAsync(query);
    }

    public async Task<Product> AddProductAsync(Product product)
    {
        await _repository.AddAsync(product);
        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return null;  // Or throw an exception
        }

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;

        await _repository.UpdateAsync(existingProduct);
        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null)
        {
            return false;
        }

        await _repository.DeleteAsync(id);
        return true;
    }
}