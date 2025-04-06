namespace Inventory.Core.Models.Abstracts;

public interface IOrder
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int productIds { get; set; }
}