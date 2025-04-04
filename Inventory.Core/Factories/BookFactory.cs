using Inventory.Core.Models;

namespace Inventory.Core.Factories;

public class PhysicalOrderFactory : IOrderFactory
{
    
    public IProduct CreateProduct(string productType)
    {
        return new Book { TypeId = productType };
    }
}