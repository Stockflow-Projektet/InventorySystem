namespace Inventory.Core.Models.Abstracts
{
    public interface IOrder
    {
        int OrderId { get; set; }
        DateTime OrderDate { get; set; }
        List<IOrderDetail> OrderDetails { get; set; }
    }
}

//public interface IOrder
//{
//    public int OrderId { get; set; }
//    public DateTime OrderDate { get; set; }
//    public List<IOrderDetail> OrderItems { get; set; }
//}