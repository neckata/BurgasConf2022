using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class ModulesController : ControllerBase
    {
        [HttpGet]
        public bool Get()
        {
            return true;
        }
    }
}
