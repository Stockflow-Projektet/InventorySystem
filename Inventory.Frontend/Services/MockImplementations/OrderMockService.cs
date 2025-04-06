using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.MockImplementations
{
    public class OrderMockService : IOrderService
    {
        private static readonly List<OrderViewModel> _orders = new List<OrderViewModel>
        {
            new OrderViewModel
            {
                OrderId = 1,
                OrderDate = DateTime.Now.AddDays(-2)
            },
            new OrderViewModel
            {
                OrderId = 2,
                OrderDate = DateTime.Now.AddDays(-1)
            },
        };

        public Task<IEnumerable<OrderViewModel>> GetOrdersAsync()
        {
            return Task.FromResult(_orders.AsEnumerable());
        }

        public Task<OrderViewModel> GetOrderByIdAsync(long orderId)
        {
            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            return Task.FromResult(order);
        }

        public Task CreateOrderAsync(OrderViewModel order)
        {
            order.OrderId = _orders.Max(o => o.OrderId) + 1;
            _orders.Add(order);
            return Task.CompletedTask;
        }
    }
}
