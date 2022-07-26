using Drinks.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drinks.Core.Interfaces
{
    public interface IDrinksClient
    {
        Task<List<DrinkModel>> GetItemsAsync();
    }
}
