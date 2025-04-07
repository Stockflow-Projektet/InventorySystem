using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models;

public class Book : Product
{
    private string Type { get; set; }
    private int ProductId { get; set; }
    private string Name { get; set; }
    private string Description { get; set; }
    private decimal Price { get; set; }
    private int Pages { get; set; }
    private string Author { get; set; }
    private string Publisher { get; set; }
    private int PublicationYear { get; set; }
}