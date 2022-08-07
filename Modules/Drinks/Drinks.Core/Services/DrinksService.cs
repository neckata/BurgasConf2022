using Drinks.Core.Interfaces;
using Drinks.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drinks.Core.Services
{
    public class DrinksService : IDrinksService
    {
        public async Task<List<DrinkModel>> GetDrinksAsync()
        {
            List<DrinkModel> items = new List<DrinkModel>
            {
                new DrinkModel
                {
                    Name = "CocaCola",
                    Price = 1
                },
                new DrinkModel
                {
                    Name = "Fanta",
                    Price = 2
                }
            };

            return items;
        }
    }
}
