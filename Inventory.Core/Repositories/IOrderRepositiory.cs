using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Repositories;

public interface IOrderRepository
{
    Task AddOrderAsync(IOrder order);
    Task<IEnumerable<IOrder>> GetOrdersAsync();
    Task<IOrder> GetOrderByIdAsync(int orderId);
    Task<IEnumerable<IOrder>> QueryOrdersAsync(string query);
    Task UpdateOrderAsync(IOrder order);
    Task DeleteOrderAsync(int orderId);
}