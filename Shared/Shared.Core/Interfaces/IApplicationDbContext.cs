using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Module> Modules { get; set; }

        public DbSet<Action> Actions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        int SaveChanges();
    }
}