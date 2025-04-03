using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly IOrderService _orderService;

        [BindProperty]
        public OrderViewModel NewOrder { get; set; } = new();

        public CreateModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public void OnGet()
        {
            // Display empty form by default
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _orderService.CreateOrderAsync(NewOrder);
            return RedirectToPage("Index");
        }
    }
}
