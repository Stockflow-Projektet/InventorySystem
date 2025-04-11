namespace Inventory.Core.Models.Abstracts
{
    public interface IOrder
    {
        int OrderId { get; set; }
        DateTime OrderDate { get; set; }
        List<IOrderDetail> OrderDetails { get; set; }
    }
}