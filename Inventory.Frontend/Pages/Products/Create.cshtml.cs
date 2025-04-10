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
            Log.Verbose("Create Product OnGet called, displaying empty form.");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Log.Verbose("OnPostAsync started for Create Product.");

            if (!ModelState.IsValid)
            {
                // The user has missing/invalid fields
                Log.Warning("Create product form invalid. Will return the same page to show errors.");
                return Page();
            }

            // Log basic info about what user is trying to create
            Log.Information("User submitted a form to create a product: {@NewProduct}", NewProduct);

            try
            {
                Log.Debug("Calling ProductService.CreateProductAsync with the new product data...");
                await _productService.CreateProductAsync(NewProduct);

                Log.Debug("ProductService.CreateProductAsync completed successfully for product: {ProductName}",
                    NewProduct.Name);

                // If the API call succeeded
                Log.Information("Redirecting to Index page after successful product creation.");
                return RedirectToPage("Index");
            }
            catch (Exception ex)
            {
                // If the API call threw an exception or returned an error
                Log.Fatal(ex, "Fatal error while creating product: {ProductName}", NewProduct.Name);

                // Show a generic error message in the page
                ModelState.AddModelError(string.Empty,
                    "An error occurred creating this product. Please check logs or try again.");

                // Stay on the create page
                return Page();
            }
        }
    }
}
