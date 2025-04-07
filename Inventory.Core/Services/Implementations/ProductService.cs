using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductFactory _productFactory;
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository, IProductFactory productFactory)
    {
        _repository = repository;
        _productFactory = productFactory;
    }

    public void AddProduct(string productDto)
    {
        var product = _productFactory.CreateProduct(productDto);
        _repository.AddProductToDb(product);
    }

    public List<Product> GetProducts()
    {
        return _repository.GetAllProductsFromDb();
    }

    public Product GetProductById(int productId)
    {
        return _repository.GetProductByIdFromDb(productId);
    }

    public List<Product> QueryProducts(string query)
    {
        return _repository.QueryProductsFromDb(query);
    }
    public void UpdateProduct(int id)
    {
       throw new NotImplementedException();
    }

    public void DeleteProduct(int id)
    {
        throw new NotImplementedException();
    }
}