using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<IOrderDetail> OrderDetails { get; set; }
    }
}

