namespace Inventory.Core.Models.Abstracts
{
    public abstract class Product
    {
        public int ProductId { get; set; }

        /// <summary>
        /// For the DB, we store 'B', 'P', 'W', etc.  
        /// Book = BOK, Paper = PAP, WritingImplements = WRI, etc.
        /// </summary>
        public string Type { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// For example, "ACME Inc." or "Random House" etc.
        /// </summary>
        public string Manufacturer { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int? Amount { get; set; }

        public string Status { get; set; }

        public int GetProductId()
        {
            return ProductId;
        }
    }
}
