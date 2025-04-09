using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Interfaces;

public interface IProductFactory
{
    string FactoryType { get; } // Get the factory type for resolving factory selection,
    Product CreateProduct(ProductCreationArgs productCreationArgs);
    Product UpdateProduct(Product existingProduct, ProductCreationArgs productUpdateargs);
}