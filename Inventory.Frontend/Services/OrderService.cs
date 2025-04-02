using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
        {
            return new List<OrderViewModel>();
        }

        public async Task<bool> CreateOrderAsync(OrderViewModel model)
        {
            return true;
        }
    }
}
