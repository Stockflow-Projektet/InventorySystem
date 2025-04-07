using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Services.Interfaces;

public interface IOrderService
{
    void PlaceOrder(string orderDto);
    void GetOrders();
    void GetOrderById(int orderId);
    void QueryOrders(string query);
    void UpdateOrder(int orderId);
    void DeleteOrder(int orderId);
    void AddProductToBasket(Product product);
    void RemoveProductFromBasket(Product product);
}