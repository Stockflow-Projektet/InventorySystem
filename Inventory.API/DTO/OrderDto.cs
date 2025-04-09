namespace Inventory.API.DTO_s;

public class OrderDto
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public int productIds { get; set; }
}