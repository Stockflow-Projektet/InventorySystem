using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetOrdersAsync();
        Task<OrderViewModel> GetOrderByIdAsync(long orderId);
        Task CreateOrderAsync(OrderViewModel order);
        // Additional methods: Update, Delete, etc.
    }
}
