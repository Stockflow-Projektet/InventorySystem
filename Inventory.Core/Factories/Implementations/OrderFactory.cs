using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class OrderFactory : IOrderFactory
{
    public IOrder CreateOrder(List<Product> cart)
    {
        IOrder order = new Order();

        order.OrderDate = DateTime.Now;
        foreach (Product product in cart)
        {
            var orderItem = new OrderDetail();
            orderItem.ProductId = product.GetProductId();
        }
        return order;
    }
}