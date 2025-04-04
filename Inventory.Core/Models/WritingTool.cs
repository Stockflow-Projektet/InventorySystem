namespace Inventory.Core.Models;

public class WritingTool : Product
{
    string Type {get; set;}
    string Name {get; set;}
    string Description {get; set;}
    Decimal Price {get; set;}
    string InkColor {get; set;}
    string InkType {get; set;}
    decimal TipSize {get; set;}
    bool IsErasable {get; set;}
}