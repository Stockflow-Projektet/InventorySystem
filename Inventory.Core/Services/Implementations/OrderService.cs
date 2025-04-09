using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;
using Inventory.Core.RepositoryInterfaces;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly List<Product> _basket;
        private readonly IInventoryService _inventoryService;
        private readonly IOrderFactory _orderFactory;
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderFactory orderFactory,
                            IInventoryService inventoryService,
                            IOrderRepository orderRepository)
        {
            _orderFactory = orderFactory;
            _inventoryService = inventoryService;
            _orderRepository = orderRepository;
            _basket = new List<Product>();
        }

        public void AddProductToBasket(Product product)
        {
            int currentQty = _inventoryService.GetProductQuantity(product.GetProductId());
            if (currentQty > 0)
                _basket.Add(product);
            else
                throw new ApplicationException("Product out of stock");
        }

        public void RemoveProductFromBasket(Product product)
        {
            if (!_basket.Remove(product))
                throw new ApplicationException("Product not in basket");
        }

        public async Task PlaceOrder(int placeholderId)
        {
            // The 'placeholderId' parameter here is just an example 
            // from your minimal API route. In practice you'd pass in an order detail object.

            if (_basket.Count == 0)
                throw new ApplicationException("Basket is empty");

            IOrder order = _orderFactory.CreateOrder(_basket);
            await _orderRepository.AddOrderAsync(order);

            // Could also reduce inventory, etc.
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return orders.Cast<Order>();  // or map them
        }

        public async Task<Order> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            return (Order)order; // if needed
        }

        public async Task<IEnumerable<Order>> QueryOrders(string query)
        {
            // You don't have a 'QueryOrdersFromDb' yet, so you'd add it to IOrderRepository
            throw new NotImplementedException();
        }

        public async Task UpdateOrder(int orderId)
        {
            // Implement logic to update an order
            throw new NotImplementedException();
        }

        public async Task DeleteOrder(int orderId)
        {
            await _orderRepository.DeleteOrderAsync(orderId);
        }
    }
}
