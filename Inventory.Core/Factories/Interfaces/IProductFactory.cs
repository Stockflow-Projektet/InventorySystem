using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Interfaces;

public interface IProductFactory
{
    Product CreateProduct(string productDto);
}