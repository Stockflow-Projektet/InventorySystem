using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Serilog;

namespace Inventory.Frontend.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        [BindProperty]
        public OrderViewModel NewOrder { get; set; } = new();

        // Route parameter /Orders/Create?productId=123
        [BindProperty(SupportsGet = true)]
        public int? ProductId { get; set; }

        // Just so we can display the product name read-only if we came from a single product
        [BindProperty]
        public string? PreselectedProductName { get; set; }

        public CreateModel(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public async Task OnGetAsync()
        {
            if (ProductId.HasValue)
            {
                Log.Debug("User is creating an order from productId={ProductId}", ProductId);

                var product = await _productService.GetProductByIdAsync(ProductId.Value);
                if (product != null)
                {
                    // Add one detail line for that product, default quantity = 1
                    NewOrder.Details.Add(new OrderDetailViewModel
                    {
                        ProductId = product.ProductId,
                        Quantity = 1
                    });

                    // Default order date, just for convenience
                    NewOrder.OrderDate = DateTime.Today;

                    // So we can show a friendly label on the form
                    PreselectedProductName = product.Name;
                }
                else
                {
                    Log.Warning("No product found for productId={ProductId}", ProductId);
                }
            }
            else
            {
                Log.Debug("No preselected product; user can fill form from scratch.");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Log.Information("User submitted a form to create an order: {@Order}", NewOrder);

            // This will now send the entire OrderViewModel with .Details
            await _orderService.CreateOrderAsync(NewOrder);

            return RedirectToPage("Index");
        }

        // Optional: Let the user add more detail rows before creating
        public IActionResult OnPostAddDetail()
        {
            // Add a blank row so user can enter ProductId, etc.
            NewOrder.Details.Add(new OrderDetailViewModel { Quantity = 1 });
            return Page();
        }
    }
}
