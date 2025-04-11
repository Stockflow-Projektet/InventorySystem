using Inventory.Core.DTO_s;
using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Factories.Interfaces;

public interface IOrderFactory
{
    IOrder CreateOrder(OrderDto dto);
}