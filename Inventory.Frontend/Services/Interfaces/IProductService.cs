using Inventory.Frontend.Models;

namespace Inventory.Frontend.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
        Task<bool> CreateProductAsync(ProductViewModel model);
        // Possibly more methods, e.g., for editing, deleting, etc.
    }
}
