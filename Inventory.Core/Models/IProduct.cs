namespace Inventory.Core.Models;

public abstract class Product
{
    string TypeId {get; set;}
    string Name {get; set;}
    string Description {get; set;}
    string Price {get; set;}
}