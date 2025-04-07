using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Inventory.Frontend.Pages.Products;

public class CreateModel : PageModel
{
    private readonly IProductService _productService;

    public CreateModel(IProductService productService)
    {
        _productService = productService;
    }

    [BindProperty] public ProductViewModel NewProduct { get; set; } = new();

    public void OnGet()
    {
        // Just show the empty form
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
            // Return the same page if validation fails
            return Page();

        await _productService.CreateProductAsync(NewProduct);

        return RedirectToPage("Index");
    }
}