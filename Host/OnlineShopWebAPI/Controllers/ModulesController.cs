using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.Core.Wrapper;
using OnlineShop.Shared.DTOs.Module;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class ModulesController : ControllerBase
    {
        private IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<IResult<List<ModuleResponse>>> GetModules()
        {
            return await _moduleService.GetAllModulesAsync();
        }
    }
}
