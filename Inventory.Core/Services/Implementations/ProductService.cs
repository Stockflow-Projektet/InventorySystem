using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductFactoryResolverService _productFactoryResolverService;
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository, IProductFactoryResolverService productFactoryResolverService)
    {
        _repository = repository;
        _productFactoryResolverService = productFactoryResolverService;
    }

    public async Task AddProduct(ProductCreationArgs productCreationArgs)
    {
        var productFactory = _productFactoryResolverService.GetFactory(productCreationArgs.Type);
        if (productFactory == null)
        {
            throw new Exception($"No factory found for product type: {productCreationArgs.Type}");
        }

        var product = productFactory.CreateProduct(productCreationArgs);
       await _repository.AddProductToDb(product);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _repository.GetAllProductsFromDb();
    }

    public async Task<Product> GetProductById(int productId)
    {
        return await _repository.GetProductByIdFromDb(productId);
    }

    public async Task<IEnumerable<Product>> QueryProducts(string query)
    {
        return await _repository.QueryProductsFromDb(query);
    }
    public async Task UpdateProduct(int id, ProductCreationArgs productCreationArgs)
    {
        Product existingProduct = await _repository.GetProductByIdFromDb(id);
        if (existingProduct.Type != productCreationArgs.Type)
        {
            throw new Exception("Cannot change product type");
        }
        
        var productFactory = _productFactoryResolverService.GetFactory(productCreationArgs.Type);
        if (productFactory == null)
        {
            throw new Exception($"No factory found for product type: {productCreationArgs.Type}");
        }

        var product = productFactory.UpdateProduct(existingProduct, productCreationArgs);
        await _repository.UpdateAsync(product);
    }

    public async Task DeleteProduct(int id)
    {
        await _repository.DeleteAsync(id);
    }
}