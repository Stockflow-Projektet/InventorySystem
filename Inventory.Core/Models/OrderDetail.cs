using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models
{
    public class OrderDetail : IOrderDetail
    {
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int OrderId { get; set; }
        public int DepotId { get; set; }
    }
}

