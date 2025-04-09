namespace Inventory.Core.Models.Abstracts;

public abstract class Product
{
    public int ProductId { get; set; }
    public string Type { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public int GetProductId()
    {
        return ProductId;
    }
}