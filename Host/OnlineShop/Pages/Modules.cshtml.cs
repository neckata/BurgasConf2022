using Host.OnlineShop.ModuleResolver;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using OnlineShop.DTOs.Items;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgasConf2022.Pages
{
    public class ModulesModel : PageModel
    {
        private readonly IModuleResolver _moduleResolver;
        private readonly IModuleService _moduleService;
        private readonly IMediator _mediator;

        public ModuleResponse Module = null;
        public List<Item> Items = new List<Item>();

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
            var module = await _moduleService.GetModuleAsync(Guid.Parse(id[0]));
            Module = module.Data;

            var command = _moduleResolver.CreateCommand(Module.Name);

            object response = await _mediator.Send(command);

            Items = JsonConvert.DeserializeObject<List<Item>>(JsonConvert.SerializeObject(response));
        }
    }
}
