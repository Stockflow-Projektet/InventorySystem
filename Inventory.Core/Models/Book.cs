using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models;

public class Book : Product
{
    string Type { get; set; }
    int ProductId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    public int Pages { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int PublicationYear { get; set; }
}