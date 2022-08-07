using Drinks.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drinks.Core.Interfaces
{
    public interface IDrinksService
    {
        Task<List<DrinkModel>> GetDrinksAsync();
    }
}
