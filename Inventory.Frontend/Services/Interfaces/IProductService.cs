using System.Collections.Generic;
using System.Threading.Tasks;
using Inventory.Frontend.Views;
 
namespace Inventory.Frontend.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetProductsAsync();
        Task<IEnumerable<ProductViewModel>> GetProductsByTypeAsync(string productType);
        Task<ProductViewModel> GetProductByIdAsync(int productId);
        Task CreateProductAsync(ProductViewModel product);
        // Add more if needed, e.g. Update, Delete, etc.
    }
}