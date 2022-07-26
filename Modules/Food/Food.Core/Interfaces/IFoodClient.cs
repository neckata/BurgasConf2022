using Food.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Interfaces
{
    public interface IFoodClient
    {
        Task<List<FoodModel>> GetItemsAsync();
    }
}
