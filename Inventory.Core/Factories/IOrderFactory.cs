using Inventory.Core.Models;

namespace Inventory.Core.Factories;

public interface IOrderFactory
{
    void PlaceOrder();
}

public class OrderFactory : IOrderFactory
{
    List<IOrderDetail> orderDetails = new List<IOrderDetail>();
    void PlaceOrder();
    {
        Foreach
    }
}