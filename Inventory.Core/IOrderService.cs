namespace Inventory.Core;

public interface IOrderService
{
    void StartOrder();
    void PlaceOrder();
    
    void DeleteOrder();
    
    void AddProductToBasket(int productId);

}
