using Microsoft.AspNetCore.Mvc.RazorPages;
using Inventory.Frontend.Services.Interfaces;
using Inventory.Frontend.Views;

namespace Inventory.Frontend.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly IOrderService _orderService;

        public IndexModel(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public List<OrderViewModel> Orders { get; set; } = new();

        public async Task OnGetAsync()
        {
            var orders = await _orderService.GetOrdersAsync();
            Orders = orders.ToList();
        }
    }
}
