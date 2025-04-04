using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Inventory.Frontend.Pages.Orders;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Tests.Frontend.Pages.Orders
{
    public class OrdersIndexModelTests
    {
        [Fact]
        public async Task OnGetAsync_ShouldPopulateOrdersProperty()
        {
            // Arrange
            var mockService = new Mock<IOrderService>();
            var sampleOrders = new List<OrderViewModel>
            {
                new OrderViewModel { OrderId = 1 },
                new OrderViewModel { OrderId = 2 }
            };

            mockService.Setup(s => s.GetOrdersAsync())
                       .ReturnsAsync(sampleOrders);

            var indexModel = new IndexModel(mockService.Object);

            // Act
            await indexModel.OnGetAsync();

            // Assert
            Assert.NotNull(indexModel.Orders);
            Assert.Equal(2, indexModel.Orders.Count);
            Assert.Contains(indexModel.Orders, o => o.OrderId == 1);
        }
    }
}
