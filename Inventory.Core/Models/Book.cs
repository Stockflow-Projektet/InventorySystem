namespace Inventory.Core.Models;

public class Book : Product
{
    public string TypeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Price { get; set; }
    public int Pages { get; set; }
    public string Author { get; set; }
    public int Published { get; set; }
}