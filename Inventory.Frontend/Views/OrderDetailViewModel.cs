using System.ComponentModel.DataAnnotations;

namespace Inventory.Frontend.Views
{
    public class OrderDetailViewModel
    {
        public int DetailId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Range(1, 999999)]
        public int Quantity { get; set; }

        public int DepotId { get; set; }
    }
}
