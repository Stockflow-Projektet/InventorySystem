using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Serilog;
 
namespace Inventory.Frontend.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly IProductService _productService;
 
        public IndexModel(IProductService productService)
        {
            _productService = productService;
        }
 
        public List<ProductViewModel> Products { get; set; } = new();
 
        public async Task OnGetAsync(string productType = null)
        {
            if (!string.IsNullOrWhiteSpace(productType))
            {
                Log.Information("Products Index: fetching products filtered by type '{ProductType}'", productType);
                var filtered = await _productService.GetProductsByTypeAsync(productType);
                Products = filtered.ToList();
            }
            else
            {
                Log.Information("Products Index: fetching all products (no type filter).");
                var all = await _productService.GetProductsAsync();
                Products = all.ToList();
            }
        }
    }
}