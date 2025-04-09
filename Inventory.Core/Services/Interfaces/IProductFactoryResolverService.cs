using Inventory.Core.Factories.Interfaces;

namespace Inventory.Core.Services.Interfaces;

public interface IProductFactoryResolverService
{
    IProductFactory? GetFactory(string productType);
    
    void RefreshFactories();
}
