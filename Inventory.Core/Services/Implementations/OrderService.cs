using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IInventoryService _inventoryService;
    private readonly IOrderFactory _orderFactory;
    private readonly List<Product> _basket;

    public OrderService(IOrderFactory orderFactory, IInventoryService inventoryService)
    {
        _orderFactory = orderFactory;
        _inventoryService = inventoryService;
        _basket = new List<Product>();
    }

    public void AddProductToBasket(Product product)
    {
        if (_inventoryService.GetProductQuantity(product.GetProductId()) > 0)
            _basket.Add(product);
        else
            throw new ApplicationException("Product out of stock");
    }

    public void RemoveProductFromBasket(Product product)
    {
        if (_basket.Remove(product))
            return;
        
        throw new ApplicationException("Product not in basket");
    }

    public void PlaceOrder()
    {
        if (_basket.Count > 0)
        {
            IOrder order = _orderFactory.CreateOrder(_basket);
            SaveOrder(order);
            foreach (Orderitem item in order.)
        }
        else
        {
            throw new ApplicationException("Basket is empty");
        }
    }
}