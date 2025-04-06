using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models;

public class Book : Product
{
    string Type { get; set; }
    int ProductId { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    decimal Price { get; set; }
    int Pages { get; set; }
    string Author { get; set; }
    string Publisher { get; set; }
    int PublicationYear { get; set; }
}