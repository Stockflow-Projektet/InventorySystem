using System.ComponentModel.DataAnnotations;

namespace Inventory.Frontend.Views;

public class OrderDetailViewModel
{
    public long DetailId { get; set; }

    [Required] public long ProductId { get; set; }

    [Range(1, 999999)] public long Quantity { get; set; }

    public long DepotId { get; set; }
}