using System.ComponentModel.DataAnnotations;
 
namespace Inventory.Frontend.Views
{
    public class ProductViewModel
    {
        public long ProductId { get; set; }
 
        [Required]
        [StringLength(3)]
        public string Type { get; set; }  // e.g. "BOOK", "WRI", "PAP"
 
        [Required]
        public string Name { get; set; }
 
        public string Manufacturer { get; set; }
        public string Description { get; set; }
 
        [Range(0, 999999)]
        public decimal Price { get; set; }
 
        [Range(0, 999999)]
        public long Amount { get; set; }
 
        [Required]
        [StringLength(1)]
        public string Status { get; set; } = "A"; // 'A' for active, e.g.
 
        // Book-specific
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string ISBN { get; set; }
        public int? PublicationYear { get; set; }
        public int? NumberOfPages { get; set; }
 
        // Writing-implements-specific
        public string InkColor { get; set; }
        public string InkType { get; set; }
        public decimal? TipSize { get; set; }
        public string PencilLeadHardness { get; set; }
        public bool? IsErasable { get; set; }
 
        // Paper-specific
        public string PaperSize { get; set; }
        public int? PaperWeight { get; set; }
        public string PaperColor { get; set; }
        public string CoatingType { get; set; }
    }
}