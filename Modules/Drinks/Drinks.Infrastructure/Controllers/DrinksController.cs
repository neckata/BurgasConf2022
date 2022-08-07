using Drinks.Core.Interfaces;
using Drinks.Core.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Modules.Drinks.Infrastructure.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class DrinksController : ControllerBase
    {
        private readonly IDrinksService _drinksClient;

        public DrinksController(IDrinksService drinksClient)
        {
            _drinksClient = drinksClient;
        }

        [HttpGet]
        public async Task<List<DrinkModel>> GetDrinks()
        {
            return await _drinksClient.GetDrinksAsync();
        }
    }
}
