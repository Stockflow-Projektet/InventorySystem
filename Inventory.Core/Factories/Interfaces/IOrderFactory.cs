using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Interfaces;

public interface IOrderFactory
{
    IOrder CreateOrder(List<Product> products);
    
}