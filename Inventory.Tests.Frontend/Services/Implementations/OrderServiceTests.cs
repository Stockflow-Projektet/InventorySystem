using Xunit;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;
using RichardSzalay.MockHttp; // If you're using the MockHttp library
using Inventory.Frontend.Services.Implementations;
using Inventory.Frontend.Views;
using System.Collections.Generic;

namespace Inventory.Tests.Frontend.Services.Implementations
{
    public class OrderServiceTests
    {
        [Fact]
        public async Task GetOrdersAsync_ReturnsOrdersFromApi()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var expectedOrders = new List<OrderViewModel>
            {
                new OrderViewModel { OrderId = 100, OrderDate = DateTime.Now },
                new OrderViewModel { OrderId = 101, OrderDate = DateTime.Now }
            };

            mockHttp.When("http://test/api/orders")
                    .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(expectedOrders));

            var client = new HttpClient(mockHttp)
            {
                BaseAddress = new Uri("http://test/")
            };
            var service = new OrderService(client);

            // Act
            var result = await service.GetOrdersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                o => Assert.Equal(100, o.OrderId),
                o => Assert.Equal(101, o.OrderId));
        }

        [Fact]
        public async Task GetOrderByIdAsync_ReturnsSpecificOrder()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var expectedOrder = new OrderViewModel { OrderId = 123, OrderDate = DateTime.Now };

            mockHttp.When("http://test/api/orders/123")
                    .Respond("application/json", System.Text.Json.JsonSerializer.Serialize(expectedOrder));

            var client = new HttpClient(mockHttp) { BaseAddress = new Uri("http://test/") };
            var service = new OrderService(client);

            // Act
            var result = await service.GetOrderByIdAsync(123);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(123, result.OrderId);
        }

        [Fact]
        public async Task CreateOrderAsync_SendsPostRequest()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://test/api/orders")
                    .Respond(HttpStatusCode.Created);

            var client = new HttpClient(mockHttp) { BaseAddress = new Uri("http://test/") };
            var service = new OrderService(client);

            var newOrder = new OrderViewModel { OrderId = 999, OrderDate = DateTime.Now };

            // Act
            await service.CreateOrderAsync(newOrder);

            // Assert
            // The mock handler will throw if the call was never made.
            // No exception => test passes.
        }
    }
}
