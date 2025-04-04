using Inventory.Core.Models;

namespace Inventory.Core.Factories;

public interface IProductFactory
{
    IProduct CreateProduct(string productType);
}