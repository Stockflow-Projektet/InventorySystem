using System.ComponentModel.DataAnnotations;

namespace Inventory.Frontend.Views;

public class ProductViewModel
{
    public long ProductId { get; set; }

    [Required] [StringLength(3)] public string Type { get; set; } // E.g. "ELE", "FUR", "TOY", etc.

    [Required] public string Name { get; set; }

    public string Manufacturer { get; set; }

    public string Description { get; set; }

    [Range(0, 999999)] public decimal Price { get; set; }

    [Range(0, 999999)] public long Amount { get; set; }
}