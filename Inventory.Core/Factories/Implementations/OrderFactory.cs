using Inventory.Core.DTO_s;
using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Models;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Implementations;

public class OrderFactory : IOrderFactory
{
    public IOrder CreateOrder(OrderDto dto)
    {
        // Create the domain model
        IOrder order = new Order
        {
            OrderDate = dto.OrderDate
            // You can also set other top-level properties if your domain supports them
        };

        // For each detail in dto, convert to an IOrderDetail
        foreach (var detailDto in dto.Details)
        {
            var detail = new OrderDetail
            {
                ProductId = detailDto.ProductId,
                Quantity = detailDto.Quantity,
                DepotId = detailDto.DepotId
            };
            order.OrderDetails.Add(detail);
        }

        return order;
    }

}