using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class OrderFactory : IOrderFactory
{
    public IOrder CreateOrder(List<Product> basket);
    {
        IOrder order = new Order();

        order.OrderDate = DateTime.Now;
        foreach (Product product in basket)
        {
            product
            var orderItem = new OrderItem();
            orderItem.ProductId = product.GetProductId();
        }
    }
}