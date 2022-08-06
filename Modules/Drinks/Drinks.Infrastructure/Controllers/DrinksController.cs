using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.Modules.Drinks.Infrastructure.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class DrinksController : ControllerBase
    {
        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}
