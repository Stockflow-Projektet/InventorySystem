using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models;

public class Paper : Product
{
    private int ProductId { get; set; }
    private string Type { get; set; }
    private string Name { get; set; }
    private string Description { get; set; }
    private decimal Price { get; set; }
    private string PaperSize { get; set; }
    private decimal PaperWeight { get; set; }
    private string CoatingType { get; set; }
}