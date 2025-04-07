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

        // Optional: so we can show product info or fill details
        [BindProperty(SupportsGet = true)]
        public long? ProductId { get; set; }

        public CreateModel(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }

        public async Task OnGetAsync()
        {
            if (ProductId.HasValue)
            {
                // The user clicked "Order" from a product card
                Log.Debug("User is creating an order from productId={ProductId}", ProductId);

                var product = await _productService.GetProductByIdAsync(ProductId.Value);
                if (product != null)
                {
                    // Option 1: We just store product info in an OrderDetail
                    // so the user can see which product they're ordering
                    var detail = new OrderDetailViewModel
                    {
                        ProductId = product.ProductId,
                        Quantity = 1 // default to 1 maybe
                        // DepotId = ??? if you have a default depot
                    };
                    NewOrder.Details.Add(detail);

                    // Maybe set a default date
                    NewOrder.OrderDate = DateTime.Now;
                }
                else
                {
                    Log.Warning("No product found for productId={ProductId} in order creation.", ProductId);
                }
            }
            else
            {
                // Normal scenario: user just visits /Orders/Create
                Log.Debug("User is creating a new order with no preselected product.");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Log the data the user submitted
            Log.Information("User submitted a form to create an order with data: {@Order}", NewOrder);

            // Call the service to send data to the API
            await _orderService.CreateOrderAsync(NewOrder);

            return RedirectToPage("Index");
        }
    }
}
