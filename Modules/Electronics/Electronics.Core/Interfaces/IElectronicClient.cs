using Electronics.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Electronics.Core.Interfaces
{
    public interface IElectronicClient
    {
        Task<List<ElectronicModel>> GetItemsAsync();
    }
}
