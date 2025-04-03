using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<ProductViewModel> GetProductByIdAsync(long productId);
        Task CreateProductAsync(ProductViewModel product);
        // You might add more methods as needed, e.g. Update, Delete, etc.
    }
}
