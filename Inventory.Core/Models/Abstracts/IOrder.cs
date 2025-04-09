namespace Inventory.Core.Models.Abstracts;

public interface IOrder
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<IOrderItem> OrderItems { get; set; }
}