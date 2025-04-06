using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Frontend.Services;
using Inventory.Frontend.Views;
using System.Collections.Generic;
using System;
using Inventory.Frontend.Services.MockImplementations;

namespace Inventory.Tests.Frontend.Services.Mocks
{
    public class OrderMockServiceTests
    {
        [Fact]
        public async Task GetOrdersAsync_ReturnsAllOrders()
        {
            // Arrange
            var service = new OrderMockService();

            // Act
            var orders = await service.GetOrdersAsync();

            // Assert
            Assert.NotNull(orders);
            Assert.True(orders.Any(), "Should return at least one order from mock data.");
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsCorrectOrder()
        {
            // Arrange
            var service = new OrderMockService();
            long existingId = 1;

            // Act
            var order = await service.GetOrderByIdAsync(existingId);

            // Assert
            Assert.NotNull(order);
            Assert.Equal(existingId, order.OrderId);
        }

        [Fact]
        public async Task GetOrderByIdAsync_NonExistentId_ReturnsNull()
        {
            // Arrange
            var service = new OrderMockService();
            long nonExistentId = 9999;

            // Act
            var order = await service.GetOrderByIdAsync(nonExistentId);

            // Assert
            Assert.Null(order);
        }

        [Fact]
        public async Task CreateOrderAsync_AddsNewOrderWithNextId()
        {
            // Arrange
            var service = new OrderMockService();
            var newOrder = new OrderViewModel
            {
                OrderDate = DateTime.Now
            };

            // Act
            await service.CreateOrderAsync(newOrder);

            // Assert
            // We expect the new order to have been assigned the next ID after the existing mocks
            var allOrders = await service.GetOrdersAsync();
            var maxId = allOrders.Max(o => o.OrderId);

            Assert.Equal(maxId, newOrder.OrderId);
            Assert.Contains(allOrders, o => o.OrderId == newOrder.OrderId);
        }
    }
}
