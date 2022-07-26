using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Entities;

namespace OnlineShop.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<Module> Modules { get; set; }
    }
}