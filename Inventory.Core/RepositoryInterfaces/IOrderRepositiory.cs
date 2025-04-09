using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.RepositoryInterfaces
{
    public interface IOrderRepository
    {
        Task<int> AddOrderAsync(IOrder order);
        Task<IOrder> GetOrderByIdAsync(int orderId);
        Task<IEnumerable<IOrder>> GetAllOrdersAsync();
        Task DeleteOrderAsync(int orderId);
    }
}

//public interface IOrderRepository
//{
//    Task AddOrderAsync(IOrder order);
//    Task<IEnumerable<IOrder>> GetOrdersAsync();
//    Task<IOrder> GetOrderByIdAsync(int orderId);
//    Task<IEnumerable<IOrder>> QueryOrdersAsync(string query);
//    Task UpdateOrderAsync(IOrder order);
//    Task DeleteOrderAsync(int orderId);
//}