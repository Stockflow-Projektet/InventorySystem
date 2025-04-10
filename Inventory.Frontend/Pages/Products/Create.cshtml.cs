using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Serilog;

namespace Inventory.Frontend.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly IProductService _productService;

        [BindProperty]
        public ProductViewModel NewProduct { get; set; } = new();

        public CreateModel(IProductService productService)
        {
            _productService = productService;
        }

        public void OnGet()
        {
            // Display empty form on GET
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Return the same page if validation fails
                return Page();
            }

            Log.Information("User submitted a form to create a product with data: {@Product}", NewProduct);

            // Call the frontend service which posts to the API expecting ProductCreationArgs
            await _productService.CreateProductAsync(NewProduct);

            return RedirectToPage("Index");
        }
    }
}
