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
            List<ModuleResponse> Modules = await _context.Modules.AsNoTracking().Select(c =>
            new ModuleResponse { Id = c.Id, Name = c.Name, isActive = c.IsActive, IsInSolution = c.IsInSolution }).ToListAsync();

            return await Result<List<ModuleResponse>>.SuccessAsync(Modules);
        }

        /// <summary>
        /// Gets Module detailed information
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public async Task<IResult<ModuleResponse>> GetModuleAsync(Guid moduleId)
        {
            ModuleResponse Module = await _context.Modules.AsNoTracking().Select(c =>
           new ModuleResponse { Id = c.Id, Name = c.Name, isActive = c.IsActive, IsInSolution = c.IsInSolution }).FirstOrDefaultAsync(c => c.Id == moduleId);

            return await Result<ModuleResponse>.SuccessAsync(Module);
        }

        /// <summary>
        /// Deactivates module
        /// </summary>
        /// <param name="ModumoduleIdleId"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateModuleStatus(Guid moduleId, bool status)
        {
            Entities.Module module = await _context.Modules.AsNoTracking().FirstOrDefaultAsync(c => c.Id == moduleId);
            module.IsActive = status;
            _context.Modules.Update(module);
            await _context.SaveChangesAsync();

            return await Result<ModuleResponse>.SuccessAsync();
        }
    }
}
