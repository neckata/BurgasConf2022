using Food.Core.Interfaces;
using Food.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Food.Core.Services
{
    public class FoodClient : IFoodClient
    {
        public async Task<List<FoodModel>> GetItemsAsync()
        {
            List<FoodModel> items = new List<FoodModel>
            {
                new FoodModel
                {
                    Name = "Tomato",
                    Price = 2
                },
                new FoodModel
                {
                    Name = "Banana",
                    Price = 1
                }
            };

            return items;
        }
    }
}
