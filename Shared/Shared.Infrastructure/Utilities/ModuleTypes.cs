using OnlineShop.Shared.Core.Entities;
using System.Collections.Generic;

namespace OnlineShop.Shared.Infrastructure.Utilities
{
    public sealed class ModuleTypes
    {
        private ModuleTypes() { }

        public List<Module> Modules { get; set; }

        private static ModuleTypes instance = null;

        public static ModuleTypes Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ModuleTypes();
                }
                return instance;
            }
        }
    }
}
