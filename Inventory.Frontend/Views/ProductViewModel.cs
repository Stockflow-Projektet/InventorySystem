using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Frontend.Views
{
    public class ProductViewModel : IValidatableObject
    {
        public long ProductId { get; set; }

        // Always required, no matter the product type:
        [Required(ErrorMessage = "Product Type is required (B, P, or W).")]
        [StringLength(1, ErrorMessage = "Type must be a single character: B, P, or W.")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [MinLength(2, ErrorMessage = "Name must have at least 2 characters.")]
        public string Name { get; set; }

        // If you truly want Description required for *every* product type, you can keep [Required].
        // But if it's only required for certain types, remove the attribute and check it in Validate().
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }

        // Using [Range] means the browser sees it must be > 0. If you want Price optional, switch to decimal? and remove [Range].
        [Range(0.01, 999999, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

        // Always required, used by your system (Active, etc.)
        [Required(ErrorMessage = "Status is required (default = A).")]
        public string Status { get; set; } = "A";

        // Book-specific fields (no [Required] here!)
        public string Author { get; set; }
        public string Publisher { get; set; }
        public int? PublicationYear { get; set; }
        public int? NumberOfPages { get; set; }

        // Paper-specific fields
        public string PaperSize { get; set; }
        public decimal? PaperWeight { get; set; }
        public string PaperColor { get; set; }
        public string CoatingType { get; set; }

        // Writing-specific fields
        public string InkColor { get; set; }
        public string InkType { get; set; }
        public decimal? TipSize { get; set; }
        public string PencilLeadHardness { get; set; }
        public bool IsErasable { get; set; }

        /// <summary>
        /// Implement IValidatableObject to handle *conditional* checks
        /// based on the product Type chosen.
        /// </summary>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // 1) Basic check: must be 'B', 'P', or 'W'
            if (Type != "B" && Type != "P" && Type != "W")
            {
                yield return new ValidationResult(
                    "Type must be 'B', 'P', or 'W'.",
                    new[] { nameof(Type) }
                );
            }

            // 2) If Type == "B" => require Book fields
            if (Type == "B")
            {
                if (string.IsNullOrWhiteSpace(Author))
                {
                    yield return new ValidationResult(
                        "Author is required for books.",
                        new[] { nameof(Author) }
                    );
                }
                if (!PublicationYear.HasValue)
                {
                    yield return new ValidationResult(
                        "PublicationYear is required for books.",
                        new[] { nameof(PublicationYear) }
                    );
                }
                // You can add more checks as needed...
            }

            // 3) If Type == "P" => require Paper fields
            if (Type == "P")
            {
                if (string.IsNullOrWhiteSpace(PaperSize))
                {
                    yield return new ValidationResult(
                        "PaperSize is required for paper.",
                        new[] { nameof(PaperSize) }
                    );
                }
                if (!PaperWeight.HasValue)
                {
                    yield return new ValidationResult(
                        "PaperWeight is required for paper.",
                        new[] { nameof(PaperWeight) }
                    );
                }
                // Add more checks if you need them
            }

            // 4) If Type == "W" => require Writing implements fields
            if (Type == "W")
            {
                if (string.IsNullOrWhiteSpace(InkColor))
                {
                    yield return new ValidationResult(
                        "InkColor is required for writing implements.",
                        new[] { nameof(InkColor) }
                    );
                }
                if (string.IsNullOrWhiteSpace(InkType))
                {
                    yield return new ValidationResult(
                        "InkType is required for writing implements.",
                        new[] { nameof(InkType) }
                    );
                }
                // etc.
            }
        }
    }
}
