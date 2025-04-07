using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventory.Frontend.Pages.Products;

public class IndexModel : PageModel
{
    private readonly IProductService _productService;

    public IndexModel(IProductService productService)
    {
        _productService = productService;
    }

    public List<ProductViewModel> Products { get; set; } = new();

    public async Task OnGetAsync()
    {
        var products = await _productService.GetProductsAsync();
        Products = products.ToList();
    }
}