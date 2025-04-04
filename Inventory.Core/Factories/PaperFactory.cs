using Inventory.Core.Models;

namespace Inventory.Core.Factories;

public class PaperFactory : IProductFactory
{
    public ProductType ProductType { get; }
    public IProduct CreateProduct(string productType)
    {
        return new Paper { TypeId = productType };
    }
}