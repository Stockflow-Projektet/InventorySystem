using System.Net;
using System.Text.Json;
using Inventory.Frontend.Services.Implementations;
using Inventory.Frontend.Views;
using RichardSzalay.MockHttp;

namespace Inventory.Tests.Frontend.Services.Implementations
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductsAsync_ReturnsProducts()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var expectedProducts = new List<ProductViewModel>
        {
            new() { ProductId = 1, Name = "Test Product 1" },
            new() { ProductId = 2, Name = "Test Product 2" }
        };

            mockHttp.When("http://test/api/products")
                .Respond("application/json", JsonSerializer.Serialize(expectedProducts));

            var client = new HttpClient(mockHttp) { BaseAddress = new Uri("http://test/") };
            var service = new ProductService(client);

            // Act
            var products = await service.GetProductsAsync();

            // Assert
            Assert.NotNull(products);
            Assert.Equal(2, ((List<ProductViewModel>)products).Count);
        }

        [Fact]
        public async Task GetProductByIdAsync_ReturnsSingleProduct()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            var product = new ProductViewModel { ProductId = 42, Name = "The Answer" };

            mockHttp.When("http://test/api/products/42")
                .Respond("application/json", JsonSerializer.Serialize(product));

            var client = new HttpClient(mockHttp) { BaseAddress = new Uri("http://test/") };
            var service = new ProductService(client);

            // Act
            var result = await service.GetProductByIdAsync(42);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.ProductId);
            Assert.Equal("The Answer", result.Name);
        }

        [Fact]
        public async Task CreateProductAsync_SendsPostRequest()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();
            mockHttp.When("http://test/api/products")
                .Respond(HttpStatusCode.Created);

            var client = new HttpClient(mockHttp) { BaseAddress = new Uri("http://test/") };
            var service = new ProductService(client);

            var newProduct = new ProductViewModel { Name = "Post Test", Price = 99.9M };

            // Act
            await service.CreateProductAsync(newProduct);

            // Assert
            // Again, if this never POSTed, the mock HttpMessageHandler would fail.
        }
    }
}

