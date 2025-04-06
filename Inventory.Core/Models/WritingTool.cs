using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models;

public class WritingTool : Product
{
    private int ProductId { get; set; }
    private string Type { get; set; }
    private string Name { get; set; }
    private string Description { get; set; }
    private decimal Price { get; set; }
    private string InkColor { get; set; }
    private string InkType { get; set; }
    private decimal TipSize { get; set; }
    private bool IsErasable { get; set; }
}