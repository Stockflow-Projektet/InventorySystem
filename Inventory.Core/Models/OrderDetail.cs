namespace Inventory.Core.Models;

public class OrderDetail : IOrderDetail
{
    public int DetailId { get; set; }
    public int ProductI { get; set; }
    public int Quantity { get; set; }
    public int OrderId { get; set; }
    public int DepotId { get; set; }
}