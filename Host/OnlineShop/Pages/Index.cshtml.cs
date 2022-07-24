using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.DTOs.Module;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgasConf2022.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IModuleService _moduleService;

        public List<ModuleResponse> Modules = new List<ModuleResponse>();

        public IndexModel(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        public async Task OnGet()
        {
            var response = await _moduleService.GetAllModulesAsync();
            Modules = response.Data;
        }
    }
}
