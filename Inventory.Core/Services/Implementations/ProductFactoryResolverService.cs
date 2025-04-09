using Inventory.Core.Factories.Interfaces;
using Inventory.Core.Services.Interfaces;

namespace Inventory.Core.Services.Implementations;

public class ProductFactoryResolverService : IProductFactoryResolverService
{
    private Dictionary<string, IProductFactory> _factories;

    public ProductFactoryResolverService()
    {
        _factories = DiscoverFactories();
    }

    private static Dictionary<string, IProductFactory> DiscoverFactories()
    {
        var factoryTypes = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IProductFactory).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

        var factories = new Dictionary<string, IProductFactory>(StringComparer.OrdinalIgnoreCase);

        foreach (var type in factoryTypes)
        {
            var factoryInstance = (IProductFactory)Activator.CreateInstance(type)!;
            factories[factoryInstance.FactoryType] = factoryInstance;
        }

        return factories;
    }

    public IProductFactory? GetFactory(string productType)
    {
        return _factories.TryGetValue(productType, out var factory) ? factory : null;
    }

    public void RefreshFactories()
    {
        _factories = DiscoverFactories();
    }
}






