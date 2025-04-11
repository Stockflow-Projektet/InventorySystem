using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.DTO;

public class ProductCreateDto : Product
{
    string Type { get; set; }
    int ProductId { get; set; }
    string Name { get; set; }
    string? Description { get; set; }
    decimal Price { get; set; }
    public int? Pages { get; set; }
    public string? Author { get; set; }
    public string? Publisher { get; set; }
    public int? PublicationYear { get; set; }
    private string? PaperSize { get; set; }
    private decimal? PaperWeight { get; set; }
    private string? CoatingType { get; set; }
    private string? InkColor { get; set; }
    private string? InkType { get; set; }
    private decimal? TipSize { get; set; }
    private bool IsErasable { get; set; }
}