using System.ComponentModel.DataAnnotations;

namespace Inventory.Frontend.Views;

public class OrderViewModel
{
    public long OrderId { get; set; }

    [Required] public DateTime OrderDate { get; set; } = DateTime.Now;

    // Optional: If you want to include a list of order details
    public List<OrderDetailViewModel> Details { get; set; } = new();
}