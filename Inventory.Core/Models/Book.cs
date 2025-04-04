namespace Inventory.Core.Models;

public class Book : Product
{
    public string Type = "Book";
    public string Name { get; set; }
    public string Description { get; set; }
    public Decimal Price { get; set; }
    public int Pages { get; set; }
    public string Author { get; set; }
    public string Publisher { get; set; }
    public int PublicationYear { get; set; }
}