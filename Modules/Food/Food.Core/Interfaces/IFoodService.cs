using Food.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Interfaces
{
    public interface IFoodService
    {
        Task<List<FoodModel>> GetFoodAsync();
    }
}
