namespace Inventory.Core.Models;

public class Paper : Product
{
    string Type {get; set;}
    string Name {get; set;}
    string Description {get; set;}
    Decimal Price {get; set;}
    string PaperSize {get; set;}
    decimal PaperWeight {get; set;}
    string CoatingType {get; set;}
    
}