using Identity.Core.Entities;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Shared.Core.Interfaces;

namespace Identity.Core.Interfaces
{
    public interface IIdentityDbContext : IDbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<RoleClaim> RoleClaims { get; set; }
    }
}