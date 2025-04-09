using Inventory.Core.Models.Abstracts;

namespace Inventory.Core.Models
{
    public class Paper : Product
    {
        public string PaperSize { get; set; }
        public decimal? PaperWeight { get; set; }
        public string PaperColor { get; set; }
        public string CoatingType { get; set; }
    }
}

//public class Paper : Product
//{
//    public int ProductId { get; set; }
//    public string Type { get; set; }
//    public string Name { get; set; }
//    public string Description { get; set; }
//    public decimal Price { get; set; }
//    public string PaperSize { get; set; }
//    public decimal PaperWeight { get; set; }
//    public string CoatingType { get; set; }
//}