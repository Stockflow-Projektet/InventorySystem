namespace Inventory.Core;

public class ProductCreationArgs
{
    public string Type { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Pages { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int PublicationYear { get; set; }
    public string PaperSize { get; set; }
    public decimal PaperWeight { get; set; }
    public string CoatingType { get; set; }
    public string InkColor { get; set; }
    public string InkType { get; set; }
    public decimal TipSize { get; set; }
    public bool IsErasable { get; set; }
    public string Status { get; set; }
}