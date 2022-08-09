using Microsoft.AspNetCore.Mvc;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.Core.Wrapper;
using OnlineShop.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebAPI.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("[controller]")]
    public class ModulesController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModulesController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet]
        public async Task<IResult<List<ModuleResponse>>> GetModules()
        {
            return await _moduleService.GetAllModulesAsync();
        }

        [HttpGet("get-module/{moduleId}")]
        public async Task<IResult<ModuleResponse>> GetModule(Guid moduleId)
        {
            return await _moduleService.GetModuleAsync(moduleId);
        }

        [HttpPut]
        public async Task<IResult> UpdateModuleStatus(Guid moduleId, bool isActive)
        {
            return await _moduleService.UpdateModuleStatus(moduleId, isActive);
        }
    }
}
