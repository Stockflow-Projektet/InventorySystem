using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.RepositoryInterfaces
{
    public interface IDepotRepository
    {
        Task<int> AddDepotAsync(string depotName);
        Task<IEnumerable<(int depotId, string name)>> GetAllDepotsAsync();
        // etc.
    }
}
