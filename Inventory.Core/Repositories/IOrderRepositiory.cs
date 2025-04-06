using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Repositories;

public interface IOrderRepository
{
    void AddOrderAsync(IOrder order);
    IOrder GetOrderById(string orderId);
    List<IOrder> GetOrdersAsync();
    void UpdateOrder(string orderId);
    void DeleteOrder(string orderId);
}