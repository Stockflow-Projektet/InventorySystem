using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
        Task<bool> CreateOrderAsync(OrderViewModel model);
        // Possibly more methods for retrieving an order by ID, etc.
    }
}
