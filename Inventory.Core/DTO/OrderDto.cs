namespace Inventory.Core.DTO_s
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }

        // If you only have one productId, keep 'productIds' as is, or rename it.
        // But for multiple details, create a list of detail DTOs:
        public List<OrderDetailDto> Details { get; set; } = new();
    }

    public class OrderDetailDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int DepotId { get; set; }
    }
}
