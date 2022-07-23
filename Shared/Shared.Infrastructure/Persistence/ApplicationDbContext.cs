using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;
using OnlineShop.Shared.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        protected string Schema => "OnlineShop";

        public DbSet<Module> Modules { get; set; }

        public DbSet<Action> Actions { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await SaveChangesAsync(true, cancellationToken);
        }
    }
}