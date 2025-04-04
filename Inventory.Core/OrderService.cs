using Inventory.Core.Models;

namespace Inventory.Core;

public class OrderService : IOrderService
{
    public IOrder StartOrder()
    {
        List<Product> Basket = new List<Product>();
    }

    public void PlaceOrder()
    {
        
    }

    public void DeleteOrder()
    {
        throw new NotImplementedException();
    }

    public void AddProductToBasket(int productId)
    {
        Product product = GetProductByIdAsync(int productId);
        list 
    }
}