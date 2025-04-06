using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.MockImplementations
{
    public class ProductMockService : IProductService
    {
        private static readonly List<ProductViewModel> _products = new List<ProductViewModel>
        {
            new ProductViewModel
            {
                ProductId = 1,
                Type = "ELE",
                Name = "Widget A",
                Manufacturer = "Acme Inc.",
                Description = "Sample electronic widget",
                Price = 49.99M,
                Amount = 100
            },
            new ProductViewModel
            {
                ProductId = 2,
                Type = "TOY",
                Name = "Gadget B",
                Manufacturer = "Globex",
                Description = "Fun gadget for kids",
                Price = 19.95M,
                Amount = 50
            }
        };

        public Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<ProductViewModel> GetProductByIdAsync(long productId)
        {
            var product = _products.FirstOrDefault(p => p.ProductId == productId);
            return Task.FromResult(product);
        }

        public Task CreateProductAsync(ProductViewModel product)
        {
            product.ProductId = _products.Max(p => p.ProductId) + 1;
            _products.Add(product);
            return Task.CompletedTask;
        }
    }
}
