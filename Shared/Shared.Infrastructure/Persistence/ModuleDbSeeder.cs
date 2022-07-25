using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Interfaces;
using OnlineShop.Shared.Core.Interfaces.Services;
using OnlineShop.Shared.Infrastructure.Utilities;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Infrastructure.Persistence
{
    internal class ModuleDbSeeder : IDatabaseSeeder
    {
        private readonly IApplicationDbContext _db;

        public ModuleDbSeeder(
            IApplicationDbContext db)
        {
            _db = db;
        }

        public void Initialize()
        {
            AddModules();
            _db.SaveChanges();
        }

        private void AddModules()
        {
            Task.Run(async () =>
            {
                var modules = await _db.Modules.ToListAsync();
                if (ModuleTypes.Instance.Modules != null)
                {
                    foreach (string ModuleName in ModuleTypes.Instance.Modules)
                    {
                        Module Module = new Module { Name = ModuleName };
                        var ModuleInDb = await _db.Modules.FirstOrDefaultAsync(x => x.Name == ModuleName);
                        if (ModuleInDb == null)
                        {
                            Module.IsUsed = true;
                            await _db.Modules.AddAsync(Module);
                        }
                        else
                        {
                            if (!ModuleInDb.IsUsed)
                            {
                                ModuleInDb.IsUsed = false;
                                _db.Modules.Update(ModuleInDb);
                                await _db.SaveChangesAsync();
                            }

                            modules.Remove(ModuleInDb);
                        }
                    }
                }

                foreach (var module in modules)
                {
                    module.IsUsed = false;
                    _db.Modules.Update(module);
                    await _db.SaveChangesAsync();
                }


            }).GetAwaiter().GetResult();
        }
    }
}
