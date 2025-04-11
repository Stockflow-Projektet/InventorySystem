using Inventory.Core.Models.Abstracts;
using System.Collections.Generic;

namespace Inventory.Core.Models
{
    public class Order : IOrder
    {
        public Order()
        {
            // Initialize the list so it's never null
            OrderDetails = new List<IOrderDetail>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<IOrderDetail> OrderDetails { get; set; }
    }
}
