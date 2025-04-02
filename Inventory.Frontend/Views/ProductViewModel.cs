namespace Inventory.Frontend.Views
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        // Etc. — only the fields you actually need on the frontend
    }
}
