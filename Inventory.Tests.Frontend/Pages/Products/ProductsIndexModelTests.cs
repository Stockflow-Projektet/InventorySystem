using Inventory.Frontend.Pages.Products;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Tests.Frontend.Pages.Products;

public class ProductsIndexModelTests
{
    [Fact]
    public async Task OnGetAsync_ShouldPopulateProductsProperty()
    {
        // Arrange
        var mockService = new Mock<IProductService>();
        var sampleProducts = new List<ProductViewModel>
        {
            new() { ProductId = 1, Name = "MockProduct1" },
            new() { ProductId = 2, Name = "MockProduct2" }
        };

        mockService.Setup(s => s.GetProductsAsync())
            .ReturnsAsync(sampleProducts);

        var indexModel = new IndexModel(mockService.Object);

        // Act
        await indexModel.OnGetAsync();

        // Assert
        Assert.NotNull(indexModel.Products);
        Assert.Equal(2, indexModel.Products.Count);
        Assert.Contains(indexModel.Products, p => p.Name == "MockProduct1");
    }
}