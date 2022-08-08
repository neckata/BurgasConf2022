using OnlineShop.Shared.Core.Wrapper;
using OnlineShop.Shared.DTOs.Module;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Core.Interfaces.Services.Module
{
    /// <summary>
    /// Get modules which are available for use
    /// </summary>
    public interface IModuleService
    {
        /// <summary>
        /// Gets all available modules
        /// </summary>
        /// <returns></returns>
        Task<IResult<List<ModuleResponse>>> GetAllModulesAsync();

        /// <summary>
        /// Gets Module detailed information
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IResult<ModuleResponse>> GetModuleAsync(Guid moduleId);

        /// <summary>
        /// Updates Module status
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        Task<IResult> UpdateModuleStatus(Guid moduleId, bool status);
    }
}
