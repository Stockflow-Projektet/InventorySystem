using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.Repositories;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IProductFactory _productFactory;

    public ProductService(IProductRepository repository, IProductFactory productFactory)
    {
        _repository = repository;
        _productFactory = productFactory;
    }

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

    public async Task<Product> AddProductAsync(string productType)
    {
        Product product = _productFactory.CreateProduct();
        await _repository.AddAsync(product);
        return product;
    }

    public async Task<Product> UpdateProductAsync(int id, Product product)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null) return null; // Or throw an exception

        existingProduct.Name = product.Name;
        existingProduct.Description = product.Description;
        existingProduct.Price = product.Price;

        await _repository.UpdateAsync(existingProduct);
        return existingProduct;
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var existingProduct = await _repository.GetByIdAsync(id);
        if (existingProduct == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}