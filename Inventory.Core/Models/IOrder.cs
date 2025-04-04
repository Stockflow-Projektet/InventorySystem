namespace Inventory.Core.Models;

public interface IOrderDetail
{
    int DetailId { get; set; }

    int ProductI { get; set; }

    int Quantity { get; set; }

    int OrderId { get; set; }

    int DepotId { get; set; }
}