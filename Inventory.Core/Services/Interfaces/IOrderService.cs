using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.DTO_s;

namespace Inventory.Core.Services.Interfaces;

public interface IOrderService
{
    Task<int> PlaceOrder(OrderDto dto);
    Task<IEnumerable<Order>> GetOrders();
    Task<Order> GetOrderById(int orderId);
    Task<IEnumerable<Order>> QueryOrders(string query);
    Task UpdateOrder(int orderId);
    Task DeleteOrder(int orderId);
    void AddProductToBasket(Product product);
    void RemoveProductFromBasket(Product product);
}