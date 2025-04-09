using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models;

public class WritingTool : Product
{
    public int ProductId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string InkColor { get; set; }
    public string InkType { get; set; }
    public decimal TipSize { get; set; }
    public bool IsErasable { get; set; }
}