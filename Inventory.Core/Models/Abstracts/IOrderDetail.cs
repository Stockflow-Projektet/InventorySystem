namespace Inventory.Core.Models.Abstracts
{
    public interface IOrderDetail
    {
        int ItemId { get; set; }
        int ProductId { get; set; }
        int Quantity { get; set; }
        int OrderId { get; set; }
        int DepotId { get; set; }
    }
}