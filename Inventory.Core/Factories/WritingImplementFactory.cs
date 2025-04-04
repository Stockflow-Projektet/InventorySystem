using Inventory.Core.Models;

namespace Inventory.Core.Factories;

public class WritingImplementFactory : IProductFactory
{
    public IProduct CreateProduct(string productType)
    {
        return new WritingImplement() { TypeId = productType };
    }
}