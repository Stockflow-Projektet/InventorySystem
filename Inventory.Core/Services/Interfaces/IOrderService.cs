using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Services.Interfaces;

public interface IOrderService
{
    void AddProductToBasket(Product product);
    void RemoveProductFromBasket(Product product);
    void PlaceOrder();
}