namespace Inventory.Core.Models.Abstracts;

public abstract class Product
{
    private int ProductId { get; set; }
    private string Type { get; set; }
    private string Name { get; set; }
    private string Description { get; set; }
    private string Price { get; set; }

    public int GetProductId()
    {
        return ProductId;
    }
}