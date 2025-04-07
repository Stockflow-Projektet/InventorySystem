namespace Inventory.Core.Models.Abstracts;

public interface IOrderItem
{
    int ItemId { get; set; }

    int ProductId { get; set; }

    int Quantity { get; set; }

    int OrderId { get; set; }

    int DepotId { get; set; }
}