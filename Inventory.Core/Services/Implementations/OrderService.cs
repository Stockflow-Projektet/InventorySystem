using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly List<Product> _basket;
    private readonly IInventoryService _inventoryService;
    private readonly IOrderFactory _orderFactory;
    private readonly IOrderRepository _orderRepository;

    public OrderService(IOrderFactory orderFactory, IInventoryService inventoryService,
        IOrderRepository orderRepository)
    {
        _orderFactory = orderFactory;
        _inventoryService = inventoryService;
        _orderRepository = orderRepository;

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

    public void PlaceOrder(string orderDto)
    {
        if (_basket.Count > 0)
        {
            IOrder order = _orderFactory.CreateOrder(_basket);
            _orderRepository.AddOrderAsync(order);
        }
        else
        {
            throw new ApplicationException("Basket is empty");
        }
    }

    public void GetOrders()
    {
        throw new NotImplementedException();
    }


    public void GetOrderById(int orderId)
    {
        throw new NotImplementedException();
    }

    public void QueryOrders(string query)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrder(int orderId)
    {
        throw new NotImplementedException();
    }

    public void DeleteOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}


