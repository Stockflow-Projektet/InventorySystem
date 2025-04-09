using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Core.RepositoryInterfaces
{
    public interface ITransferRepository
    {
        Task<int> TransferProductAsync(int productId, int sendingDepotId, int receivingDepotId, int quantity);
    }
}
