using System;

namespace OnlineShop.Shared.DTOs.Module
{
    /// <summary>
    /// ModuleResponse
    /// </summary>
    public class ModuleResponse
    {
        /// <summary>
        /// Module ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Module Name
        /// </summary>
        public string Name { get; set; }

        public bool IsInSolution { get; set; }

        public bool isActive { get; set; }
    }
}
