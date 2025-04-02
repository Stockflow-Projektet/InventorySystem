using Inventory.Frontend.Models;
using Inventory.Frontend.Services.Interfaces;

namespace Inventory.Frontend.Services.Implementations
{
    public class ProductService : IProductService
    {
        public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
        {
            // Stub: Return empty list or mock data
            return new List<ProductViewModel>();
        }

        public async Task<bool> CreateProductAsync(ProductViewModel model)
        {
            // Stub: Return true but do no real work yet
            return true;
        }
    }
}
