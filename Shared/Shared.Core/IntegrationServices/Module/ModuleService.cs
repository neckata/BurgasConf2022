using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Interfaces.Services.Module;
using OnlineShop.Shared.Core.Wrapper;
using OnlineShop.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Core.IntegrationServices.Module
{
    /// <summary>
    /// Get modules which are availabe for use
    /// </summary>
    public class ModuleService : IModuleService
    {
        private readonly IApplicationDbContext _context;

        /// <summary>
        /// ModuleService
        /// </summary>
        /// <param name="context"></param>
        public ModuleService(IApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all availabe modules
        /// </summary>
        /// <returns></returns>
        public async Task<IResult<List<ModuleResponse>>> GetAllModulesAsync()
        {
            List<ModuleResponse> Modules = await _context.Modules.AsNoTracking().Select(c => new ModuleResponse { Id = c.Id, Name = c.Name }).ToListAsync();

            return await Result<List<ModuleResponse>>.SuccessAsync(Modules);
        }

        /// <summary>
        /// Gets Module detailed information
        /// </summary>
        /// <param name="ModuleId"></param>
        /// <returns></returns>
        public async Task<IResult<ModuleResponse>> GetModuleAsync(Guid ModuleId)
        {
            ModuleResponse Module = await _context.Modules.AsNoTracking().Select(c => new ModuleResponse { Id = c.Id, Name = c.Name }).FirstOrDefaultAsync(c => c.Id == ModuleId);

            return await Result<ModuleResponse>.SuccessAsync(Module);
        }
    }
}
