using Food.Core.Interfaces;
using Food.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Modules.Foods.Infrastructure.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class FoodController : ControllerBase
    {
        IFoodService _drinksClient;

        public FoodController(IFoodService drinksClient)
        {
            _drinksClient = drinksClient;
        }

        [HttpGet]
        public async Task<List<FoodModel>> GetFood()
        {
            return await _drinksClient.GetFoodAsync();
        }
    }
}
