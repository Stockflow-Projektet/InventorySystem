using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Inventory.Frontend.Views
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        // A list of order details, to match the new OrderDto
        public List<OrderDetailViewModel> Details { get; set; } = new();
    }
}
