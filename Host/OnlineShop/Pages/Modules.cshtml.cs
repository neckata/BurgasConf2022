using Host.OnlineShop.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.DTOs.Module;
using System;
using System.Threading.Tasks;

namespace BurgasConf2022.Pages
{
    public class ModulesModel : PageModel
    {
        private readonly IModuleResolver _moduleResolver;
        private readonly IModuleService _moduleService;
        private readonly IMediator _mediator;

        public ModuleResponse Module = null;

        public ModulesModel(IModuleResolver moduleResolver, IModuleService moduleService, IMediator mediator)
        {
            _moduleResolver = moduleResolver;
            _moduleService = moduleService;
            _mediator = mediator;
        }

        public async Task OnGet()
        {
            Microsoft.Extensions.Primitives.StringValues id;
            Request.Query.TryGetValue("id", out id);
            var response = await _moduleService.GetModuleAsync(Guid.Parse(id[0]));
            Module = response.Data;
        }
    }
}
