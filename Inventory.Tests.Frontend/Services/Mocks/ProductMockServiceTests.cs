using Xunit;
using System.Linq;
using System.Threading.Tasks;
using Inventory.Frontend.Services;
using Inventory.Frontend.Views;
using System.Collections.Generic;
using Inventory.Frontend.Services.MockImplementations;

namespace Inventory.Tests.Frontend.Services.Mocks
{
    public class ProductMockServiceTests
    {
        [Fact]
        public async Task GetProductsAsync_ReturnsAllProducts()
        {
            // Arrange
            var service = new ProductMockService();

            // Act
            var products = await service.GetProductsAsync();

            // Assert
            Assert.NotNull(products);
            Assert.True(products.Any());
        }

        [Fact]
        public async Task GetProductByIdAsync_ExistingId_ReturnsProduct()
        {
            // Arrange
            var service = new ProductMockService();
            long existingId = 1;

            // Act
            var product = await service.GetProductByIdAsync(existingId);

            // Assert
            Assert.NotNull(product);
            Assert.Equal(existingId, product.ProductId);
        }

        [Fact]
        public async Task GetProductByIdAsync_NonExistentId_ReturnsNull()
        {
            // Arrange
            var service = new ProductMockService();
            long nonExistentId = 999;

            // Act
            var product = await service.GetProductByIdAsync(nonExistentId);

            // Assert
            Assert.Null(product);
        }

        [Fact]
        public async Task CreateProductAsync_AddsProductWithIncrementedId()
        {
            // Arrange
            var service = new ProductMockService();
            var newProduct = new ProductViewModel
            {
                Type = "NEW",
                Name = "New Product",
                Manufacturer = "New Mfg",
                Description = "Description",
                Price = 10.00M,
                Amount = 5
            };

            // Act
            await service.CreateProductAsync(newProduct);

            // Assert
            var allProducts = await service.GetProductsAsync();
            Assert.Contains(allProducts, p => p.ProductId == newProduct.ProductId);
        }
    }
}
