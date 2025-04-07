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
            // Just show the empty form
        }
 
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Return the same page if validation fails
                return Page();
            }
 
            Log.Information("User submitted a form to create an Product with data: {@Product}", NewProduct);
 
            await _productService.CreateProductAsync(NewProduct);
 
            return RedirectToPage("Index");
        }
    }
}